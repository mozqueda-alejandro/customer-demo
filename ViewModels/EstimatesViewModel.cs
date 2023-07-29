using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CustomerDemo.Services;

namespace CustomerDemo.ViewModels;

public partial class EstimatesViewModel : ViewModelBase
{
    public static List<string> JobScopes = new()
    {
        "Brick",
        "Extra Courses",
        "New Wall",
        "Paint",
        "Precast",
        "Repair",
        "Stone",
        "Stucco",
        "Temporary Fence",
        "Wall on Top"
    };

    [ObservableProperty]
    private string _title = "Binding from EstimatesViewModel";

    [ObservableProperty]
    private INavigationService _navigationService;
    
    [RelayCommand]
    private void NavigateToHome() => NavigationService.NavigateTo<HomeViewModel>();

    public EstimatesViewModel() { }
    
    public EstimatesViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
    }
}