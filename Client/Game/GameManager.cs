using System;
using System.Drawing;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Engine.SceneGraph.Components;
using VoxelValley.Client.Game.Entities;
using VoxelValley.Client.Game.Enviroment;
using VoxelValley.Client.Game.Enviroment.Generation.Maps;
using VoxelValley.Common;
using VoxelValley.Common.Diagnostics;

namespace VoxelValley.Client.Game
{
    public static class GameManager
    {
        static Type type = typeof(GameManager);
        public static Camera ActiveCamera;

        public static void Start()
        {
            // DoTests();

            VoxelTypeManager.LoadVoxelTypes();

            World world = new World("World");
            new Player("Player", world.gameObject, new Vector3(0, CommonConstants.World.chunkSize.Y, 0));
        }

        static void DoTests()
        {
            int size = 1500;

            float[,] heightMapData = new float[size, size];
            Color[,] heatMapData = new Color[size, size];
            Color[,] moistureMapData = new Color[size, size];

            for (int x = -size / 2; x < size / 2; x++)
                for (int z = -size / 2; z < size / 2; z++)
                {
                    float height = HeightMap.GetHeight(x, z);
                    heightMapData[x + size / 2, z + size / 2] = height;

                    heatMapData[x + size / 2, z + size / 2] = HeatMap.GetColor(HeatMap.GetHeatType(x, z, height));
                    moistureMapData[x + size / 2, z + size / 2] = MoistureMap.GetColor(MoistureMap.GetMoistureType(x, z, height));
                }

            TextureGenerator.GenerateTexture(heightMapData, "heightmap");
            TextureGenerator.GenerateTexture(heatMapData, "heatmap");
            TextureGenerator.GenerateTexture(moistureMapData, "moisturemap");
        }
    }
}