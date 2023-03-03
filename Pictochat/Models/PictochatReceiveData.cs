using System.Net;

namespace Pictochat.Models;

public class PictochatReceiveData
{
    public ECommandType Command;
    public IPAddress IP;
    public string Name;
    public object? Data;

    public PictochatReceiveData(ECommandType command, IPAddress ip, string name, object? data = null)
    {
        Command = command;
        IP = ip;
        Name = name;
        Data = data;
    }

    public T GetData<T>()
    {
        return (T) Data;
    }
}