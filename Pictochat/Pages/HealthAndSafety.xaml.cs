using System;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Pictochat.Services;

namespace Pictochat.Pages;

public partial class HealthAndSafety
{
    private bool IsFadingOut = false;
    public HealthAndSafety()
    {
        InitializeComponent();

        var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(1));
        BeginAnimation(OpacityProperty, fadeIn);
        
        var bounce = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(1))
        {
            RepeatBehavior = RepeatBehavior.Forever,
            AutoReverse = true
        };
        BounceFadeBox.BeginAnimation(OpacityProperty, bounce);
    }

    private void OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (IsFadingOut) return;
        IsFadingOut = true;
        
        var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.5));
        fadeOut.Completed += (o, args) =>
        {
            AppService.MainVM.ActivePage = new Lobby();
        };

        BeginAnimation(OpacityProperty, fadeOut);
    }
}