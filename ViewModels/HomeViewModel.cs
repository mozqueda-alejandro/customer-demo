using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CustomerDemo.Services;

namespace CustomerDemo.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    [ObservableProperty]
    private INavigationService? _navigationService;
    
    [RelayCommand]
    private void NavigateToSettings() => NavigationService?.NavigateTo<SettingsViewModel>();
    
    [RelayCommand]
    private void NavigateBack() => NavigationService?.NavigateBack();

    public HomeViewModel()
    {
    }
    
    public HomeViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
    }
}