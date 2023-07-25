using Avalonia;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CustomerDemo.Services;

namespace CustomerDemo.ViewModels;

public partial class SettingsViewModel : ViewModelBase
{
    public ThemeVariant[] AppThemes { get; } =
        new[] { ThemeVariant.Light, ThemeVariant.Dark};

    [ObservableProperty]
    private ThemeVariant _currentAppTheme = ThemeVariant.Dark;
    
    partial void OnCurrentAppThemeChanged(ThemeVariant? value)
    {
        if (Application.Current!.ActualThemeVariant != value)
        {
            Application.Current.RequestedThemeVariant = value;
        }
    }

    
    public SettingsViewModel()
    {
        
    }
}