using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MessageBox = Pictochat.Controls.MessageBox;

namespace Pictochat.PageModels;

public partial class ChatroomPageModel : ObservableObject
{
    [ObservableProperty] private string inputText;
    [ObservableProperty] private BitmapImage roomIdentifier;
    [ObservableProperty] private ObservableCollection<MessageBox> messages = new();

    [RelayCommand]
    public void Paste()
    {
        InputText += Clipboard.GetText();
    }
    
    [RelayCommand]
    public void Copy()
    {
        Clipboard.SetText(InputText);
    }
}