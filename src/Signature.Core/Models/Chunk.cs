namespace Signature.Core.Models;

internal class Chunk
{
    public int Id { get; }

    public byte[] Bytes { get; }

    public Chunk(int id, byte[] bytes)
    {
        Id = id;
        Bytes = bytes;
    }

    public override string ToString() => $"ChunkId: {Id}";
}