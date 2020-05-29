using System;
using System.Threading;
using VoxelValley.Common;
using VoxelValley.Common.Diagnostics;
using VoxelValley.Common.Network.PacketManagement;

namespace VoxelValley.Server.Game
{
    public class GameManager
    {
        Type type = typeof(GameManager);
        public static GameManager Instance;
        Network.Server server;

        Thread gameThread;
        bool isGameRunning = true;

        public GameManager()
        {
            Instance = this;

            Start();
        }

        #region  Start/ Stop

        public void Start()
        {
            StartServer();
            StartGameLoop();
        }

        void StartServer()
        {
            server = new Network.Server();
            server.Start();
        }

        void StartGameLoop()
        {
            Log.Info(type, "Starting Game loop..");

            gameThread = new Thread(new ThreadStart(GameLogicThread));
            gameThread.Start();
        }

        public void Stop()
        {
            StopServer();
            StopGameLoop();
        }

        void StopGameLoop()
        {
            Log.Info(type, "Stopping Game loop..");

            isGameRunning = false;
        }

        void StopServer()
        {
            server.Stop();
        }

        #endregion

        void GameLogicThread()
        {
            Log.Info(type, "Game thread started. Running at " + CommonConstants.Simulation.TicksPerSecond + " ticks per second");

            DateTime nextUpdate = DateTime.Now;

            while (isGameRunning)
            {
                nextUpdate = DateTime.Now.AddMilliseconds(CommonConstants.Simulation.MsPerTick);

                GameManager.Instance.OnTick();

                if (nextUpdate > DateTime.Now)
                    Thread.Sleep(nextUpdate - DateTime.Now);
            }
        }

        public void OnTick()
        {

        }

        public void MessageReceived(byte clientId, byte[] data)
        {
            Packet packet = PacketManager.ConvertToPacket(data);

            Log.Info(type, $"Got {packet.Type} from {clientId} ");

            switch (packet.Type)
            {
                case PacketManager.PacketType.DEBUG_TEXT:
                    Log.Info(type, ((Common.Network.PacketManagement.Packets.Debug)packet.Deserialize(data)).Text);
                    break;
                default:
                    break;
            }
        }
    }
}