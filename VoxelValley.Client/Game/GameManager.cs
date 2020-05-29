using System;
using System.Net;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Game.Entities;
using VoxelValley.Client.Game.Enviroment;
using VoxelValley.Common.Diagnostics;
using VoxelValley.Common.Enviroment;
using VoxelValley.Common.Enviroment.Structures;
using VoxelValley.Common.Network.PacketManagement;
using VoxelValley.Common.Network.PacketManagement.Packets;

namespace VoxelValley.Client.Game
{
    public class GameManager
    {
        public static GameManager Instance;
        Type type = typeof(GameManager);
        Engine.Network.Client client;

        public GameManager()
        {
            Instance = this;

            StartNetwork();
            LoadData();
            CreateWorld();
        }

        void StartNetwork()
        {
            client = new Engine.Network.Client(IPAddress.Parse("161.35.210.170"));
            client.Start();

            client.SendMessage(new Debug("Hello Server").Serialize());
        }

        void LoadData()
        {
            VoxelManager.LoadVoxels();
            StructureManager.LoadStructures();
        }

        void CreateWorld()
        {
            World world = new World("World");
            new Player("Player", world.gameObject, new Vector3(0, 150, 0));
            new DebugCamera("Debug Camera", world.gameObject, new Vector3(0, 150, 0));
        }

        public void MessageReceived(byte[] data)
        {
            Packet packet = PacketManager.ConvertToPacket(data);

            Log.Info(type, $"Got {packet.Type}");

            switch (packet.Type)
            {
                case PacketManager.PacketType.DEBUG_TEXT:
                    Log.Info(type, ((Common.Network.PacketManagement.Packets.Debug)packet.Deserialize(data)).Text);
                    break;
                default:
                    break;
            }
        }
    }
}