﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Administrationsapplication.MVVM.Core;
using Administrationsapplication.MVVM.ViewModels;
using Administrationsapplication.Services;
using System.Windows;

namespace Administrationsapplication;

public partial class App : Application
{
    private static IHost? AppHost { get; set; }

    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((config, services) =>
            {
                services.AddSingleton<NavigationStore>();
                services.AddSingleton<DateTimeService>();


                services.AddSingleton<MainWindow>();
                services.AddSingleton<HomeViewModel>();
                services.AddSingleton<SettingsViewModel>();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs args)
    {
        var mainWindow = AppHost!.Services.GetRequiredService<MainWindow>();
        var navigationStore = AppHost!.Services.GetRequiredService<NavigationStore>();
        var dateTimeService = AppHost!.Services.GetService<DateTimeService>();

        navigationStore.CurrentViewModel = new HomeViewModel(navigationStore, dateTimeService!);

        await AppHost!.StartAsync();
        mainWindow.Show();
        base.OnStartup(args);
    }
}
