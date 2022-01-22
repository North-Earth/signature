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
        catch (System.Exception)
        {
            System.Console.WriteLine("\nInvalid startup arguments. Expected: ");
            ShowHelp();
        }
    }

    public static void Hash(StartupArgs args)
    {
        var signature = new Signature.Core.Signature();
        try
        {
            signature.HashedChunkHandler += ShowHashedChunk;
            signature.Run(args.FilePath, args.ChunkSize, args.MemoryBufferLimit);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            signature.HashedChunkHandler -= ShowHashedChunk;
        }
    }

    public static void ShowHashedChunk(object? sender, HashChunkEventArgs e)
        => System.Console.WriteLine($"{e.ChunkId} - {e.HashValue}");

    public static void ShowHelp()
    {
        System.Console.WriteLine("\nUsage: dotnet Signature.Shell.dll [path]");
        System.Console.WriteLine("Usage: dotnet Signature.Shell.dll [path] [options]");
        System.Console.WriteLine("\nOptions:");
        System.Console.WriteLine($"\t {StartupValidator.ShortHelpArg} | {StartupValidator.HelpArg} \t \t \t Display help.");
        System.Console.WriteLine($"\t {StartupValidator.ChunkSizeArg} [number] \t \t Block chunk of the source file in bytes for SHA256, default = {StartupValidator.ChunkSizeDefault} bytes.");
        System.Console.WriteLine($"\t {StartupValidator.MemoryBufferLimitArg} [number] \t Memory buffer limit in megabytes, default = {StartupValidator.MemoryBufferLimitDefault} megabytes.");
    }
}