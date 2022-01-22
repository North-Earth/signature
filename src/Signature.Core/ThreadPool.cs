using Signature.Core.Models;

namespace Signature.Core;

internal class ThreadPool : IDisposable
{
    public Thread[] Threads { get; }

    public AutoResetEvent[] ResetEvents { get; }

    public ThreadPool(LaunchConfiguration configuration, ReadFileConveyor conveyor)
    {
        Threads = new Thread[configuration.ThreadCount];
        ResetEvents = new AutoResetEvent[configuration.ThreadCount];

        for (int i = 0; i < configuration.ThreadCount; i++)
        {
            ResetEvents[i] = new AutoResetEvent(false);
            var resetEvent = ResetEvents[i];

            if (i == 0)
            {
                Threads[i] = new Thread(()
                    => Reader.StartReadProcess(configuration.FilePath, configuration.ChunkSize,
                    conveyor, resetEvent, configuration.ChunkBufferSize))
                {
                    Name = $"Reader Thread #{i}",
                };
                continue;
            }

            Threads[i] = new Thread(() => Hasher.StartHashProcess(conveyor, resetEvent))
            {
                Name = $"Hasher Thread #{i}",
            };
        }

    }

    public void Dispose()
    {
        for (int i = 0; i < ResetEvents.Count(); i++)
        {
            ResetEvents[i].Dispose();
        }
    }
}