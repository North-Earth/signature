namespace Signature.Service;

internal static class Analyzer
{
    internal static LaunchConfiguration GetLaunchConfiguration(string path, int chunkSize, int bufferMb)
    {
        var fileInfo = GetFileInfo(path);

        Validator.CheckChunkSize(chunkSize, fileInfo.Length);

        var threadCount = GetThreadCount();
        var chunkBufferSize = GetChunkBuffer(chunkSize, bufferMb);


        return new LaunchConfiguration(fileInfo.FullName, threadCount, chunkSize, chunkBufferSize);
    }

    internal static FileInfo GetFileInfo(string path)
    {
        try
        {
            Validator.CheckFileExists(path);

            return new FileInfo(path);
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    internal static int GetThreadCount()
    {
        return Environment.ProcessorCount > 2 ? Environment.ProcessorCount : 2;
    }

    internal static int GetChunkBuffer(int chunkSize, int bufferMb)
    {
        return bufferMb.ToBytes() / chunkSize;
    }

    public static int ToBytes(this int mb)
    {
        return mb * 1024 * 1024;
    }
}