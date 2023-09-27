﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Administrationsapplication.MVVM.ViewModels;
using Administrationsapplication.Services;
using System.Windows;
using System.Net.Http;

namespace Administrationsapplication;

public partial class App : Application
{
    private static IHost? AppHost { get; set; }

    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddTransient<HttpClient>();
                services.AddSingleton<DateAndTimeService>();
                services.AddSingleton<WeatherService>();
                services.AddSingleton<DeviceService>();

                services.AddSingleton<HomeViewModel>();
                services.AddSingleton<SettingsViewModel>();
                services.AddSingleton<MainWindowViewModel>();
                services.AddSingleton<MainWindow>();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        var mainWindow = AppHost!.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();

        base.OnStartup(e);
    }
}