using Signature.Core.Models;

namespace Signature.Core;

internal static class Reader
{
    public static void StartReadProcess(string path, int chunkSize,
        ReadFileConveyor conveyor, AutoResetEvent resetEvent)
    {
        try
        {
            using (FileStream stream = File.OpenRead(path))
            {
                var chunkId = 0;
                var buffer = new byte[chunkSize];

                while (stream.Read(buffer, 0, buffer.Length) > 0)
                {
                    conveyor.BufferFreeEvent.WaitOne();

                    var chunk = new Chunk(chunkId++, buffer.ToArray());
                    conveyor.EnqueueChunk(chunk);
                }

                conveyor.ReadComplited();
            }
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            resetEvent.Set();
        }
    }
}