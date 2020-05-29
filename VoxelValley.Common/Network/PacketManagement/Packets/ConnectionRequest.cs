namespace VoxelValley.Common.Network.PacketManagement.Packets
{
    public class ConnectionRequest : Packet
    {
        public override PacketManager.PacketType Type { get => PacketManager.PacketType.CONNECTION_REQUEST; }

        public ConnectionRequest()
        {

        }

        public override byte[] Serialize()
        {
            return new byte[] { (byte)Type };
        }

        public override Packet Deserialize(byte[] data)
        {
            return new ConnectionRequest();
        }
    }
}