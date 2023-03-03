using System;
using System.IO;
using System.Text;

namespace Pictochat.IO;

public class BufferWriter : BinaryWriter
{
    private readonly MemoryStream _memoryData;

    public BufferWriter()
    {
        _memoryData = new MemoryStream {Position = 0};
        OutStream = _memoryData;
    }

    public byte[] GetBuffer() => _memoryData.ToArray();

    public long Length => _memoryData.Length;
    public long Position => _memoryData.Position;

    public void WriteArray(byte[] data)
    {
        Write(data.Length);
        Write(data);
    }
    
    public void WriteFString(string data)
    {
        Write(data.Length+1);
        Write(data, data.Length+1);
    }
    
    public void Write(string value, int len)
    {
        var padded = new byte[len];
        var bytes = Encoding.UTF8.GetBytes(value);
        Buffer.BlockCopy(bytes, 0, padded, 0, bytes.Length);
        Write(padded);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _memoryData.Dispose();
    }
}