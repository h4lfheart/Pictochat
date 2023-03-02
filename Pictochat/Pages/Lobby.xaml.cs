using System;
using System.Windows.Media.Animation;

namespace Pictochat.Pages;

public partial class Lobby
{
    public Lobby()
    {
        InitializeComponent();
        
        var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));
        BeginAnimation(OpacityProperty, fadeIn);
    }
}