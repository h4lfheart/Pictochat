using System;

namespace Pictochat;

public static class Globals
{
    public static string UserName = Environment.UserName;
    
    public const int PORT_A = 24280;
    public const int PORT_B = 24281;
    public const int PORT_C = 24282;
    public const int PORT_D = 24283;
}

public delegate void EventHandler<in TSender, in TArgs>(TSender sender, TArgs args);
