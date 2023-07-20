using CustomerDemo.ViewModels;

namespace CustomerDemo.Services;

public interface INavigationService
{
    string CurrentViewName { get; }
    ViewModelBase CurrentView { get; }
    void NavigateTo<T>() where T : ViewModelBase;
}
