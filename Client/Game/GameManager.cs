using System;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Game.Entities;
using VoxelValley.Common;
using VoxelValley.Common.Enviroment;

namespace VoxelValley.Client.Game
{
    public static class GameManager
    {
        static Type type = typeof(GameManager);

        public static void Start()
        {
            World world = new World("World");
            ReferencePointer.World = world;
            ReferencePointer.Player = new Player("Player", world.gameObject, new Vector3(0, CommonConstants.World.chunkSize.Y, 0));
        }
    }
}