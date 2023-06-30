using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CustomerDemo.Models;
using CustomerDemo.Services;

namespace CustomerDemo.ViewModels;

public partial class ClientsViewModel : ViewModelBase
{
    #region Navigation
    
    [ObservableProperty]
    private INavigationService? _navigationService;
    
    [RelayCommand]
    private void NavigateToNewClient() => NavigationService?.NavigateTo<NewClientViewModel>();

    #endregion

    #region Constructors
    
    public ClientsViewModel()
    {
    }
    
    public ClientsViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
    }

    #endregion

    // Data source for the datagrid containing all clients
    public ObservableCollection<Client> Clients { get; set; } = new();
    
    
}