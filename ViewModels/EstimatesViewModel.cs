using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CustomerDemo.Services;

namespace CustomerDemo.ViewModels;

public partial class EstimatesViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _title = "Binding from EstimatesViewModel";
    
    [ObservableProperty]
    private INavigationService _navigationService;
    
    [RelayCommand]
    private void NavigateToHome() => NavigationService.NavigateTo<HomeViewModel>();
    
    [RelayCommand]
    private void NavigateToClients() => NavigationService.NavigateTo<ClientsViewModel>();
    
    [RelayCommand]
    private void NavigateToVendors() => NavigationService.NavigateTo<VendorsViewModel>();

    public EstimatesViewModel() { }
    
    public EstimatesViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
    }
}