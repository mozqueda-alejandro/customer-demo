using CommunityToolkit.Mvvm.ComponentModel;

namespace CustomerDemo.Models;

[INotifyPropertyChanged]
public partial class Client
{
    public void foo()
    {
        var x = 5;
    }
}