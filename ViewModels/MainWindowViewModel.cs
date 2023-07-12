using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CustomerDemo.Services;
using CustomerDemo.Views;
using FluentAvalonia.UI.Controls;

namespace CustomerDemo.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private INavigationService? _navigationService;

    [ObservableProperty]
    private object _selectedCategory;

    [ObservableProperty]
    private Control _currentPage;

    
    [RelayCommand]
    private void NavigateToHome() => NavigationService?.NavigateTo<HomeViewModel>();
    
    [RelayCommand]
    private void NavigateToSettings() => NavigationService?.NavigateTo<SettingsViewModel>();

    [RelayCommand]
    private void NavigateToClients() => NavigationService?.NavigateTo<ClientsViewModel>();

    public MainWindowViewModel()
    {
    }
    
    public MainWindowViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
        NavigateToHome();
        
        
    }
}

public abstract class CategoryBase { }

public class Separator : CategoryBase { }

public class Category : CategoryBase
{
    public string Name { get; set; }
    public string ToolTip { get; set; }
    public Symbol Icon { get; set; }
}

public class MenuItemTemplateSelector : DataTemplateSelector
{
    [Content]
    public IDataTemplate ItemTemplate { get; set; }

    public IDataTemplate SeparatorTemplate { get; set; }

    protected override IDataTemplate SelectTemplateCore(object item)
    {
        return item is Separator ? SeparatorTemplate : ItemTemplate;
    }
}