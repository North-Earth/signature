namespace Signature.Core;

internal static class Validator
{
    public static void RunValidation(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"File not found: {path}");
        }
    }

    public static void CheckFileExists(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"File not found: {path}");
        }
    }

    public static void CheckChunkSize(int chunkSize, long fileSize)
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