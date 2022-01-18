using System.Text;
using Signature.Service.Models;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Signature.Service;

public class Signature
{
    public event EventHandler<HashChunkEventArgs>? HashedChunkHandler;

    public void Run(string path, int chunkSize, int maxBufferSize = 512)
    {
        try
        {
            var launchConfiguration = Analyzer.GetLaunchConfiguration(path, chunkSize, maxBufferSize);

            using (var conveyor = new ReadFileConveyor(launchConfiguration.ChunkBufferSize))
            {
                using (var threadPool = new ThreadPool(launchConfiguration, conveyor))
                {
                    Hasher.HashedChunkHandler += OnHashedChunk;
                    threadPool.Threads.ToList().ForEach(thread => thread?.Start());

                    WaitHandle.WaitAll(threadPool.ResetEvents);
                }
            }

            System.Console.WriteLine("Done!");
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    public void OnHashedChunk(object? sender, HashChunkEventArgs e) => HashedChunkHandler?.Invoke(this, e);
}