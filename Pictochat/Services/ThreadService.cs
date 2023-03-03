using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pictochat.Services;

public static class ThreadService
{
    private static List<Task> ActiveTasks = new();

    public static void Queue(Action action)
    {
        ActiveTasks.Add(Task.Run(action));
    }
    
    public static void QueueInfinite(Action action)
    {
        ActiveTasks.Add(Task.Run(() =>
        {
            while (true)
            {
                action();
            }
        }));
    }
}