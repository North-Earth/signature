namespace Signature.Shell.Models;

public class StartupArgs
{
    public string FilePath { get; private set; }

    public int ChunkSize { get; private set; }

    public int MemoryBufferLimit { get; private set; }

    public StartupArgs(string filePath, int chunkSize, int memoryBufferLimit)
    {
        FilePath = filePath;
        ChunkSize = chunkSize;
        MemoryBufferLimit = memoryBufferLimit;
    }
}