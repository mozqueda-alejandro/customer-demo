using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Styling;
using FluentAvalonia.UI.Windowing;
using System;
using CustomerDemo.ViewModels;
using FluentAvalonia.Core;
using FluentAvalonia.UI.Controls;

namespace CustomerDemo.Views;

public partial class MainWindow : AppWindow
{
    public MainWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        var nv = this.FindControl<NavigationView>("NavigationViewMain");
        nv.SelectionChanged += OnMenuItemChanged;
        nv.SelectedItem = nv.MenuItems.ElementAt(0);
    }
    
    private void OnMenuItemChanged(object sender, NavigationViewSelectionChangedEventArgs e)
    {
        var vm = this.DataContext as MainWindowViewModel;
        if (sender == null) throw new ArgumentNullException(nameof(sender));
        
        if (e.IsSettingsSelected)
        {
            vm!.NavigateToSettingsCommand.Execute(null);
        }
        else if (e.SelectedItem is NavigationViewItem nvi)
        {
            switch (nvi.Tag)
            {
                case "Home":
                    vm?.NavigateToHomeCommand.Execute(null);
                    break;
                case "Dashboard":
                    vm?.NavigateToDashboardCommand.Execute(null);
                    break;
                case "Clients":
                    vm?.NavigateToClientsCommand.Execute(null);
                    break;
                case "Settings":
                    vm?.NavigateToSettingsCommand.Execute(null);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void MainWindow_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var mw = this.FindControl<Window>("MainWindowControl");
        mw?.Focus();
    }
}

#region FluentAvalonia NavigationView code
public abstract class CategoryBase { }

public class Separator : CategoryBase { }

public class Category : CategoryBase
{
    public string Name { get; set; }
    public string ToolTip { get; set; }
    public Symbol Icon { get; set; }
}

#endregion