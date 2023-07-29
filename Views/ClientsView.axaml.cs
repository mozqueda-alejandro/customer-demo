using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using CustomerDemo.Models;
using CustomerDemo.ViewModels;

namespace CustomerDemo.Views;

public partial class ClientsView : UserControl
{
    private ClientsViewModel _viewModel;

    public ClientsView()
    {
        InitializeComponent();
        Loaded += (sender, args) =>
        {
            _viewModel = (DataContext as ClientsViewModel)!;
            _clients = new List<Client>(_viewModel.Clients);
            ClientGrid.ItemsSource = _clients;
        };
    }

    private void DataGrid_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is DataGrid dataGrid)
        {
            dataGrid.SelectedItem = null; // Prevents the selection from sticking
        }
    }

    private void ClientTextBox_OnKeyUp(object? sender, KeyEventArgs e)
    {
        if (string.IsNullOrEmpty(ClientTextBox.Text))
        {
            var grid1 = this.FindControl<DataGrid>("ClientGrid");
            grid1.ItemsSource = _clients;
            return;
        }
        var filtered = _clients.Where(client => client.FirstName.StartsWith(ClientTextBox.Text, true, null) ||
                                                client.LastName.StartsWith(ClientTextBox.Text, true, null));
        var grid = this.FindControl<DataGrid>("ClientGrid");
        grid.ItemsSource = filtered;
    }

    private List<Client> _clients;
}