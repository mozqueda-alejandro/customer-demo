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
    private INavigationService? _navigationService;

    [ObservableProperty]
    private object? _selectedCategory;
    partial void OnSelectedCategoryChanged(object? placeholder)
    {
        SetCurrentPage();
    }

    [ObservableProperty]
    private Control? _currentPage;
    
    
    #region NavigationService
    [RelayCommand]
    private void NavigateToHome() => NavigationService?.NavigateTo<HomeViewModel>();
    
    [RelayCommand]
    private void NavigateToDashboard() => NavigationService?.NavigateTo<DashboardViewModel>();
    
    [RelayCommand]
    private void NavigateToSettings() => NavigationService?.NavigateTo<SettingsViewModel>();

    [RelayCommand]
    private void NavigateToClients() => NavigationService?.NavigateTo<ClientsViewModel>();
    #endregion

    public MainWindowViewModel() { }
    
    public MainWindowViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
    }
    
    private void SetCurrentPage()
    {
        if (SelectedCategory is Category cat)
        {
            
        }
        else if (SelectedCategory is NavigationViewItem nvi)
        {
            NavigateToSettings();
        }
    }
}
