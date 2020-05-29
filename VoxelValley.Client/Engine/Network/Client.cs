using System;
using System.Net;
using System.Threading;
using VoxelValley.Common.Diagnostics;
using VoxelValley.Common.Network;


namespace VoxelValley.Client.Engine.Network
{
    public class Client
    {
        public static Client Instance;

        Type type = typeof(Client);
        UDPListener listener;
        IPAddress serverIP;

        public Client(IPAddress serverIP)
        {
            Instance = this;
            this.serverIP = serverIP;
        }

        public void Start()
        {
            Log.Info(type, "Starting networking..");
            UDPSender.PunchHole(serverIP);

            new Thread(new ThreadStart(StartListener)).Start();
        }

        public void Stop()
        {
            Log.Info(type, "Stopping networking..");
            listener.Stop();
        }

        void StartListener()
        {
            listener = new UDPListener();
            listener.OnMessageReceived += MessageReceived;
            listener.Start();
        }

        void MessageReceived(byte[] data, IPEndPoint endPoint)
        {
            Game.GameManager.Instance.MessageReceived(data);
        }

        public void SendMessage(byte[] data)
        {
            UDPSender.SendMessage(serverIP, data);
        }
    }
}