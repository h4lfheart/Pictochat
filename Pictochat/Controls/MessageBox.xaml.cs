using System;
using System.Windows;
using System.Windows.Media;
using Pictochat.Models;
using Color = System.Windows.Media.Color;

namespace Pictochat.Controls;

public partial class MessageBox
{
    public MessageBox()
    {
        InitializeComponent();
    }
    
    public MessageBox(PictochatReceiveData args) : this()
    {
        switch (args.Command)
        {
            case ECommandType.MessageText:
                Name.Text = args.Name;
                Message.Text = args.Data.ToString();
                break;
            
            case ECommandType.EventJoin:
                Name.Foreground = new SolidColorBrush(Colors.DodgerBlue);
                Name.Text = $"{args.Name} has joined the chatroom.";
                Message.Visibility = Visibility.Collapsed;
                break;
            
            case ECommandType.EventLeave:
                Name.Foreground = new SolidColorBrush(Colors.OrangeRed);
                Name.Text = $"{args.Name} has left the chatroom.";
                Message.Visibility = Visibility.Collapsed;
                break;
        }
        
    }
    
    public MessageBox(string header, string text) : this()
    {
        Name.Text = header;
        Message.Text = text;
        Name.Foreground = new SolidColorBrush(Colors.SpringGreen);
    }
    
    public static Color ColorFromHSV(double hue, double saturation, double value)
    {
        var hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
        var f = hue / 60 - Math.Floor(hue / 60);

        value *= 255;
        var v = (byte) Convert.ToInt32(value);
        var p = (byte) Convert.ToInt32(value * (1 - saturation));
        var q = (byte) Convert.ToInt32(value * (1 - f * saturation));
        var t = (byte) Convert.ToInt32(value * (1 - (1 - f) * saturation));

        return hi switch
        {
            0 => Color.FromArgb(255, v, t, p),
            1 => Color.FromArgb(255, q, v, p),
            2 => Color.FromArgb(255, p, v, t),
            3 => Color.FromArgb(255, p, q, v),
            4 => Color.FromArgb(255, t, p, v),
            _ => Color.FromArgb(255, v, p, q)
        };
    }
}