using Signature.Core.Models;

namespace Signature.Core;

internal static class Hasher
{
    public static event EventHandler<HashChunkEventArgs>? HashedChunkHandler;

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
                    HashedChunkHandler?.Invoke(nameof(StartHashProcess), new HashChunkEventArgs(hash.Id, hash.HexadecimalValue));
                }
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