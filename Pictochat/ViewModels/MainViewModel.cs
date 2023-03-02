using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Pictochat.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty] private UserControl activePage;
}