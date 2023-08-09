using System;
using CustomerDemo.ViewModels;

namespace CustomerDemo.Services;

public class DialogService : IDialogService
{
    public DialogService(Func<Type, ViewModelBase> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
        
    }
    
    public void ShowDialog<TViewModelBase>(string message) where TViewModelBase : ViewModelBase
    {
        // var viewModel = _viewModelFactory.Invoke(typeof(TViewModelBase));
        // if (sender is not MenuItem menuItem) return;
        // var client = (Client)menuItem.CommandParameter!;
        //
        // var editViewModel = new ClientFormEditViewModel(client);
        // var editDialog = new ContentDialog
        // {
        //     Title = "Edit Client",
        //     PrimaryButtonText = "Cancel",
        //     SecondaryButtonText = "Save changes",
        //     IsSecondaryButtonEnabled = false,
        //     // DefaultButton = ContentDialogButton.Secondary,
        //     Content = new NewClientView
        //     {
        //         DataContext = editViewModel
        //     }
        // };
        //
        // editViewModel.PropertyChanged += (s, f) =>
        // {
        //     if (f.PropertyName == nameof(editViewModel.IsValid))
        //     {
        //         editDialog.IsSecondaryButtonEnabled = editViewModel.IsValid;
        //     }
        // };
        //
        // editDialog.Closed += async (s, e) =>
        // {
        //     if (e.Result == ContentDialogResult.Primary) return;
        //     var clientResult = editViewModel.EditedClient;
        //     _viewModel.EditClientCommand.Execute(clientResult);
        //
        // };
        //
        // _ = await editDialog.ShowAsync();
    }
    
    
    private readonly Func<Type, ViewModelBase> _viewModelFactory;
}