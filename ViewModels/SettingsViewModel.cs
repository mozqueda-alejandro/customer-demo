using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CustomerDemo.Services;

namespace CustomerDemo.ViewModels;

public partial class SettingsViewModel : ViewModelBase
{
    [ObservableProperty]
    private INavigationService? _navigationService;
    
    [RelayCommand]
    private void NavigateToHome() => NavigationService?.NavigateTo<HomeViewModel>();

    public SettingsViewModel()
    {
    }
    
    public SettingsViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
    }
}