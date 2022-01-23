using Signature.Core.Models;
using Signature.Shell.Models;

namespace Signature.Shell;

public static class Sturtup
{
    public static void Run(string[] args)
    {
        try
        {
            if (StartupValidator.IsNeedHelp(args))
            {
                ShowHelp();
                return;
            }

            Hash(StartupValidator.ValidateStartupArgs(args));
        }
        catch (Exception)
        {
            Console.WriteLine("\nInvalid startup arguments. Expected: ");
            ShowHelp();
        }
    }

    private static void Hash(StartupArgs args)
    {
        var signature = new Signature.Core.Signature();

        try
        {
            signature.HashedChunkHandler += ShowHashedChunk;
            signature.Run(args.FilePath, args.ChunkSize, args.MemoryBufferLimit);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            signature.HashedChunkHandler -= ShowHashedChunk;
        }
    }

    private static void ShowHashedChunk(object? sender, HashChunkEventArgs e)
        => Console.WriteLine($"{e.ChunkId} - {e.HashValue}");

    private static void ShowHelp()
    {
        Console.WriteLine("\nUsage: Signature.Shell [path]");
        Console.WriteLine("Usage: Signature.Shell [path] [options]");
        Console.WriteLine("\nOptions:");
        Console.WriteLine($"\t {StartupValidator.ShortHelpArg} | {StartupValidator.HelpArg} \t \t \t Display help.");
        Console.WriteLine($"\t {StartupValidator.ChunkSizeArg} [number] \t \t Chunk size of the source file in bytes for SHA256, default = {StartupValidator.ChunkSizeDefault} bytes.");
        Console.WriteLine($"\t {StartupValidator.MemoryBufferLimitArg} [number] \t Memory buffer limit in megabytes, default = {StartupValidator.MemoryBufferLimitDefault} megabytes.");
    }
}