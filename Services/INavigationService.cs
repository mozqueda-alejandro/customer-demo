using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CustomerDemo.ViewModels;

namespace CustomerDemo.Services;

public interface INavigationService
{
    string CurrentViewName { get; }
    ViewModelBase CurrentView { get; }
    public event PropertyChangedEventHandler PropertyChanged;
    void NavigateTo<T>() where T : ViewModelBase;
}
