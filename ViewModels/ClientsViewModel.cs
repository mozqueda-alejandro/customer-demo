using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CustomerDemo.Messages;
using CustomerDemo.Models;
using CustomerDemo.Services;
using FluentAvalonia.UI.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomerDemo.ViewModels;

public partial class ClientsViewModel : ViewModelBase
{
    private readonly IMessenger _messenger;
    
    private List<Client> _clients = new();
    
    [ObservableProperty]
    private ObservableCollection<Client> _filteredClients = new();

    [ObservableProperty]
    private string _searchText = string.Empty;

    partial void OnSearchTextChanged(string? value)
    {
        ApplyFilter();
    }

    #region Constructors
    // public ClientsViewModel() { }
    
    public ClientsViewModel()
    {
        WeakReferenceMessenger.Default.Register<EditClientMessage>(this, (r, m) => EditClient(m.Value));
    }

    #endregion

    [RelayCommand]
    private async Task LoadClients()
    {
        _clients = await _context.Clients.ToListAsync();
        ApplyFilter();
    }
    
    private void ApplyFilter()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            FilteredClients.Clear();
            
            foreach (var client in _clients)
            {
                FilteredClients.Add(client);
            }
        }
        else
        {
            FilteredClients = new ObservableCollection<Client>(_clients.Where(c => c.FirstName.StartsWith(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                                                                   c.LastName.StartsWith(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                                                                   c.Address.StartsWith(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                                                                   c.Email.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                                                                   c.Phone.StartsWith(SearchText, StringComparison.OrdinalIgnoreCase)));
        }
    }

    private void EditClient(object client)
    {
        
    }
    
    [RelayCommand]
    private void DeleteClient(Client client)
    {
        _context.Clients.Remove(client);
        _context.SaveChanges();
        _clients.Remove(client);
        FilteredClients.Remove(client);
    }

    private CimentalContext _context = new();
}