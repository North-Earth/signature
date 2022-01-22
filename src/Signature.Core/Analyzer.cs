using Signature.Core.Models;

namespace Signature.Core;

internal static class Analyzer
{
    public static LaunchConfiguration GetLaunchConfiguration(string path, int chunkSize, int memoryBufferLimit)
    {
        var fileInfo = GetFileInfo(path);
        var bufferLimitBytes = memoryBufferLimit.ToBytes();

        Validator.CheckChunkSize(chunkSize, fileInfo.Length, bufferLimitBytes);

        var threadCount = GetThreadCount();
        var chunkBufferSize = GetChunkBuffer(chunkSize, bufferLimitBytes);


        return new LaunchConfiguration(fileInfo.FullName, threadCount, chunkSize, chunkBufferSize);
    }

    public static FileInfo GetFileInfo(string path)
    {
        try
        {
            Validator.CheckFileExists(path);

            return new FileInfo(path);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static int GetThreadCount()
        => Environment.ProcessorCount > 2 ? Environment.ProcessorCount : 2;

    public static int GetChunkBuffer(int chunkSize, long bufferLimit)
        => (int)(bufferLimit / chunkSize);

    public static long ToBytes(this int mb)
        => mb * 1024 * 1024;
}