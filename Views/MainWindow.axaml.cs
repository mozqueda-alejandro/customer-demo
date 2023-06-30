using Avalonia.Controls;
using Avalonia.Input;

namespace CustomerDemo.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        FocusManager.Instance?.Focus(null);
    }
}