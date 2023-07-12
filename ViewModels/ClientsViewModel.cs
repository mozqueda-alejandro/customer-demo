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

    public ClientsViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
    }

    #endregion

    // Data source for the datagrid containing all clients
    public ObservableCollection<Client> Clients { get; set; } = FillClients();

    private static ObservableCollection<Client> FillClients()
    {
        // Add multiple clients with first name, last name, address, email, phone, and source
        ObservableCollection<Client> group = new();
        group.Add(new Client("John", "Doe", "123 Main St", "j@gmail.com", "123-456-7890", "Google"));
        group.Add(new Client("Jane", "Doe", "123 Main St", "jane@hotmail.com", "231-456-7890", "Google"));
        return group;
    }
}