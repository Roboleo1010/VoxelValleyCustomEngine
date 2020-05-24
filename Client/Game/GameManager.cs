using System;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Game.Entities;
using VoxelValley.Client.Game.Enviroment;
using VoxelValley.Client.Game.Enviroment.Structures;

namespace VoxelValley.Client.Game
{
    public static class GameManager
    {
        static Type type = typeof(GameManager);

        public static void Start()
        {
            VoxelManager.LoadVoxels();
            StructureManager.LoadStructures();

            World world = new World("World");
            new Player("Player", world.gameObject, new Vector3(0, 150, 0));
            new DebugCamera("Debug Camera", world.gameObject, new Vector3(0, 150, 0));
        }
    }
}