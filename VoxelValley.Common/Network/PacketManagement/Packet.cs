using System;

namespace VoxelValley.Common.Network.PacketManagement
{
    public abstract class Packet
    {
        public abstract PacketManager.PacketType Type { get; }

        public Packet() { } //For PacketManager

        public abstract byte[] Serialize();

        public abstract Packet Deserialize(byte[] data);
    }
}