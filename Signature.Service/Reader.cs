using Signature.Service.Models;

namespace Signature.Service;

public static class Reader
{
    public static void StartReadProcess(string path, int chunkSize,
        ReadFileConveyor conveyor, AutoResetEvent resetEvent, int maxBufferSize)
    {
        try
        {
            using (FileStream stream = File.OpenRead(path))
            {
                var chunkId = 0;
                var buffer = new byte[chunkSize];

                while (stream.Read(buffer, 0, buffer.Length) > 0)
                {
                    conveyor.BufferFreeEvent.WaitOne(500);

                    var chunk = new Chunk(chunkId++, buffer.ToArray());
                    conveyor.EnqueueChunk(chunk);
                }

                conveyor.ReadComplited();
            }
        }
        catch (System.Exception)
        {
            throw;
        }
        finally
        {
            resetEvent.Set();
        }
    }
}