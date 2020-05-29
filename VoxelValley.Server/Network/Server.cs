using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using VoxelValley.Common.Diagnostics;
using VoxelValley.Common.Network;
using VoxelValley.Common.Network.PacketManagement;
using VoxelValley.Common.Network.PacketManagement.Packets;
using VoxelValley.Server.Game;

namespace VoxelValley.Server.Network
{
    public class Server
    {
        Type type = typeof(Server);

        public static Server Instance;
        UDPListener listener;
        Dictionary<IPAddress, ConnectedClient> clients = new Dictionary<IPAddress, ConnectedClient>();

        public Server()
        {
            Instance = this;
        }

        public void Start()
        {
            Log.Info(type, "Starting networking..");
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
            if (!clients.TryGetValue(endPoint.Address, out ConnectedClient client))
                client = AddClient(endPoint);

            GameManager.Instance.MessageReceived(client.Id, data);
        }

        ConnectedClient AddClient(IPEndPoint endPoint)
        {
            ConnectedClient client = new ConnectedClient(endPoint);
            clients.Add(endPoint.Address, client);

            Log.Info(type, $"Added new Client. ID: {client.Id}");

            SendMessage(new Debug("Hello Client").Serialize(), client.Id);
            BroadcastMessage(new Debug("A new Client has joined").Serialize());

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
    }
}