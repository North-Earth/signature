using System.Collections.Concurrent;
using Signature.Core.Models;

namespace Signature.Core;

internal class ReadFileConveyor : IDisposable
{
    public ConcurrentQueue<Chunk> ChunkQueue { get; } = new ConcurrentQueue<Chunk>();

    public AutoResetEvent AnyChunkEvent { get; } = new AutoResetEvent(false);

    public AutoResetEvent BufferFreeEvent { get; } = new AutoResetEvent(true);

    public int maxBufferSize { get; }

    public bool IsReadComplited { get; set; }

    public bool IsComplited => (IsReadComplited && ChunkQueue.IsEmpty);

    public ReadFileConveyor(int bussefSize)
    {
        ChunkQueue.Clear();
        maxBufferSize = bussefSize;
        IsReadComplited = false;
    }

    public Chunk? DequeueChunk()
    {
        if (ChunkQueue.IsEmpty && !IsReadComplited)
        {
            AnyChunkEvent.Reset();
            return null;
        }

        ChunkQueue.TryDequeue(out Chunk? chunk);

        /* TODO Create Custom Exception */
        //return chunk is null ? throw new Exception() : chunk;
        BufferFreeEvent.Set();
        return chunk;
    }

    public void EnqueueChunk(Chunk chunk)
    {
        ChunkQueue.Enqueue(chunk);

        if (ChunkQueue.Count >= maxBufferSize)
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