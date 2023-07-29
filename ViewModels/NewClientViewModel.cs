using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CustomerDemo.Models;
using CustomerDemo.Services;

namespace CustomerDemo.ViewModels;

public partial class NewClientViewModel : ViewModelBase
{
    #region Navigation
    [ObservableProperty]
    private INavigationService? _navigationService;

    [RelayCommand]
    private void NavigateToClients() => NavigationService?.NavigateTo<ClientsViewModel>();
    
    [RelayCommand]
    private void NavigateBack() => NavigationService?.NavigateBack();
    #endregion

    #region Constructors
    public NewClientViewModel() { }
    
    public NewClientViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
    }
    #endregion

    [ObservableProperty]
    private string _firstName = string.Empty;
    
    [ObservableProperty]
    private string _lastName = string.Empty;
    
    [ObservableProperty]
    private string _address = string.Empty;
    
    [ObservableProperty]
    private string _email = string.Empty;
    
    [ObservableProperty]
    private string _phone = string.Empty;
    
    [ObservableProperty]
    private string _source = string.Empty;
    
    [RelayCommand]
    private void SaveClient()
    {
        // convert source to int if possible, else set to 0
        var sourceId = int.TryParse(Source, out var source) ? source : 0;
        var client = new Client
        {
            FirstName = FirstName,
            LastName = LastName,
            Address = Address,
            Email = Email,
            Phone = Phone,
            SourceId = sourceId
        };
        db.Add(client);
        db.SaveChanges();
        
        NavigateToClients();
    }

    private CimentalContext db = new();

}