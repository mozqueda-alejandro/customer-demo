using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CustomerDemo.Views;

public partial class VendorsView : UserControl
{
    public VendorsView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}