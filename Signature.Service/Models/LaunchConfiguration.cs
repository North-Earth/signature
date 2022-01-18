public class LaunchConfiguration
{
    public string FilePath { get; }

    public int ThreadCount { get; }

    public int ChunkSize { get; }

    public int ChunkBufferSize { get; }

    public LaunchConfiguration(string path, int threadsCount, int chunkSize, int chunkBufferSize)
    {
        FilePath = path;
        ThreadCount = threadsCount;
        ChunkSize = chunkSize;
        ChunkBufferSize = chunkBufferSize;
    }
}