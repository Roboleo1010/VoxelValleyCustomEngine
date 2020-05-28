using System;
using System.Net;
using System.Net.Sockets;
using VoxelValley.Common.Diagnostics;

namespace VoxelValley.Common.Network
{
    public static class UDPSender
    {
        static Type type = typeof(UDPSender);

        public static void SendMessage(IPAddress address, byte[] data)
        {
            try
            {
                UdpClient client = new UdpClient(NetworkConstants.PortSender);

                // Log.Info(type, "Send to: " + new IPEndPoint(address, NetworkConstants.PortListener).ToString());

                client.Connect(new IPEndPoint(address, NetworkConstants.PortListener));
                client.Send(data, data.Length);

                client.Dispose();
            }
            catch (Exception ex)
            {
                Log.Error(type, ex.InnerException, ex);
            }
        }
    }
}