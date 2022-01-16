namespace Signature.Service.Models;

public class Chunk
{
    public int Id { get; }

    public byte[] Bytes { get; }

    public Chunk(int id, byte[] bytes)
    {
        Id = id;
        Bytes = bytes;
    }
}