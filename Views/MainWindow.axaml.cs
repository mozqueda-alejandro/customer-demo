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

namespace CustomerDemo.Views;

public partial class MainWindow : AppWindow
{
    public MainWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void MainWindow_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        MainWindowControl.Focus();
    }
}