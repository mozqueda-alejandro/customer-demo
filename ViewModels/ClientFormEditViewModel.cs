using System;
using System.ComponentModel;
using System.Runtime.InteropServices.JavaScript;
using CommunityToolkit.Mvvm.ComponentModel;
using CustomerDemo.Models;
using CustomerDemo.Validators;
using FluentValidation;

namespace CustomerDemo.ViewModels;

public partial class ClientFormEditViewModel : ViewModelBase, IClientFormViewModel
{
    ClientValidator validator = new();
    
    private bool _isInitialized = false;
    private string _changingProperty = string.Empty;
    
    public ClientFormEditViewModel(Client client)
    {
        EditedClient = client;
        FirstName = client.FirstName;
        LastName = client.LastName;
        Address = client.Address;
        Email = client.Email;
        Phone = client.Phone;
        Source = client.SourceId.ToString();

        _isInitialized = true;
        IsValid = true;
    }

    // [ObservableProperty]
    private string _firstName;
    // partial void OnFirstNameChanging(string? value)
    // {
    //     if (_isInitialized) _changingProperty = FirstName;
    // }
    // partial void OnFirstNameChanged(string? value)
    // {
    //     if (!_isInitialized) return;
    //     Validate();
    //     if (!IsValid || FirstName != string.Empty) FirstName = _changingProperty;
    // }
    
    public string FirstName
    {
        get => _firstName;
        set
        {
            if (value == _firstName) return;
            var oldValue = _firstName;
            _firstName = value;
            if (_isInitialized && !ValidateByProperty(nameof(FirstName)))
            {
                _firstName = oldValue;
                return;
            }
            OnPropertyChanged();
        }
    }

    private bool ValidateByProperty(string propertyName)
    {
        return validator.Validate(EditedClient, options =>
        {
            options.IncludeProperties(propertyName);
        }).IsValid;
    }


    [ObservableProperty]
    private string _lastName;
    partial void OnLastNameChanged(string? value)
    {
        // if (_isInitialized) Validate();
    }
    
    [ObservableProperty]
    private string _address;
    partial void OnAddressChanged(string? value)
    {
        // if (_isInitialized) Validate();
    }
    
    [ObservableProperty]
    private string _email;
    partial void OnEmailChanged(string? value)
    {
        // if (_isInitialized) Validate();
    }
    
    [ObservableProperty]
    private string _phone;
    partial void OnPhoneChanged(string? value)
    {
        // if (_isInitialized) Validate();
    }
    
    [ObservableProperty]
    private string _source;
    partial void OnSourceChanged(string? value)
    {
        // if (_isInitialized) Validate();
    }
    
    [ObservableProperty]
    private bool _isValid;

    private Client _editedClient;
    public Client EditedClient
    {
        get
        {
            _editedClient.FirstName = FirstName;
            _editedClient.LastName = LastName;
            _editedClient.Address = Address;
            _editedClient.Email = Email;
            _editedClient.Phone = Phone;
            // _editedClient.SourceId = int.Parse(Source);
            return _editedClient;
        }
        private set => SetProperty(ref _editedClient, value);
    }
}