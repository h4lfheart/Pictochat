using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Pictochat.Extensions;
using Pictochat.Models;
using Pictochat.Services;
using Pictochat.Views;
using MessageBox = Pictochat.Controls.MessageBox;

namespace Pictochat.Pages;

public partial class Chatroom
{
    private PictochatUser User;

    private static readonly ECommandType[] VisibleCommands =
    {
        ECommandType.MessageText,
        ECommandType.MessageImage,
        ECommandType.EventJoin,
        ECommandType.EventLeave
    };
    
    public Chatroom(ERoom roomType)
    {
        InitializeComponent();

        var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));
        BeginAnimation(OpacityProperty, fadeIn);

        User = PictochatService.Get(roomType);
        User.Received += (sender, args) =>
        {
            if (VisibleCommands.Contains(args.Command))
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Messages.Items.Add(new MessageBox(args));
                    MessagesScroll.ScrollToBottom();
                });
            }
        };
        
        User.Join();
        
        RoomIdentifier.Source = new BitmapImage(new Uri($"pack://application:,,,/Resources/{roomType.ToString()}/Room.png"));
        
        MainView.Instance.KeyDown += OnKeyDown;
    }

    private void SendMessage()
    {
        var input = InputText.Text;
        
        if (input.StartsWith("/clear", StringComparison.OrdinalIgnoreCase))
        {
            Messages.Items.Clear();
            Refresh();
            return;
        }
        
        if (input.StartsWith("/connected", StringComparison.OrdinalIgnoreCase))
        {
            var userString = User.Peers.Count == 1 ? "user" : "users";
            Messages.Items.Add(new MessageBox($"There are {User.Peers.Count} {userString} connected.", User.Peers.CommaJoin()));
            Refresh();
            return;
        }
        
        User.Send(ECommandType.MessageText, input);
        Refresh();
    }

    private void Refresh()
    {
        InputText.Text = string.Empty;
        MessagesScroll.ScrollToBottom();
    }
    
    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.Enter:
                SendMessage();
                return;
            case Key.Back:
                if (InputText.Text.Length == 0) break;
                InputText.Text = InputText.Text[..^1];
                return;
            default:
                InputText.Text += Keyboard.GetCharFromKey(e.Key).ToString() ?? string.Empty;
                break;
        }
    }
}