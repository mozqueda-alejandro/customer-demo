using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.DependencyInjection;
using CustomerDemo.Models;
using CustomerDemo.Services;
using CustomerDemo.ViewModels;
using FluentAvalonia.Core;
using FluentAvalonia.UI.Controls;

namespace CustomerDemo.Views;

public partial class ClientsView : UserControl
{
    private ClientsViewModel _viewModel;

    public ClientsView()
    {
        InitializeComponent();
        Loaded += ClientsView_Loaded;
    }

    private void ClientsView_Loaded(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not ClientsViewModel viewModel) return;
        _viewModel = viewModel;
        _viewModel.LoadClientsCommand.Execute(null);
    }

    
    private async void AddClient_OnClick(object? sender, RoutedEventArgs e)
    {
        var createDialog = new ContentDialog
        {
            Title = "Add Client",
            PrimaryButtonText = "Cancel",
            SecondaryButtonText = "Save client",
            DefaultButton = ContentDialogButton.Secondary
        };
        
        var createViewModel = new ClientFormCreateViewModel();
        createDialog.Content = new ClientFormView()
        {
            DataContext = createViewModel
        };

        void DialogClosing(ContentDialog s, ContentDialogClosedEventArgs e)
        {
            if (e.Result == ContentDialogResult.Primary) return;
            var clientResult = createViewModel.NewClient;
            _viewModel.AddClientCommand.Execute(clientResult);
            createDialog.Closed -= DialogClosing;
        }
        createDialog.Closed += DialogClosing;

        _ = await createDialog.ShowAsync();
    }

    private async void EditMenuItem_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is not MenuItem menuItem) return;
        var client = (Client)menuItem.CommandParameter!;
        
        var editViewModel = new ClientFormEditViewModel(client);
        var editDialog = new ContentDialog
        {
            Title = "Edit Client",
            PrimaryButtonText = "Cancel",
            SecondaryButtonText = "Save changes",
            IsSecondaryButtonEnabled = false,
            // DefaultButton = ContentDialogButton.Secondary,
            Content = new ClientFormView()
            {
                DataContext = editViewModel
            }
        };
        
        editViewModel.PropertyChanged += (s, f) =>
        {
            if (f.PropertyName == nameof(editViewModel.IsValid))
            {
                editDialog.IsSecondaryButtonEnabled = editViewModel.IsValid;
            }
        };
        
        editDialog.Closed += async (s, e) =>
        {
            if (e.Result == ContentDialogResult.Primary) return;
            var clientResult = editViewModel.EditedClient;
            _viewModel.EditClientCommand.Execute(clientResult);
        
        };
        
        _ = await editDialog.ShowAsync();
    }

    private void DataGrid_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is DataGrid dataGrid && !_canSelectRow)
            dataGrid.SelectedItem = null;
        e.Handled = true;
        _canSelectRow = false;
    }

    private void MenuFlyout_OnPointerEntered(object? sender, PointerEventArgs e)
    {
        // if (sender is not MenuItem menuItem) return;
        // var client = (Client)menuItem.CommandParameter!;
        //
        // _canSelectRow = true;
        // ClientsGrid.SelectedItem = client;

        // SharedClass.MenuFlyout_OnPointerEntered();
    }

    private void MenuFlyout_OnPointerExited(object? sender, PointerEventArgs e)
    {
        ClientsGrid.SelectedItem = null;
        _canSelectRow = false;
    }

    private bool _canSelectRow = false;
}