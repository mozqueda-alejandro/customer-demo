using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using CustomerDemo.Services;
using CustomerDemo.ViewModels;
using LiveChartsCore.Kernel.Helpers;

namespace CustomerDemo.Services;

public enum DialogFileFilter
{
    Csv,
    Xlsx
}

public class DialogService : IDialogService
{
    public DialogService(Func<Type, ViewModelBase> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
        
    }

    public DialogService()
    {
        
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

    public async Task<string> OpenFileDialog(Visual visual, DialogFileFilter fileFilter)
    {
        var topLevel = TopLevel.GetTopLevel(visual) ?? throw new NullReferenceException("Invalid Owner");
        var fileFilterName = fileFilter.ToString().ToUpper();
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = $"Open {fileFilterName} File",
            AllowMultiple = false, 
            FileTypeFilter = new FilePickerFileType[]
            {
                new($"{fileFilterName} Files (*.{fileFilterName.ToLower()})")
                {
                    Patterns = new[] { $"*.{fileFilterName.ToLower()}" }
                }
            }
                
        });

        return files.Count == 0 ? string.Empty : files[0].Path.AbsolutePath;
    }
    
    
    private readonly Func<Type, ViewModelBase> _viewModelFactory;
}