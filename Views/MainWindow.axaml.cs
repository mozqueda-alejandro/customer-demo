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
    public NavigationViewItem? NavigationViewItem { get; set; }
    public string ParentName { get; set; } = string.Empty;
    public bool IsChild { get; set; }
}

public partial class MainWindow : AppWindow
{
    private readonly MainWindowViewModel _viewModel;
    private readonly NavigationView _navigationView;

    private Dictionary<string, MenuItemContainer> NavigationItems { get; } = new();
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

        NavigationCommands = new Dictionary<string, IRelayCommand>
        {
            { nameof(HomeView), _viewModel.NavigateToHomeCommand },
            { nameof(DashboardView), _viewModel.NavigateToDashboardCommand },
            { nameof(EstimatesView), _viewModel.NavigateToEstimatesCommand },
            { nameof(ClientsView), _viewModel.NavigateToClientsCommand },
            { nameof(VendorsView), _viewModel.NavigateToVendorsCommand },
            { nameof(SettingsView), _viewModel.NavigateToSettingsCommand }
        };
        
        InitializeNavigation();
        
        _navigationView.SelectedItem = _navigationView.MenuItems[0];
        
        SetMenuItemExpansion(true);
    }

    private void InitializeNavigation()
    {
        foreach (var menuItem in _navigationView.MenuItems)
        {
            if (menuItem is not NavigationViewItem navigationViewItem) continue;
            
            if (navigationViewItem.MenuItems.Count == 0)
            {
                NavigationItems.Add(navigationViewItem.Name!, new MenuItemContainer
                {
                    NavigationViewItem = navigationViewItem,
                    IsChild = false
                });
            }
            else
            {
                foreach (NavigationViewItem subItem in navigationViewItem.MenuItems)
                {
                    NavigationItems.Add(subItem.Name!, new MenuItemContainer
                    {
                        NavigationViewItem = subItem,
                        ParentName = navigationViewItem.Name!,
                        IsChild = true
                    });
                }
            }
        }

        foreach (var footerItem in _navigationView.FooterMenuItems)
        {
            if (footerItem is not NavigationViewItem navigationViewItem) continue;
            NavigationItems.Add(navigationViewItem.Name!, new MenuItemContainer
            {
                NavigationViewItem = navigationViewItem,
                IsChild = false
            });
        }

        _navigationView.SelectionChanged += OnMenuItemChanged;
        _viewModel.NavigationService.PropertyChanged += OnCurrentViewChanged;
        // _navigationView.SelectedItem = _navigationView.MenuItems[0];
    }

    private void OnMenuItemChanged(object? sender, NavigationViewSelectionChangedEventArgs e)
    {
        if (e.SelectedItem is not NavigationViewItem selectedItem) return;
        if (_viewModel.NavigationService.CurrentView != null)
        {
            if (selectedItem.Name == _viewModel.NavigationService.CurrentViewName) return; // Prevents view from being reselected
        }
        
        if (NavigationCommands.TryGetValue(selectedItem.Name!, out var navigationCommand))
        {
            navigationCommand.Execute(null);
        }
    }

    private void OnCurrentViewChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is not INavigationService navigationService) return;
        if (e.PropertyName != nameof(navigationService.CurrentViewName)) return;
        var prevSelection = _navigationView.SelectedItem as NavigationViewItem;
        var prevSelectionName = prevSelection?.Name;
        var currentViewName = navigationService.CurrentViewName;
        if (prevSelection!.Name == currentViewName) return; // Prevents item from being reselected
        
        var wasPaneExpanded = _navigationView.IsPaneOpen;
        _navigationView.IsPaneOpen = true;
        
        SetMenuItemExpansion(true);

        if (!NavigationItems.TryGetValue(navigationService.CurrentViewName, out var menuItemContainer)) return;
        NavigationViewItem parent = new();
        if (menuItemContainer.IsChild)
        {
            parent = this.FindControl<NavigationViewItem>(menuItemContainer.ParentName)!;
            parent.IsChildSelected = true;
        }
        var child = this.FindControl<NavigationViewItem>(currentViewName)!;
        if (menuItemContainer.NavigationViewItem is { } newSelection)
        {
            _navigationView.SelectedItem = child;
            child.ApplyTemplate();
            child.IsSelected = true;
        }


        _navigationView.IsPaneOpen = wasPaneExpanded;
        // Check if previous selection is a child, if so, set parent IsChildSelected to false
        var sales = this.FindControl<NavigationViewItem>("Sales");
        if (prevSelectionName == "EstimatesView")
        {
            sales.IsChildSelected = false;
            sales.IsSelected = false;
        }
        
        SetMenuItemExpansion(false);
    }

    public void SetMenuItemExpansion(bool isExpanded)
    {
        // Takes a bool and sets the IsExpanded property of all parent menu items to that value
        var sales = this.FindControl<NavigationViewItem>("Sales");
        var purchases = this.FindControl<NavigationViewItem>("Purchases");
        sales!.IsExpanded = isExpanded;
        purchases!.IsExpanded = isExpanded;
    }
    

    private void MainWindow_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var mainWindow = this.FindControl<Window>("MainWindowControl");
        mainWindow?.Focus();
    }
}
