using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Messaging;
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
        
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<MainWindow>();

        services.AddSingleton<HomeViewModel>();
        services.AddSingleton<DashboardViewModel>();
        services.AddSingleton<JobsViewModel>();
        services.AddSingleton<EstimatesViewModel>();
        services.AddSingleton<ClientsViewModel>();
        services.AddSingleton<ClientsView>();
        services.AddSingleton<VendorsViewModel>();
        services.AddSingleton<SettingsViewModel>();
        
        // Testing
        services.AddSingleton<ClientFormEditViewModel>();
        services.AddSingleton<IDialogService, DialogService>();
        services.AddSingleton<ClientsView>();
        
        services.AddSingleton<CimentalContext>();
        
        services.AddSingleton<IMessenger, WeakReferenceMessenger>();
        services.AddSingleton<INavigationService, NavigationService>();
        
        services.AddSingleton<Func<Type, ViewModelBase>>(serviceProvider =>
            viewModelType => (ViewModelBase)serviceProvider.GetRequiredService(viewModelType));

        _serviceProvider = services.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        }

        base.OnFrameworkInitializationCompleted();
    }
}