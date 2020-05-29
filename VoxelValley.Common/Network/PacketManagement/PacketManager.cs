using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace VoxelValley.Common.Network.PacketManagement
{
    public static class PacketManager
    {
        public enum PacketType
        {
            CONNECTION_REQUEST,
            CONNECTED,
            IM_STILL_ALIVE,
            DISCONNECTION_REQUEST,
            DISCONECTED,
            DEBUG_TEXT
        }

        public static Dictionary<PacketType, Packet> packets = new Dictionary<PacketType, Packet>();


        static PacketManager()
        {
            Type[] packetTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && t.Namespace == "VoxelValley.Common.Network.PacketManagement.Packets").ToArray();
            Packet[] packetClasses = new Packet[packetTypes.Length];

            for (int i = 0; i < packetTypes.Length; i++)
                packetClasses[i] = (Packet)Activator.CreateInstance(packetTypes[i]);

            foreach (string packetType in Enum.GetNames(typeof(PacketType)))
            {
                Enum.TryParse(typeof(PacketType), packetType, out object type); //FIXME: Besser machen

                packets.Add((PacketType)type, packetClasses.Where(c => c.Type == (PacketType)type).FirstOrDefault());
            }
        }

        public static Packet GetPacket(PacketType type)
        {
            if (packets.TryGetValue(type, out Packet packet))
                return packet;

            return null;
        }

        public static Packet ConvertToPacket(byte[] data)
        {
            Packet packet = GetPacket((PacketType)data[0]).Deserialize(data);
            if (packet != null)
                return packet;

            return new Packets.Debug().Deserialize(data);
        }
    }
}