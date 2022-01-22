using Signature.Core.Models;
using System.Collections.Concurrent;

namespace Signature.Core;

internal class ReadFileConveyor : IDisposable
{
    private int MaxBufferSize { get; }

    private bool IsReadComplited { get; set; }

    public ConcurrentQueue<Chunk> ChunkQueue { get; } = new ConcurrentQueue<Chunk>();

    public AutoResetEvent AnyChunkEvent { get; } = new AutoResetEvent(false);

    public AutoResetEvent BufferFreeEvent { get; } = new AutoResetEvent(true);

    public bool IsComplited => (IsReadComplited && ChunkQueue.IsEmpty);

    public ReadFileConveyor(int bufferSize)
    {
        ChunkQueue.Clear();
        MaxBufferSize = bufferSize;
        IsReadComplited = false;
    }

    public Chunk? DequeueChunk()
    {
        if (ChunkQueue.IsEmpty && !IsReadComplited)
        {
            return null;
        }

        ChunkQueue.TryDequeue(out Chunk? chunk);

        BufferFreeEvent.Set();
        return chunk;
    }

    public void EnqueueChunk(Chunk chunk)
    {
        ChunkQueue.Enqueue(chunk);

        if (ChunkQueue.Count >= MaxBufferSize)
        {
            BufferFreeEvent.Reset();
        }

        AnyChunkEvent.Set();
    }

    public void ReadComplited()
    {
        IsReadComplited = true;
        AnyChunkEvent.Set();
    }

    public void Dispose()
    {
        AnyChunkEvent.Dispose();
        BufferFreeEvent.Dispose();
    }
}