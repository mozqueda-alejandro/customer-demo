using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CustomerDemo.Services;
using CustomerDemo.ViewModels;
using CustomerDemo.Views;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerDemo;

public partial class App : Application
{
    private ServiceProvider? _serviceProvider;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        var services = new ServiceCollection();
        
        services.AddSingleton<Func<Type, ViewModelBase>>(serviceProvider =>
            viewModelType => (ViewModelBase)serviceProvider.GetRequiredService(viewModelType));

        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<MainWindow>();
        
        services.AddSingleton<HomeViewModel>();
        services.AddSingleton<DashboardViewModel>();
        services.AddSingleton<EstimatesViewModel>();
        services.AddSingleton<ClientsViewModel>();
        services.AddSingleton<NewClientViewModel>();
        services.AddSingleton<VendorsViewModel>();
        services.AddSingleton<SettingsViewModel>();
        services.AddSingleton<INavigationService, NavigationService>();

        services.AddSingleton<IMyDependency, MyDependency>();
        _serviceProvider = services.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            await Task.Delay(1);
            ((MainWindow)desktop.MainWindow).Title = "Customer Demo";
            ((MainWindow)desktop.MainWindow).SetMenuItemExpansion(false);
        }

        base.OnFrameworkInitializationCompleted();
    }
}