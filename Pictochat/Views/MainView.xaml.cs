using Pictochat.Pages;
using Pictochat.Services;
using Pictochat.ViewModels;

namespace Pictochat.Views;

public partial class MainView
{
    public static MainView Instance;
    
    public MainView()
    {
        InitializeComponent();
        Instance = this;
        AppService.MainVM = new MainViewModel();
        DataContext = AppService.MainVM;

        AppService.MainVM.ActivePage = new HealthAndSafety();
        PictochatService.Initialize();
    }
}