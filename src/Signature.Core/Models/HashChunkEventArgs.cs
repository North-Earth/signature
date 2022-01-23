namespace Signature.Core.Models;

public class HashChunkEventArgs : EventArgs
{
    public int ChunkId { get; set; }

    public string HashValue { get; set; }

    public HashChunkEventArgs(int chunkId, string hashValue)
    {
        ChunkId = chunkId;
        HashValue = hashValue;
    }
}