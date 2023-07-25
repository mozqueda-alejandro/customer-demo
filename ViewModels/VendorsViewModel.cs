using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace CustomerDemo.ViewModels;

public partial class VendorsViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _title = "Binding from VendorsViewModel";
    
    public ISeries[] Series { get; set; } 
        = new ISeries[]
        {
            new LineSeries<double>
            {
                Values = new double[] { 2, 1, 3, 5, 3, 4, 6 },
                Fill = null
            }
        };
}