using System;
using System.Windows;
using Pictochat.Pages;
using Pictochat.Services;
using Pictochat.ViewModels;

namespace Pictochat.Views;

public partial class MainView
{
    public MainView()
    {
        InitializeComponent();
        AppService.MainVM = new MainViewModel();
        DataContext = AppService.MainVM;

        AppService.MainVM.ActivePage = new HealthAndSafety();
    }
}