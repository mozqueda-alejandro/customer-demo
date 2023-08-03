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
    public ClientsView()
    {
        InitializeComponent();
        Loaded += ClientsView_Loaded;
    }

    private void ClientsView_Loaded(object? sender, RoutedEventArgs e)
    {
        if (DataContext is ClientsViewModel viewModel) viewModel.LoadClientsCommand.Execute(null);
    }

    private void DataGrid_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is DataGrid dataGrid) dataGrid.SelectedItem = null;
    }

    private void MenuItemView_OnClick(object? sender, RoutedEventArgs e)
    {
        // Pop dialog

    }
}