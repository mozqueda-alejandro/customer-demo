using System;
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
    
    private void DeleteAllClients()
    {
        db.RemoveRange(db.Clients);
        db.SaveChanges();
    }

    private void GenerateRandomClient()
    {
        var client = new Client
        {
            FirstName = GetRandomFirstName(),
            LastName = GetRandomLastName(),
            Address = GetRandomAddress(),
            Email = GetRandomEmail(FirstName, LastName),
            Phone = GetRandomPhone(),
            SourceId = GetRandomSource()
        };
        db.Add(client);
        db.SaveChanges();
    }


    private string[] firstNames = { "John", "Emma", "Michael", "Olivia", "William", "Ava", "James", "Sophia", "Benjamin", "Isabella", "Alexander", "Mia" };
    private string[] lastNames = { "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Anderson", "Thomas", "Jackson", "White" };
    private string[] streetNames = { "Main St", "First St", "Second St", "Third St", "Fourth St", "Fifth St", "Sixth St", "Seventh St", "Eighth St", "Ninth St", "Tenth St", "Elm St", "Oak St", "Maple St", "Cedar St", 
        "Pine St", "Birch St", "Spruce St", "Cherry St", "Willow St" 
    };
    private string[] emailProviders = { "gmail.com", "yahoo.com", "hotmail.com", "outlook.com", "aol.com" };
    

    // Helper method to get a random first name
    private string GetRandomFirstName()
    {
        Random rand = new Random();
        return firstNames[rand.Next(0, firstNames.Length)];
    }

    // Helper method to get a random last name
    private string GetRandomLastName()
    {
        Random rand = new Random();
        return lastNames[rand.Next(0, lastNames.Length)];
    }
    
    private string GetRandomAddress()
    {
        Random rand = new Random();
        return $"{rand.Next(1, 1000)} {streetNames[rand.Next(0, streetNames.Length)]}";
    }
    
    private string GetRandomEmail(string firstName, string lastName)
    {
        Random rand = new Random();
        return $"{firstName}.{lastName}{rand.Next(1, 100)}@{emailProviders[rand.Next(0, emailProviders.Length)]}";
    }
    
    private string GetRandomPhone()
    {
        Random rand = new Random();
        return $"({rand.Next(100, 1000)}) {rand.Next(100, 1000)}-{rand.Next(1000, 10000)}";
    }
    
    private int GetRandomSource()
    {
        Random rand = new Random();
        return rand.Next(1, 5);
    }
    

    private CimentalContext db = new();

}