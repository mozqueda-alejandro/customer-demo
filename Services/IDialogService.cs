using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using CustomerDemo.ViewModels;

namespace CustomerDemo.Services;

public interface IDialogService
{
    void ShowDialog<TViewModelBase>(string message) where TViewModelBase : ViewModelBase;
    Task<string> OpenFileDialog(Visual visual, DialogFileFilter fileFilter);
}