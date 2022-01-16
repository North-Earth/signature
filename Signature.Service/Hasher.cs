namespace Signature.Service;

public static class Hasher
{
    public static void StartHashProcess(ReadFileConveyor conveyor, AutoResetEvent resetEvent)
    {
        try
        {
            while (!conveyor.IsComplited)
            {
                if (conveyor.HasAnyChunk)
                {
                    var chunk = conveyor.DequeueChunk();

                    if (chunk != null)
                    {
                        var hash = HashChunk.GetHash(chunk);
                        System.Console.WriteLine(hash.ToString());
                    }
                }
                else
                {
                    System.Console.WriteLine($"Поток {Thread.CurrentThread.Name} в режиме ожидания.");
                    Thread.Sleep(250);
                }
            }
        }
        catch (System.Exception ex)
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