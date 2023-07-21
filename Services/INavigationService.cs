using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CustomerDemo.ViewModels;

namespace CustomerDemo.Services;

public interface INavigationService
{
    string CurrentViewName { get; }
    
    public event PropertyChangedEventHandler PropertyChanged;
    
    ViewModelBase CurrentView { get; }
    
    void NavigateTo<T>() where T : ViewModelBase;
}
