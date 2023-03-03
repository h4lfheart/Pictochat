using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Pictochat.Models;
using Pictochat.Pages;
using Pictochat.Services;

namespace Pictochat.Controls;

public partial class LobbyButton
{
    public static readonly DependencyProperty RoomProperty = DependencyProperty.Register(nameof(Room), typeof(string), typeof(LobbyButton));

    public string Room
    {
        get => (string)GetValue(RoomProperty);
        set => SetValue(RoomProperty, value);
    }

    private PictochatUser User;
    private ERoom RoomType;
    private bool IsFadingOut;
    
    private BitmapSource RoomIdentifierPlain;
    private BitmapSource RoomIdentifierDown;
    private BitmapSource RoomIdentifierHover;
    
    private readonly BitmapSource LobbyBarPlain = new BitmapImage(new Uri("pack://application:,,,/Resources/RoomButton/LobbyBar.png"));
    private readonly BitmapSource LobbyBarDown = new BitmapImage(new Uri("pack://application:,,,/Resources/RoomButton/LobbyBarDown.png"));
    private readonly BitmapSource LobbyBarHover = new BitmapImage(new Uri("pack://application:,,,/Resources/RoomButton/LobbyBarHover.png"));
    
    private readonly BitmapSource WifiPlain = new BitmapImage(new Uri("pack://application:,,,/Resources/Wifi/Wifi.png"));
    private readonly BitmapSource WifiDown = new BitmapImage(new Uri("pack://application:,,,/Resources/Wifi/WifiDown.png"));
    private readonly BitmapSource WifiHover = new BitmapImage(new Uri("pack://application:,,,/Resources/Wifi/WifiHover.png"));

    private readonly SolidColorBrush PlainColor = new(Color.FromRgb(0x49, 0x49, 0x49));
    private readonly SolidColorBrush DownColor = new(Color.FromRgb(0xFB, 0xFB, 0xFB));
    private readonly SolidColorBrush HoverColor = new(Color.FromRgb(0xBB, 0xBB, 0xBB));

    public LobbyButton()
    {
        InitializeComponent();
        
        Loaded += (sender, args) =>
        {
            RoomText.Text = $"Chat Room {Room}";
            RoomType = Enum.Parse<ERoom>(Room);
            User = PictochatService.Get(RoomType);
            User.Received += (user, data) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (data.Command is ECommandType.EventJoin) RoomConnectedText.Text = User.Peers.Count.ToString();
                });
            };
            
            RoomConnectedText.Text = User.Peers.Count.ToString();
            
            RoomIdentifierPlain = new BitmapImage(new Uri($"pack://application:,,,/Resources/{Room}/Plain.png"));
            RoomIdentifierDown = new BitmapImage(new Uri($"pack://application:,,,/Resources/{Room}/Down.png"));
            RoomIdentifierHover = new BitmapImage(new Uri($"pack://application:,,,/Resources/{Room}/Hover.png"));

            RoomIdentifier.Source = RoomIdentifierPlain;
        };
    }

    // HOVER
    private void OnMouseEnter(object sender, MouseEventArgs e)
    {
        LobbyBar.Source = LobbyBarHover;
        LobbyWifi.Source = WifiHover;
        RoomText.Foreground = HoverColor;
        RoomConnectedText.Foreground = HoverColor;
        RoomIdentifier.Source = RoomIdentifierHover;
    }

    // PLAIN
    private void OnMouseLeave(object sender, MouseEventArgs e)
    {
        LobbyBar.Source = LobbyBarPlain;
        LobbyWifi.Source = WifiPlain;
        RoomText.Foreground = PlainColor;
        RoomConnectedText.Foreground = PlainColor;
        RoomIdentifier.Source = RoomIdentifierPlain;
    }
    
    // DOWN
    private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        LobbyBar.Source = LobbyBarDown;
        LobbyWifi.Source = WifiDown;
        RoomText.Foreground = DownColor;
        RoomConnectedText.Foreground = DownColor;
        RoomIdentifier.Source = RoomIdentifierDown;
    }
    
    // PLAIN
    private void OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
    {
        LobbyBar.Source = LobbyBarPlain;
        LobbyWifi.Source = WifiPlain;
        RoomText.Foreground = PlainColor;
        RoomConnectedText.Foreground = PlainColor;
        RoomIdentifier.Source = RoomIdentifierPlain;
        
        if (IsFadingOut) return;
        IsFadingOut = true;
        
        var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.5));
        fadeOut.Completed += (o, args) =>
        {
            AppService.MainVM.ActivePage = new Chatroom(RoomType);
        };
        
        AppService.MainVM.ActivePage.BeginAnimation(OpacityProperty, fadeOut);
    }
}