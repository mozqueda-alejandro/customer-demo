using CommunityToolkit.Mvvm.ComponentModel;

namespace CustomerDemo.ViewModels;

public partial class DashboardViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _title;
    
    public DashboardViewModel()
    {
        Title = "Binding from DashboardViewModel";
    }
}