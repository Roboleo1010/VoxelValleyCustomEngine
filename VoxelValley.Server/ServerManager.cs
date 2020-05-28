using System;
using System.Threading;
using VoxelValley.Common;
using VoxelValley.Common.Diagnostics;

namespace VoxelValley.Server
{
    public static class ServerManager
    {
        static Type type = typeof(ServerManager);
        static Thread serverThread;
        static Network.Server server;

        public static void StartServer()
        {
            server = new Network.Server();
            server.Start();

            serverThread = new Thread(new ThreadStart(GameLogicThread));
            serverThread.Start();
        }

        public static void StopServer()
        {
            server.Stop();
        }

        static void GameLogicThread()
        {
            Log.Info(type, "Game thread started. Running at " + CommonConstants.Simulation.TicksPerSecond + " ticks per second");

            DateTime nextUpdate = DateTime.Now;

            while (true)
            {
                nextUpdate = DateTime.Now.AddMilliseconds(CommonConstants.Simulation.MsPerTick);

                OnTick();

                if (nextUpdate > DateTime.Now)
                    Thread.Sleep(nextUpdate - DateTime.Now);
            }
        }

        static void OnTick()
        {

        }
    }
}