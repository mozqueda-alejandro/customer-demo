using System.Collections.Generic;
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
    private object? _selectedCategory;
    partial void OnSelectedCategoryChanged(object? placeholder)
    {
        SetCurrentPage();
    }

    [ObservableProperty]
    private Control? _currentPage;

    public List<CategoryBase> Categories { get; }

    #region NavigationService
    private void NavigateToHome() => NavigationService?.NavigateTo<HomeViewModel>();
    
    [RelayCommand]
    private void NavigateToSettings() => NavigationService?.NavigateTo<SettingsViewModel>();

    [RelayCommand]
    private void NavigateToClients() => NavigationService?.NavigateTo<ClientsViewModel>();
    #endregion

    public MainWindowViewModel() { }
    
    public MainWindowViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
        
        Categories = new List<CategoryBase>
        {
            new Category { Name = "Home", Icon = Symbol.Home, ToolTip = "Home" },
            new Separator(),
            new Category { Name = "Dashboard", Icon = Symbol.ViewAll, ToolTip = "Dashboard" }
        };

        SelectedCategory = Categories[0];
    }
    
    private void SetCurrentPage()
    {
        if (SelectedCategory is Category cat)
        {
            if (cat == Categories[0])
            {
                NavigateToHome();
            }
            else if (cat == Categories[2])
            {
                NavigateToClients();
            }
        }
        else if (SelectedCategory is NavigationViewItem nvi)
        {
            NavigateToSettings();
        }
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
#endregion
