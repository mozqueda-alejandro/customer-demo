using CommunityToolkit.Mvvm.ComponentModel;

namespace CustomerDemo.ViewModels;

public partial class VendorsViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _title = "Binding from VendorsViewModel";
}