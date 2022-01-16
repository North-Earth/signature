using System.Collections.Concurrent;
using Signature.Service.Models;

namespace Signature.Service;

public class ReadFileConveyor
{
    public readonly ConcurrentQueue<Chunk> ChunkQueue = new ConcurrentQueue<Chunk>();

    private bool IsReadComplited { get; set; }

    public bool IsComplited => (IsReadComplited && ChunkQueue.IsEmpty);

    public bool HasAnyChunk => !ChunkQueue.IsEmpty;

    public int StackCount => ChunkQueue.Count();

    public ReadFileConveyor()
    {
        ChunkQueue.Clear();
        IsReadComplited = false;
    }

    public Chunk? DequeueChunk()
    {
        if (IsComplited)
        {
            return null;
        }

        ChunkQueue.TryDequeue(out Chunk? chunk);

        /* TODO Create Custom Exception */
        return chunk is null ? throw new Exception() : chunk;
    }

    public void PushChunk(Chunk chunk)
    {
        ChunkQueue.Enqueue(chunk);
    }

    public void ReadComplited() => IsReadComplited = true;
}