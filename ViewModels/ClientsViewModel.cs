using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CustomerDemo.Models;
using CustomerDemo.Services;
using FluentAvalonia.UI.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomerDemo.ViewModels;

public partial class ClientsViewModel : ViewModelBase
{
    #region Navigation
    [ObservableProperty]
    private INavigationService? _navigationService;
    
    [RelayCommand]
    private void NavigateToNewClient() => NavigationService?.NavigateTo<NewClientViewModel>();
    #endregion
    
    [ObservableProperty]
    private string _searchText = string.Empty;
    partial void OnSearchTextChanged(string? value)
    {
        ApplyFilter();
    }
    
    private void ApplyFilter()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            foreach (var client in _clients)
            {
                FilteredClients.Add(client);
            }
        }
        else
        {
            FilteredClients = new ObservableCollection<Client>(_clients.Where(c => c.FirstName.StartsWith(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                                                                   c.LastName.StartsWith(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                                                                   c.Address.StartsWith(SearchText, StringComparison.OrdinalIgnoreCase)));
        }
    }

    private List<Client> _clients = new();
    
    [ObservableProperty]
    private ObservableCollection<Client> _filteredClients = new();


    public void LoadClientsAsync()
    {
        _clients = _context.Clients.ToList();
        ApplyFilter();
    }
    
    #region Constructors
    public ClientsViewModel() { }
    
    public ClientsViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
        LoadClientsAsync();
    }
    #endregion

    private CimentalContext _context = new();
}