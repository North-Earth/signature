using Signature.Core.Models;

namespace Signature.Core;

public class Signature
{
    /// <summary>
    /// Return the number of the part and the hash as it is executed.
    /// </summary>
    public event EventHandler<HashChunkEventArgs>? HashedChunkHandler;

    /// <summary>
    /// Splits the file into parts and calculates their hash using SHA 256.
    /// Returns the result as it is executed via an event HashedChunkHandler.
    /// </summary>
    /// <param name="path">Source file path.</param>
    /// <param name="chunkSize">Chunk size of the source file in bytes for SHA256.</param>
    /// <param name="memoryBufferLimit">Memory buffer limit in megabytes.</param>
    public void Run(string path, int chunkSize, int memoryBufferLimit = 512)
    {
        try
        {
            var launchConfiguration = Analyzer.GetLaunchConfiguration(path, chunkSize, memoryBufferLimit);

            using (var conveyor = new ReadFileConveyor(launchConfiguration.ChunkBufferSize))
            {
                using (var threadPool = new ThreadPool(launchConfiguration, conveyor))
                {
                    Hasher.HashedChunkHandler += OnHashedChunk;
                    threadPool.Threads.ToList().ForEach(thread => thread?.Start());

                    WaitHandle.WaitAll(threadPool.ResetEvents);
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void OnHashedChunk(object? sender, HashChunkEventArgs e) => HashedChunkHandler?.Invoke(this, e);
}