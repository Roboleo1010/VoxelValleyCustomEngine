using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using VoxelValley.Client.Engine;
using VoxelValley.Common.Diagnostics;

namespace VoxelValley.Common.Threading
{
    public static class ThreadManager
    {
        static Type type = typeof(ThreadManager);
        static int currentRunningThreads = 0;

        static ConcurrentDictionary<Guid, Thread> threads;
        static Queue<Guid> queuedThreadsLowest;
        static Queue<Guid> queuedThreadsBelowNormal;
        static Queue<Guid> queuedThreadsNormal;
        static Queue<Guid> queuedThreadsAboveNormal;
        static Queue<Guid> queuedThreadsHighest;

        static ThreadManager()
        {
            threads = new ConcurrentDictionary<Guid, Thread>();

            queuedThreadsLowest = new Queue<Guid>();
            queuedThreadsBelowNormal = new Queue<Guid>();
            queuedThreadsNormal = new Queue<Guid>();
            queuedThreadsAboveNormal = new Queue<Guid>();
            queuedThreadsHighest = new Queue<Guid>();
        }

        internal static void OnUpdate(float deltaTime)
        {
            while (currentRunningThreads < ClientConstants.Threading.MaxThreads && (queuedThreadsLowest.Count > 0 || queuedThreadsBelowNormal.Count > 0 || queuedThreadsNormal.Count > 0 || queuedThreadsAboveNormal.Count > 0 || queuedThreadsHighest.Count > 0))
            {
                if (queuedThreadsHighest.Count > 0)
                    StartThread(queuedThreadsHighest.Dequeue(), ThreadPriority.Highest);
                if (queuedThreadsAboveNormal.Count > 0)
                    StartThread(queuedThreadsAboveNormal.Dequeue(), ThreadPriority.AboveNormal);
                else if (queuedThreadsNormal.Count > 0)
                    StartThread(queuedThreadsNormal.Dequeue(), ThreadPriority.Normal);
                else if (queuedThreadsBelowNormal.Count > 0)
                    StartThread(queuedThreadsBelowNormal.Dequeue(), ThreadPriority.BelowNormal);
                else if (queuedThreadsLowest.Count > 0)
                    StartThread(queuedThreadsLowest.Dequeue(), ThreadPriority.Lowest);
            }

            if (currentRunningThreads == ClientConstants.Threading.MaxThreads)
                Log.Warn(type, "Reached maximum concurrent Threads. Delaying until threads are finished.");
        }

        static void StartThread(Guid id, ThreadPriority priority)
        {
            if (threads.TryGetValue(id, out Thread thread))
            {
                thread.Priority = priority;
                thread.Start();
                currentRunningThreads++;
            }
        }

        public static void CreateThread(Action threadWorker, Action callbackMethod, string name, ThreadPriority priority)
        {
            ThreadStart starter = new ThreadStart(threadWorker);
            Guid id = Guid.NewGuid();

            //AddCallback
            starter += () =>
                {
                    callbackMethod();
                    threads.TryRemove(id, out Thread thread);
                    currentRunningThreads--;
                };

            threads.TryAdd(id, new Thread(starter)
            {
                Name = name,
                IsBackground = true
            });

            QueueThread(id, priority);
        }

        static void QueueThread(Guid id, ThreadPriority priority)
        {
            switch (priority)
            {
                case ThreadPriority.Lowest:
                    queuedThreadsLowest.Enqueue(id);
                    break;
                case ThreadPriority.BelowNormal:
                    queuedThreadsBelowNormal.Enqueue(id);
                    break;
                case ThreadPriority.Normal:
                    queuedThreadsNormal.Enqueue(id);
                    break;
                case ThreadPriority.AboveNormal:
                    queuedThreadsAboveNormal.Enqueue(id);
                    break;
                case ThreadPriority.Highest:
                    queuedThreadsHighest.Enqueue(id);
                    break;
            }
        }
    }
}