using CustomerDemo.ViewModels;

namespace CustomerDemo.Services;

public interface INavigationService
{
    ViewModelBase CurrentView { get; }
    void NavigateTo<T>() where T : ViewModelBase;
}
