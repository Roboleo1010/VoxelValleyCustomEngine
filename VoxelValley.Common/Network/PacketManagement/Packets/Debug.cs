using System;
using System.IO;
using System.Text;

namespace VoxelValley.Common.Network.PacketManagement.Packets
{
    public class Debug : Packet
    {
        public override PacketManager.PacketType Type => PacketManager.PacketType.DEBUG_TEXT;
        public string Text;

        public Debug() { }

        public Debug(string text)
        {
            Text = text;
        }

        public override Packet Deserialize(byte[] data)
        {
            return new Debug(Encoding.ASCII.GetString(data, 1, data.Length - 1));
        }

        public override byte[] Serialize()
        {
            MemoryStream ms = new MemoryStream();
            
            ms.WriteByte((byte)Type);
            ms.Write(Encoding.ASCII.GetBytes(Text));

            return ms.ToArray();
        }
    }
}