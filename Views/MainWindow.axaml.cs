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
using System.Windows.Input;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Input;
using CustomerDemo.ViewModels;
using FluentAvalonia.Core;
using FluentAvalonia.UI.Controls;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CustomerDemo.Views;

public partial class MainWindow : AppWindow
{
    // NavigationView SelectionChanged event handler


    public MainWindow()
    {
        InitializeComponent();
    }

    public MainWindow(MainWindowViewModel mainWindowViewModel)
    {
        DataContext = mainWindowViewModel;
        InitializeComponent();
        
#if DEBUG
        this.AttachDevTools();
#endif
        
        var nv = this.FindControl<NavigationView>("NavigationViewMain");
        nv!.SelectionChanged += OnMenuItemChanged;
        nv.SelectedItem = nv.MenuItems[0];
        var nvip = this.FindControl<NavigationViewItem>("SalesView");
        nvip.IsExpanded = true;
        var nvi = this.FindControl<NavigationViewItem>("ClientsView");
        nv.SelectedItem = nvi;
    }

    private void OnMenuItemChanged(object? sender, NavigationViewSelectionChangedEventArgs e)
    {
        if (DataContext is not MainWindowViewModel vm) return;
        
        if (e.IsSettingsSelected)
        {
            vm.NavigateToSettingsCommand.Execute(null); 
        }
        else if (e.SelectedItem is NavigationViewItem navigationViewItem)
        {
            var navigationName = navigationViewItem.Name;
            ArgumentNullException.ThrowIfNull(navigationName, nameof(navigationViewItem.Name));
            if (vm.NavigationCommands!.TryGetValue(navigationName, out IRelayCommand? navigationCommand))
            {
                navigationCommand.Execute(null);
            }
        }
    }

    private void MainWindow_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var mw = this.FindControl<Window>("MainWindowControl");
        mw?.Focus();
        
        var nv = this.FindControl<NavigationView>("NavigationViewMain");
        
    }
}
