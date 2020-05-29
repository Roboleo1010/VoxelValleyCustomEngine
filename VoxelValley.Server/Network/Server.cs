using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using VoxelValley.Common;
using VoxelValley.Common.Diagnostics;
using VoxelValley.Common.Network;
using VoxelValley.Server.Game;

namespace VoxelValley.Server.Network
{
    public class Server
    {
        Type type = typeof(Server);

        public static Server Instance;
        UDPListener listener;
        Dictionary<IPAddress, ConnectedClient> clients = new Dictionary<IPAddress, ConnectedClient>();

        Thread gameThread;
        bool isGameRunning = true;

        public Server()
        {
            Instance = this;
        }

        #region  Start/ Stop
        public void Start()
        {
            new GameManager();
            StartGameLoop();
            StartNetworking();
        }

        void StartNetworking()
        {
            Log.Info(type, "Starting networking..");
            new Thread(new ThreadStart(StartListener)).Start();
        }

        void StartGameLoop()
        {
            Log.Info(type, "Starting Game loop..");

            gameThread = new Thread(new ThreadStart(GameLogicThread));
            gameThread.Start();
        }

        void Stop()
        {
            StopNetworking();
            StopGameLoop();
        }

        void StopNetworking()
        {
            Log.Info(type, "Stopping networking..");
            listener.Stop();
        }

        void StopGameLoop()
        {
            Log.Info(type, "Stopping Game loop..");

            isGameRunning = false;
        }

        #endregion

        #region  Networking
        void StartListener()
        {
            listener = new UDPListener();
            listener.OnMessageReceived += MessageReceived;
            listener.Start();
        }

        void MessageReceived(byte[] data, IPEndPoint endPoint)
        {
            if (!clients.TryGetValue(endPoint.Address, out ConnectedClient client))
                client = AddClient(endPoint);

            GameManager.Instance.MessageReceived(client.Id, data);
        }

        ConnectedClient AddClient(IPEndPoint endPoint)
        {
            ConnectedClient client = new ConnectedClient(endPoint);
            clients.Add(endPoint.Address, client);

            Log.Info(type, $"Added new Client. ID: {client.Id}");

            SendMessage(Encoding.ASCII.GetBytes("Welcome!"), client.Id);
            BroadcastMessage(Encoding.ASCII.GetBytes($"A new Player has joined (ID: {client.Id})"));

            return client;
        }

        void BroadcastMessage(byte[] data)
        {
            foreach (ConnectedClient client in clients.Values)
                UDPSender.SendMessage(client.IPAddress, data);
        }

        void SendMessage(byte[] data, byte clientId)
        {
            ConnectedClient client = clients.Values.Where(c => c.Id == clientId).FirstOrDefault();

            if (client == null)
                return;

            UDPSender.SendMessage(client.IPAddress, data);
        }

        #endregion

        #region  Game Logic Thread
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

        #endregion
    }
}