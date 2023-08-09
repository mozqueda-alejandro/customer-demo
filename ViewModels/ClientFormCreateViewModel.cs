using CommunityToolkit.Mvvm.ComponentModel;
using CustomerDemo.Models;

namespace CustomerDemo.ViewModels;

public partial class ClientFormCreateViewModel : ViewModelBase, IClientFormViewModel
{
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

    public Client NewClient => new()
    {
        FirstName = FirstName,
        LastName = LastName,
        Address = Address,
        Email = Email,
        Phone = Phone,
        SourceId = int.Parse(Source)
    };
}