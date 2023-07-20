using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CustomerDemo.ViewModels;

namespace CustomerDemo.Services;

public partial class NavigationService : ObservableObject, INavigationService
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentViewName))]
    private ViewModelBase? _currentView;

    public string CurrentViewName => CurrentView!.GetType().Name;

    private readonly Func<Type, ViewModelBase> _viewModelFactory;

    public NavigationService(Func<Type, ViewModelBase> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
    }

    public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
    {
        ViewModelBase viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
        CurrentView = viewModel;
    }
}