using System.Threading.Tasks;
using Avalonia.Controls;
using CustomerDemo.ViewModels;

namespace CustomerDemo.Services;

public interface IDialogService
{
    void ShowDialog<TViewModelBase>(string message) where TViewModelBase : ViewModelBase;
}