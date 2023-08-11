using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Metadata;
using Avalonia.Platform.Storage;
using Avalonia.Xaml.Interactivity;
using CommunityToolkit.Mvvm.DependencyInjection;
using CustomerDemo.Models;
using CustomerDemo.Services;
using CustomerDemo.ViewModels;
using FluentAvalonia.Core;
using FluentAvalonia.UI.Controls;

namespace CustomerDemo.Views;

// public class AttachedBehaviors : AvaloniaObject
// {
//     static AttachedBehaviors()
//     {
//         BehaviorsProperty.Changed.Subscribe(x => HandleBehaviorsChanged(x.Sender, x.NewValue.GetValueOrDefault<ObjectTemplate>()));
//     }
//
//     public static readonly AttachedProperty<ObjectTemplate> BehaviorsProperty = AvaloniaProperty.RegisterAttached<AttachedBehaviors, Interactive, ObjectTemplate>(
//         "Behaviors", default(ObjectTemplate), false, BindingMode.OneTime);
//
//
//     private static void HandleBehaviorsChanged(AvaloniaObject element, ObjectTemplate? behaviorTemplate)
//     {
//         BehaviorCollection collection = null;
//         if (behaviorTemplate != null) {
//             var value = behaviorTemplate.Build();
//
//             if (value is BehaviorCollection behaviorCollection)
//                 collection = behaviorCollection;
//             else if (value is Behavior behavior)
//                 collection = new BehaviorCollection { behavior };
//             else throw new Exception($"AttachedBehaviors should be a BehaviorCollection or an IBehavior.");
//         }
//
//         // collection may be null here, if e.NewValue is null
//         Interaction.SetBehaviors(element, collection);
//     }
//
//     public static void SetBehaviors(AvaloniaObject element, ObjectTemplate commandValue)
//     {
//         element.SetValue(BehaviorsProperty, commandValue);
//     }
//     
//     public static ObjectTemplate GetBehaviors(AvaloniaObject element)
//     {
//         return element.GetValue(BehaviorsProperty);
//     }
// }
//
// public class ObjectTemplate
// {
//     [Content]
//     [TemplateContent(TemplateResultType = typeof(object))]
//     public object Content { get; set; }
//
//     public object Build(object data = null) => TemplateContent.Load<object>(Content).Result;
// }


public partial class ClientsView : UserControl
{
    private ClientsViewModel _viewModel;
    private IDialogService _dialogService = new DialogService();

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
        if (sender is not MenuItem menuItem) return;
        var client = (Client)menuItem.CommandParameter!;
        
        _canSelectRow = true;
        ClientsGrid.SelectedItem = client;
    }

    private void MenuFlyout_OnPointerExited(object? sender, PointerEventArgs e)
    {
        ClientsGrid.SelectedItem = null;
        _canSelectRow = false;
    }

    private bool _canSelectRow = false;

    private async void ImportCsv_OnClick(object? sender, RoutedEventArgs e)
    {
        var fileName = await _dialogService.OpenFileDialog(this, DialogFileFilter.Csv);
        _viewModel.GetClientsFromCsvCommand.Execute(fileName);
    }
}