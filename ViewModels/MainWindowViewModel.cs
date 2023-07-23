using System;
using System.ComponentModel;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Metadata;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CustomerDemo.Services;
using CustomerDemo.Views;
using FluentAvalonia.UI.Controls;

namespace CustomerDemo.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _title = "Binding from MainWindowViewModel";

    #region NavigationService
    [ObservableProperty]
    private INavigationService _navigationService;
    
    [RelayCommand]
    private void NavigateToHome() => NavigationService.NavigateTo<HomeViewModel>();
    
    [RelayCommand]
    private void NavigateToDashboard() => NavigationService.NavigateTo<DashboardViewModel>();
    
    [RelayCommand]
    private void NavigateToEstimates() => NavigationService.NavigateTo<EstimatesViewModel>();

    [RelayCommand]
    private void NavigateToClients() => NavigationService.NavigateTo<ClientsViewModel>();
    
    [RelayCommand]
    private void NavigateToVendors() => NavigationService.NavigateTo<VendorsViewModel>();

    [RelayCommand]
    private void NavigateToSettings() => NavigationService.NavigateTo<SettingsViewModel>();
    #endregion

    public MainWindowViewModel() { }
    
    public MainWindowViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
        
    }
}
