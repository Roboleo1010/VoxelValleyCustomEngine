using System;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Engine.SceneGraph.Components;
using VoxelValley.Client.Game.Entities;
using VoxelValley.Client.Game.Enviroment;
using VoxelValley.Common;
using VoxelValley.Common.Graphics;

namespace VoxelValley.Client.Game
{
    public static class GameManager
    {
        static Type type = typeof(GameManager);
        public static Camera ActiveCamera;

        public static void Start()
        {
            TextureGenerator.GenerateTexture(new Vector2i(128, 128), "trol");

            VoxelTypeManager.LoadVoxelTypes();

            World world = new World("World");
            new Player("Player", world.gameObject, new Vector3(0, CommonConstants.World.chunkSize.Y, 0));
        }
    }
}