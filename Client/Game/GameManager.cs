using System;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Engine.SceneGraph.Components;
using VoxelValley.Client.Game.Entities;
using VoxelValley.Client.Game.Enviroment;
using VoxelValley.Common;

namespace VoxelValley.Client.Game
{
    public static class GameManager
    {
        static Type type = typeof(GameManager);
        public static Camera ActiveCamera;

        public static void Start()
        {
            VoxelTypeManager.LoadVoxelTypes();

            World world = new World("World");
            new Player("Player", world.gameObject, new Vector3(0, CommonConstants.World.chunkSize.Y, 0));
        }
    }
}