using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using VoxelValley.Common.Diagnostics;
using VoxelValley.Common.Network;

namespace VoxelValley.Server.Network
{
    public class Server
    {
        public static Server Instance;
        public event Action<byte[], ConnectedClient> OnMessageReceived;

        Type type = typeof(Server);
        UDPListener listener;
        Dictionary<IPAddress, ConnectedClient> clients = new Dictionary<IPAddress, ConnectedClient>();

        public Server()
        {
            Instance = this;
        }

        public void Start()
        {
            Log.Info(type, "Starting server..");
            new Thread(new ThreadStart(StartListener)).Start();
        }

        public void Stop()
        {
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

            Log.Info(type, $"Received Message from {client.Id}: {Encoding.ASCII.GetString(data)}");

            if (OnMessageReceived != null)
                OnMessageReceived(data, client);
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

        public void BroadcastMessage(byte[] data)
        {
            foreach (ConnectedClient client in clients.Values)
                UDPSender.SendMessage(client.IPAddress, data);
        }

        public void SendMessage(byte[] data, byte clientId)
        {
            ConnectedClient client = clients.Values.Where(c => c.Id == clientId).FirstOrDefault();

            if (client == null)
                return;

            UDPSender.SendMessage(client.IPAddress, data);
        }
    }
}