namespace Pictochat;

public enum ERoom
{
    A,
    B,
    C,
    D
}

public enum ECommandType : byte
{
    Invalid = 0x01,
    
    PingRequest = 0x02,
    PingResponse = 0x03,
    
    EventJoin = 0x04,
    EventLeave = 0x05,
    
    MessageText = 0x06,
    MessageImage = 0x07,
    
    EventRename = 0x08
}