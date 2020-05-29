using System;
using System.Net;
using System.Net.Sockets;
using VoxelValley.Common.Diagnostics;

namespace VoxelValley.Common.Network
{
    public class UDPListener
    {
        Type type = typeof(UDPListener);
        IPEndPoint endPoint;

        bool IsActive = true;

        public event Action<byte[], IPEndPoint> OnMessageReceived;

        public UDPListener()
        {
            endPoint = new IPEndPoint(IPAddress.Any, NetworkConstants.PortListener);
        }

        public void Start()
        {
            Log.Info(type, "Starting Listener..");

            UdpClient listener = new UdpClient(NetworkConstants.PortListener);

            while (IsActive)
            {
                byte[] data = listener.Receive(ref endPoint);

                if (OnMessageReceived != null)
                    OnMessageReceived(data, endPoint);
            }

            listener.Dispose();
        }

        public void Stop()
        {
            IsActive = false;
        }
    }
}