namespace Signature.Service;

public static class Hasher
{
    public static void StartHashProcess(ReadFileConveyor conveyor, AutoResetEvent resetEvent)
    {
        try
        {
            while (!conveyor.IsComplited)
            {
                conveyor.AnyChunkEvent.WaitOne(500);

                var chunk = conveyor.DequeueChunk();

                if (chunk != null)
                {
                    var hash = HashChunk.GetHash(chunk);
                    System.Console.WriteLine(hash.ToString());
                }
            }
        }
        catch (System.Exception)
        {
            throw;
        }
        finally
        {
            System.Console.WriteLine($"Поток {Thread.CurrentThread.Name} завершает работу.");
            resetEvent.Set();
        }

    }
}