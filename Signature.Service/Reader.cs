using Signature.Service.Models;

namespace Signature.Service;

public static class Reader
{
    public static void StartReadProcess(string path, int chunkSize,
        ReadFileConveyor conveyor, AutoResetEvent resetEvent, int maxBufferSize = 200)
    {
        try
        {
            using (FileStream stream = File.OpenRead(path))
            {
                var chunkId = 0;
                var buffer = new byte[chunkSize];

                while (stream.Read(buffer, 0, buffer.Length) > 0)
                {
                    if (conveyor.StackCount < maxBufferSize)
                    {
                        var chunk = new Chunk(chunkId++, buffer.ToArray());
                        conveyor.PushChunk(chunk);
                    }
                    else
                    {
                        System.Console.WriteLine($"Поток {Thread.CurrentThread.Name} в режиме ожидания.");

                        Thread.Sleep(250);
                    }
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