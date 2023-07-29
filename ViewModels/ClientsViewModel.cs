using System.Collections.ObjectModel;
using System.Linq;
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
    // public ClientsViewModel() { }
    
    public ClientsViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
        RefreshClients();
    }
    #endregion

    public void RefreshClients()
    {
        Clients = new ObservableCollection<Client>(_context.Clients);
    }
    
    public ObservableCollection<Client> Clients { get; set; }

    private CimentalContext _context = new();
}