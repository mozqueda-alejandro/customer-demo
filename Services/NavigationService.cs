using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CustomerDemo.ViewModels;

namespace CustomerDemo.Services;

public partial class NavigationService : ObservableObject, INavigationService
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentViewName))]
    private ViewModelBase? _currentView;
    public string CurrentViewName => CurrentView!.GetType().Name.Replace("ViewModel", "View");

    private readonly LinkedList<ViewModelBase> _navigationStack = new();

    [ObservableProperty]
    private bool _canNavigateBack;

    public NavigationService(Func<Type, ViewModelBase> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
    }

    public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
    {
        var view = _viewModelFactory.Invoke(typeof(TViewModel));
        CurrentView = view;
        _navigationStack.AddLast(view);
        if (_navigationStack.Count > 5)
        {
            _navigationStack.RemoveFirst();
        }
        CanNavigateBack = _navigationStack.Count > 1;
    }
    
    public void NavigateBack()
    {
        if (_navigationStack.Count <= 1) return;
        
        _navigationStack.RemoveLast();
        CurrentView = _navigationStack.Last!.Value;
        if (_navigationStack.Count <= 1)
        {
            CanNavigateBack = false;
        }
    }

    private readonly Func<Type, ViewModelBase> _viewModelFactory;
}