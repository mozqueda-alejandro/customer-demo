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

    public HomeViewModel()
    {
    }
    
    public HomeViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
    }
}