using System;
using VoxelValley.Common.Diagnostics;
using VoxelValley.Common.Network.PacketManagement;

namespace VoxelValley.Server.Game
{
    public class GameManager
    {
        Type type = typeof(GameManager);
        public static GameManager Instance;

        public GameManager()
        {
            Instance = this;
        }

        public void OnTick()
        {

        }

        public void MessageReceived(byte clientId, byte[] data)
        {
            Packet packet = PacketManager.ConvertToPacket(data);

            Log.Info(type, $"Got {packet.Type} from {clientId} ");

            switch (packet.Type)
            {
                case PacketManager.PacketType.CONNECTED:
                case PacketManager.PacketType.CONNECTION_REQUEST:
                case PacketManager.PacketType.DISCONECTED:
                case PacketManager.PacketType.DISCONNECTION_REQUEST:
                case PacketManager.PacketType.IM_STILL_ALIVE:
                default:
                    break;
            }
        }
    }
}