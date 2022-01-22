using Signature.Shell.Models;

namespace Signature.Shell;

public static class StartupValidator
{
    public const int ChunkSizeDefault = 1_048_576;
    public const int MemoryBufferLimitDefault = 512;

    public const string HelpArg = "--help";
    public const string ShortHelpArg = "-h";
    public const string ChunkSizeArg = "--chunkSize";
    public const string MemoryBufferLimitArg = "--bufferLimit";

    public static bool IsNeedHelp(string[] args)
        => args.Contains(HelpArg) || args.Contains(ShortHelpArg) || args.Length < 1;

    public static StartupArgs ValidateStartupArgs(string[] args)
    {
        try
        {
            var chunkSizeArgIdx = Array.FindIndex(args, 0, args.Length, arg => arg == ChunkSizeArg);
            var memoryBufferLimitIdx = Array.FindIndex(args, 0, args.Length, arg => arg == MemoryBufferLimitArg);

            var path = args[0];
            var chunkSize = chunkSizeArgIdx > 0 ? int.Parse(args[chunkSizeArgIdx + 1]) : ChunkSizeDefault;
            var memoryBufferLimit = memoryBufferLimitIdx > 0 ? int.Parse(args[memoryBufferLimitIdx + 1]) : MemoryBufferLimitDefault;

            return new StartupArgs(path, chunkSize, memoryBufferLimit);
        }
        catch (Exception)
        {
            throw;
        }
    }
}