using GenericReader;

namespace Pictochat.IO;

public class BufferReader : GenericBufferReader
{
    public BufferReader(byte[] buffer) : base(buffer) { }
}