using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CustomerDemo.Services;

namespace CustomerDemo.ViewModels;

public partial class DashboardViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _title;
    
    [ObservableProperty]
    private INavigationService? _navigationService;
    
    [RelayCommand]
    private void NavigateToClients() => NavigationService?.NavigateTo<ClientsViewModel>();

    public DashboardViewModel() { }
    
    public DashboardViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
        Title = "Binding from DashboardViewModel";
    }
}