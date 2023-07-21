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
    public Dictionary<string, IRelayCommand>? NavigationCommands { get; set; }

    [ObservableProperty]
    private INavigationService? _navigationService;

    #region NavigationService
    [RelayCommand]
    private void NavigateToHome() => NavigationService?.NavigateTo<HomeViewModel>();
    
    [RelayCommand]
    private void NavigateToDashboard() => NavigationService?.NavigateTo<DashboardViewModel>();

    [RelayCommand]
    private void NavigateToClients() => NavigationService?.NavigateTo<ClientsViewModel>();

    [RelayCommand]
    private void NavigateToSettings() => NavigationService?.NavigateTo<SettingsViewModel>();
    #endregion

    public MainWindowViewModel() { }
    
    public MainWindowViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
        NavigationCommands = new Dictionary<string, IRelayCommand>
        {
            { nameof(HomeView), NavigateToHomeCommand },
            { nameof(DashboardView), NavigateToDashboardCommand },
            { nameof(ClientsView), NavigateToClientsCommand },
            { nameof(SettingsView), NavigateToSettingsCommand }
        };
    }
}
