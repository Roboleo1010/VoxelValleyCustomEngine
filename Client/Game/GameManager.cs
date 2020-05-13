using System;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Engine.SceneGraph.Components;
using VoxelValley.Client.Game.Entities;
using VoxelValley.Client.Game.Enviroment;
using VoxelValley.Client.Game.Enviroment.BiomeManagement;
using VoxelValley.Common;


namespace VoxelValley.Client.Game
{
    public static class GameManager
    {
        static Type type = typeof(GameManager);
        public static Camera ActiveCamera;

        public static void Start()
        {
            VoxelManager.LoadVoxels();
            BiomeManager.LoadBiomes();

            World world = new World("World");
            new Player("Player", world.gameObject, new Vector3(0, 150, 0));
        }
    }
}