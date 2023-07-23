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
    private void NavigateToEstimates() => NavigationService?.NavigateTo<EstimatesViewModel>();
    
    [RelayCommand]
    private void NavigateToClients() => NavigationService?.NavigateTo<ClientsViewModel>();
    
    [RelayCommand]
    private void NavigateToVendors() => NavigationService?.NavigateTo<VendorsViewModel>();

    public DashboardViewModel() { }
    
    public DashboardViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
        Title = "Binding from DashboardViewModel";
    }
}