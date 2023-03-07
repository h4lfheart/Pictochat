using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pictochat.Pages;

namespace Pictochat.PageModels;

public partial class ChatroomPageModel : ObservableObject
{
    [RelayCommand]
    public void Paste()
    {
        Chatroom.Instance.InputText.Text += Clipboard.GetText();
    }
    
    [RelayCommand]
    public void Copy()
    {
        Clipboard.SetText(Chatroom.Instance.InputText.Text);
    }
}