using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CustomerDemo.ViewModels;

namespace CustomerDemo.Services;

public interface INavigationService
{
    ViewModelBase CurrentView { get; }
    string CurrentViewName { get; }
    bool CanNavigateBack { get; }
    public event PropertyChangedEventHandler PropertyChanged;
    void NavigateTo<T>() where T : ViewModelBase;
    void NavigateBack();
}
