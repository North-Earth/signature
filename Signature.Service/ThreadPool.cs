using Signature.Service.Models;

namespace Signature.Service;

internal class ThreadPool
{
    internal Thread[] Threads { get; }

    internal AutoResetEvent[] ResetEvents { get; }

    internal ThreadPool(string path, int chunkSize, int processorCount = 3)
    {
        var conveyor = new ReadFileConveyor();
        var threadCount = processorCount < 2 ? 2 : processorCount;

        Threads = new Thread[threadCount];
        ResetEvents = new AutoResetEvent[threadCount];

        for (int i = 0; i < threadCount; i++)
        {
            ResetEvents[i] = new AutoResetEvent(false);
            var resetEvent = ResetEvents[i];

            if (i == 0)
            {
                Threads[i] = new Thread(() => Reader.StartReadProcess(path, chunkSize, conveyor, resetEvent, 200))
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
}