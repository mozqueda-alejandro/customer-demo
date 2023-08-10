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
    public NavigationViewItem MenuItem { get; }
    public string ParentName { get; }
    public bool HasParent { get; }

    public MenuItemContainer(NavigationViewItem menuItem, string parentName)
    {
        MenuItem = menuItem;
        ParentName = parentName;
        HasParent = !string.IsNullOrEmpty(ParentName);
    }
}

public partial class MainWindow : AppWindow
{
    private MainWindowViewModel _viewModel;
    private NavigationView _navigationView;
    private Dictionary<string, IRelayCommand> NavigationCommands { get; } = new();
    private Dictionary<string, MenuItemContainer> NavigationItems { get; } = new();
    private HashSet<string> ParentNavigationItemNames { get; } = new();

    public MainWindow() => InitializeComponent();

    public MainWindow(MainWindowViewModel viewModel)
    {
        InitializeComponent();
        TitleBar.ExtendsContentIntoTitleBar = false;
        // TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;
#if DEBUG
        this.AttachDevTools();
#endif
        
        _viewModel = viewModel;
        _navigationView = this.FindControl<NavigationView>("NavigationViewControl")!;
        DataContext = _viewModel;

        InitNavigationCommands();
        InitNavigationItems();

        _viewModel.NavigationService.PropertyChanged += NavigationService_OnPropertyChanged;
        _navigationView.SelectionChanged += OnMenuItemChanged;
        _navigationView.BackRequested += OnBackRequested;
        _navigationView.SelectedItem = _navigationView.MenuItems.ElementAt(0);

        SetMenuItemExpansion(true);
        _navigationView.IsBackEnabled = false;
        Loaded += (sender, _) => ((MainWindow)sender!).SetMenuItemExpansion(false);
    }
    
    private void NavigationService_OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is not INavigationService navigationService) return;
        switch (e.PropertyName)
        {
            case nameof(navigationService.CurrentViewName):
                OnCurrentViewChanged();
                break;
            case nameof(navigationService.CanNavigateBack):
                OnCanNavigateBackChanged();
                break;
        }
    }

    private void OnCurrentViewChanged()
    {
        var prevSelectionName = ((NavigationViewItem)_navigationView.SelectedItem).Name;
        if (prevSelectionName == _viewModel.NavigationService.CurrentViewName) return; // Prevents item from being reselected
        
        var wasPaneExpanded = _navigationView.IsPaneOpen;
        _navigationView.IsPaneOpen = true;
        SetMenuItemExpansion(true);

        if (!NavigationItems.TryGetValue(_viewModel.NavigationService.CurrentViewName, out var menuItemContainer)) return;
        if (menuItemContainer.HasParent)
        {
            var parent = this.FindControl<NavigationViewItem>(menuItemContainer.ParentName)!;
            parent.IsChildSelected = true;
        }
        
        var newSelection = menuItemContainer.MenuItem;
        _navigationView.SelectedItem = newSelection;
        newSelection.IsSelected = true;

        // If previous selection has a parent, and if new selection's parent is not the same,
        // set previous parent IsChildSelected to false
        if (NavigationItems.TryGetValue(prevSelectionName!, out var prevMenuItemContainer))
        {
            if (prevMenuItemContainer.HasParent && prevMenuItemContainer.ParentName != menuItemContainer.ParentName)
            {
                var prevParent = this.FindControl<NavigationViewItem>(prevMenuItemContainer.ParentName)!;
                prevParent.IsChildSelected = false;
                prevParent.IsSelected = false;
            }
        }

        SetMenuItemExpansion(false);
        _navigationView.IsPaneOpen = wasPaneExpanded;
    }

    private void OnMenuItemChanged(object? sender, NavigationViewSelectionChangedEventArgs e)
    {
        if (e.SelectedItem is not NavigationViewItem selectedItem) return;
        if (_viewModel.NavigationService.CurrentView != null)
        {
            if (selectedItem.Name == _viewModel.NavigationService.CurrentViewName) return; // Prevents navigation to same view
        }
        
        if (NavigationCommands.TryGetValue(selectedItem.Name!, out var navigationCommand))
        {
            navigationCommand.Execute(null);
        }
    }

    private void OnCanNavigateBackChanged()
    {
        _navigationView.IsBackEnabled = _viewModel.NavigationService.CanNavigateBack;
    }

    private void OnBackRequested(object? sender, NavigationViewBackRequestedEventArgs e) =>
        _viewModel.NavigateBackCommand.Execute(null);

    public void SetMenuItemExpansion(bool isExpanded)
    {
        // Takes a bool and sets the IsExpanded property of all parent menu items to that value
        foreach (var parentName in ParentNavigationItemNames)
        {
            var parent = this.FindControl<NavigationViewItem>(parentName)!;
            parent.IsExpanded = isExpanded;
        }
    }

    private void InitNavigationCommands()
    {
        NavigationCommands.Add(nameof(HomeView), _viewModel.NavigateToHomeCommand);
        NavigationCommands.Add(nameof(DashboardView), _viewModel.NavigateToDashboardCommand);
        NavigationCommands.Add(nameof(JobsView), _viewModel.NavigateToJobsCommand);
        NavigationCommands.Add(nameof(EstimatesView), _viewModel.NavigateToEstimatesCommand);
        NavigationCommands.Add(nameof(ClientsView), _viewModel.NavigateToClientsCommand);
        NavigationCommands.Add(nameof(VendorsView), _viewModel.NavigateToVendorsCommand);
        NavigationCommands.Add(nameof(SettingsView), _viewModel.NavigateToSettingsCommand);
    }

    private void InitNavigationItems()
    {
        foreach (var menuItem in _navigationView.MenuItems)
        {
            if (menuItem is not NavigationViewItem navigationViewItem) continue;
            
            if (navigationViewItem.MenuItems.Count == 0)
            {
                NavigationItems.Add(navigationViewItem.Name!, new MenuItemContainer(navigationViewItem,string.Empty));
            }
            else
            {
                foreach (NavigationViewItem subItem in navigationViewItem.MenuItems)
                {
                    NavigationItems.Add(subItem.Name!, new MenuItemContainer(subItem, navigationViewItem.Name!));
                }
                ParentNavigationItemNames.Add(navigationViewItem.Name!);
            }
        }

        foreach (var footerItem in _navigationView.FooterMenuItems)
        {
            if (footerItem is not NavigationViewItem navigationViewItem) continue;
            NavigationItems.Add(navigationViewItem.Name!, new MenuItemContainer(navigationViewItem, string.Empty));
        }
    }
    
    private void MainWindow_OnPointerPressed(object? sender, PointerPressedEventArgs e) => MainWindowControl?.Focus();
}
