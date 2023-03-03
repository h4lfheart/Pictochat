using System;
using System.Collections.Generic;
using Pictochat.Models;

namespace Pictochat.Services;

public static class PictochatService
{
    private static Dictionary<ERoom, PictochatUser> Rooms = new()
    {
        {ERoom.A, new PictochatUser(Globals.PORT_A)},
        {ERoom.B, new PictochatUser(Globals.PORT_B)},
        {ERoom.C, new PictochatUser(Globals.PORT_C)},
        {ERoom.D, new PictochatUser(Globals.PORT_D)},
    };

    public static PictochatUser Get(ERoom room) => Rooms[room];

    public static void Initialize()
    {
        foreach (var (room, user) in Rooms)
        {
            user.Received += (sender, args) =>
            {
                Console.WriteLine($"{args.Name} [{room.ToString()}]: {args.Command}");
                switch (args.Command)
                {
                    case ECommandType.PingRequest:
                    {
                        if (!sender.InRoom) break;
                        
                        var miniUser = new PictochatUser(sender.Port, args.IP);
                        miniUser.Send(ECommandType.PingResponse);
                        miniUser.Dispose();
                        break;
                    }
                    case ECommandType.PingResponse:
                    {
                        sender.Peers.Add(args.Name);
                        break;
                    }
                    case ECommandType.EventJoin:
                    {
                        sender.Peers.Add(args.Name);
                        break;
                    }
                    case ECommandType.EventLeave:
                    {
                        sender.Peers.Remove(args.Name);
                        break;
                    }
                }
            };
            
            user.Send(ECommandType.PingRequest);
        }
    }

    public static void Leave()
    {
        foreach (var (_, user) in Rooms)
        {
            if (user.InRoom) user.Leave();
            user.Dispose();
        }
    }
}