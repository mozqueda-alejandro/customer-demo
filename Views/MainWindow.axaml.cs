using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Styling;
using FluentAvalonia.UI.Windowing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Avalonia.Data;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Input;
using CustomerDemo.Services;
using CustomerDemo.ViewModels;
using FluentAvalonia.Core;
using FluentAvalonia.UI.Controls;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CustomerDemo.Views;

public class MenuItemContainer
{
    NavigationViewItem? NavigationViewItem { get; set; }
    bool IsChild { get; set; }
}

public partial class MainWindow : AppWindow
{
    private readonly MainWindowViewModel _viewModel;
    private readonly NavigationView _navigationView;

    private Dictionary<string, object> NavigationItems { get; } = new();
    private Dictionary<string, IRelayCommand> NavigationCommands { get; }
    
    public MainWindow() => InitializeComponent();

    public MainWindow(MainWindowViewModel viewModel)
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        
        _viewModel = viewModel;
        _navigationView = this.FindControl<NavigationView>("NavigationView")!;
        DataContext = _viewModel;
        
        // var nv = this.FindControl<NavigationView>("NavigationViewMain");
        // // var nvip = this.FindControl<NavigationViewItem>("SalesView");
        // var nvi = this.FindControl<NavigationViewItem>("ClientsView");
        // nv!.SelectedItem = nvi;
        // NavigationViewItem nvip = (NavigationViewItem)nvi?.Parent;
        // nvip!.IsExpanded = true;
        
        NavigationCommands = new Dictionary<string, IRelayCommand>
        {
            { nameof(HomeView), _viewModel.NavigateToHomeCommand },
            { nameof(DashboardView), _viewModel.NavigateToDashboardCommand },
            { nameof(ClientsView), _viewModel.NavigateToClientsCommand },
            { nameof(SettingsView), _viewModel.NavigateToSettingsCommand }
        };
        
        InitializeNavigation();
    }

    private void InitializeNavigation()
    {
        foreach (var menuItem in _navigationView.MenuItems)
        {
            if (menuItem is not NavigationViewItem navigationViewItem) continue;
            
            if (navigationViewItem.MenuItems.Count == 0)
            {
                NavigationItems.Add(navigationViewItem.Name!, navigationViewItem);
            }
            else
            {
                foreach (NavigationViewItem subItem in navigationViewItem.MenuItems)
                {
                    NavigationItems.Add(subItem.Name!, subItem);
                }
            }
        }

        foreach (var footerItem in _navigationView.FooterMenuItems)
        {
            NavigationItems.Add(((NavigationViewItem)footerItem).Name!, footerItem);
        }

        _navigationView.SelectionChanged += OnMenuItemChanged;
        _viewModel.NavigationService.PropertyChanged += OnCurrentViewChanged;
        _navigationView.SelectedItem = _navigationView.MenuItems[0];
    }

    private void OnMenuItemChanged(object? sender, NavigationViewSelectionChangedEventArgs e)
    {
        if (e.SelectedItem is not NavigationViewItem selectedItem) return;
        if (_viewModel.NavigationService.CurrentView != null)
        {
            if (selectedItem.Name == _viewModel.NavigationService.CurrentViewName) return; // Prevents view from being reselected
        }

        if (NavigationCommands.TryGetValue(selectedItem.Name!, out IRelayCommand? navigationCommand))
        {
            navigationCommand.Execute(null);
        }
    }

    private void OnCurrentViewChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is not INavigationService navigationService) return;
        if (e.PropertyName != nameof(navigationService.CurrentViewName)) return;
        
        var prevSelection = _navigationView.SelectedItem as NavigationViewItem;
        if (prevSelection!.Name == navigationService.CurrentViewName) return; // Prevents item from being reselected
        
        if (!NavigationItems.TryGetValue(navigationService.CurrentViewName, out var navigationViewItem)) return;
        if (navigationViewItem is NavigationViewItem newSelection)
        {
            newSelection.IsSelected = true;
        }
    }

    private void MainWindow_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var mainWindow = this.FindControl<Window>("MainWindowControl");
        mainWindow?.Focus();
    }
}
