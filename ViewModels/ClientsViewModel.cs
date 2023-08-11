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
using CustomerDemo.Models;
using CustomerDemo.Services;
using FluentAvalonia.UI.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomerDemo.ViewModels;

public partial class ClientsViewModel : ViewModelBase
{
    private List<Client> _clients = new();
    
    [ObservableProperty]
    private ObservableRangeCollection<Client> _filteredClients = new();

    [ObservableProperty]
    private string _searchText = string.Empty;

    partial void OnSearchTextChanged(string? value)
    {
        ApplyFilter();
    }

    #region Constructors

    public ClientsViewModel() { }
    
    public ClientsViewModel(CimentalContext context)
    {
        _context = context;
    }

    #endregion

    [RelayCommand]
    private async Task LoadClients()
    {
        // DeleteAllClients();
        _clients = await _context.Clients.ToListAsync();
        ApplyFilter();
    }
    
    private void ApplyFilter()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            FilteredClients = new ObservableRangeCollection<Client>(_clients);
        }
        else
        {
            FilteredClients = new ObservableRangeCollection<Client>(_clients.Where(c => c.FirstName.StartsWith(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                                                                   c.LastName.StartsWith(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                                                                   c.Address.StartsWith(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                                                                   c.Email.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                                                                   c.Phone.StartsWith(SearchText, StringComparison.OrdinalIgnoreCase)));
        }
    }
    
    [RelayCommand]
    private async void AddClient(Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
        _clients.Add(client);
        FilteredClients.Add(client);
    }

    [RelayCommand]
    private void EditClient(object client)
    {
        var clientToEdit = (Client)client;
        
    }
    
    [RelayCommand]
    private void DeleteClient(Client client)
    {
        _context.Clients.Remove(client);
        _context.SaveChanges();
        _clients.Remove(client);
        FilteredClients.Remove(client);
    }
    
    private void DeleteAllClients()
    {
        _context.Clients.RemoveRange(_clients);
        _context.SaveChanges();
        _clients.Clear();
        FilteredClients.Clear();
    }

    [RelayCommand]
    private void GetClientsFromCsv(string filePath)
    {
        CsvDataService csvDataService = new();
        var clients = csvDataService.ReadCsv<Client>(filePath);
        AddClientRange(clients);
    }
    
    private void AddClientRange(IEnumerable<Client> clients)
    {
        var enumerable = clients.ToList();
        _context.Clients.AddRange(enumerable);
        _context.SaveChanges();
        _clients.AddRange(enumerable);
        FilteredClients.AddRange(enumerable);
    }

    private CimentalContext _context = new();
}