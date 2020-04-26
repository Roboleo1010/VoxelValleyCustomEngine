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
        static Queue<Guid> queuedThreadsLow;
        static Queue<Guid> queuedThreadsNormal;
        static Queue<Guid> queuedThreadsHigh;
        static Queue<Guid> queuedThreadsUrgent;

        public enum ThreadPriority
        {
            LOW,
            NORMAL,
            HIGH,
            URGENT
        }

        static ThreadManager()
        {
            threads = new ConcurrentDictionary<Guid, Thread>();

            queuedThreadsLow = new Queue<Guid>();
            queuedThreadsNormal = new Queue<Guid>();
            queuedThreadsHigh = new Queue<Guid>();
            queuedThreadsUrgent = new Queue<Guid>();
        }

        internal static void OnUpdate(float deltaTime)
        {
            while (currentRunningThreads < ClientConstants.Threading.MaxThreads && (queuedThreadsLow.Count > 0 || queuedThreadsNormal.Count > 0 || queuedThreadsHigh.Count > 0 || queuedThreadsUrgent.Count > 0))
            {
                if (queuedThreadsUrgent.Count > 0)
                    StartThread(queuedThreadsUrgent.Dequeue());
                else if (queuedThreadsHigh.Count > 0)
                    StartThread(queuedThreadsHigh.Dequeue());
                else if (queuedThreadsNormal.Count > 0)
                    StartThread(queuedThreadsNormal.Dequeue());
                else if (queuedThreadsLow.Count > 0)
                    StartThread(queuedThreadsLow.Dequeue());
            }

            if (currentRunningThreads == ClientConstants.Threading.MaxThreads)
                Log.Warn(type, "Reached maximum concurrent Threads. Delaying until threads are finished.");
        }

        static void StartThread(Guid id)
        {
            if (threads.TryGetValue(id, out Thread thread))
            {
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
                case ThreadPriority.LOW:
                    queuedThreadsLow.Enqueue(id);
                    break;
                case ThreadPriority.NORMAL:
                    queuedThreadsNormal.Enqueue(id);
                    break;
                case ThreadPriority.HIGH:
                    queuedThreadsHigh.Enqueue(id);
                    break;
                case ThreadPriority.URGENT:
                    queuedThreadsUrgent.Enqueue(id);
                    break;
            }
        }
    }
}