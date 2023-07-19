using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CustomerDemo.Services;

namespace CustomerDemo.ViewModels;

public partial class NewClientViewModel : ViewModelBase
{
    #region Navigation
    
    [ObservableProperty]
    private INavigationService? _navigationService;
    
    [RelayCommand]
    private void NavigateToClients() => NavigationService?.NavigateTo<ClientsViewModel>();

    #endregion

    #region Constructors
    
    public NewClientViewModel()
    {
    }
    
    public NewClientViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
    }

    #endregion

    [RelayCommand]
    private void SaveClient()
    {
        
    }

}