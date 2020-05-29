using System;
using System.Net;
using System.Net.Sockets;
using VoxelValley.Common.Diagnostics;
using VoxelValley.Common.Network.PacketManagement;

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

                client.Connect(new IPEndPoint(address, NetworkConstants.PortListener));
                client.Send(data, data.Length);

                client.Dispose();
            }
            catch (Exception ex)
            {
                Log.Error(type, ex.InnerException, ex);
            }
        }

        public static void PunchHole(IPAddress serverAddress)
        {
            try
            {
                byte[] data = new byte[] { (byte)PacketManager.PacketType.DEBUG_TEXT };

                UdpClient client = new UdpClient(NetworkConstants.PortListener);

                client.Connect(new IPEndPoint(serverAddress, NetworkConstants.PortSender));
                client.Send(data, data.Length);

                client.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}