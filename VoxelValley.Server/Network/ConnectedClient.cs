using System.Net;

namespace VoxelValley.Server.Network
{
    public class ConnectedClient
    {
        static byte clientId = 0; //First client = 1
        static byte ClientId { get { clientId++; return clientId; } }

        public IPEndPoint EndPoint { get; private set; }
        public IPAddress IPAddress { get; private set; }
        public byte Id { get; private set; }

        public ConnectedClient(IPEndPoint endPoint)
        {
            EndPoint = endPoint;
            IPAddress = endPoint.Address;
            Id = ClientId;
        }

        public override string ToString()
        {
            return IPAddress.ToString();
        }
    }
}