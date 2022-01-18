namespace Signature.Service;

internal static class Validator
{
    internal static void RunValidation(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"File not found: {path}");
        }
    }

    internal static void CheckFileExists(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"File not found: {path}");
        }
    }

    internal static void CheckChunkSize(int chunkSize, long fileSize)
    {
        if (chunkSize < 1)
        {
            //TODO Custom Exception.
            throw new Exception("Chunk of the file cannot be 0 or a negative number");
        }

        if (chunkSize > fileSize)
        {
            //TODO Custom Exception.
            throw new Exception("Chunk of the file is larger than the file size");
        }
    }
}