using System;
using System.Drawing;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Engine.SceneGraph.Components;
using VoxelValley.Client.Game.Entities;
using VoxelValley.Client.Game.Enviroment;
using VoxelValley.Client.Game.Enviroment.Generation;
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
            //   DoTests();

            VoxelManager.LoadVoxels();

            World world = new World("World");
            new Player("Player", world.gameObject, new Vector3(0, CommonConstants.World.chunkSize.Y, 0));
        }

        static void DoTests()
        {
            int size = 2000;

            float[,] heightMapData = new float[size, size];
            Color[,] heatMapData = new Color[size, size];
            Color[,] moistureMapData = new Color[size, size];
            Color[,] biomeMapData = new Color[size, size];

            for (int x = -size / 2; x < size / 2; x++)
                for (int z = -size / 2; z < size / 2; z++)
                {
                    // float height = HeightMap.GetHeight(x, z);
                    // heightMapData[x + size / 2, z + size / 2] = height;

                    //  heatMapData[x + size / 2, z + size / 2] = HeatMap.GetColor(HeatMap.GetHeatType(x, z, 0));
                    //  moistureMapData[x + size / 2, z + size / 2] = MoistureMap.GetColor(MoistureMap.GetMoistureType(x, z, 0));

                    // biomeMapData[x + size / 2, z + size / 2] = BiomeManager.GetBiomeColor(BiomeManager.GetBiome(x, z));

                    heightMapData[x + size / 2, z + size / 2] = GenerationUtilities.FBMCellular(x, z, 1, 0.25f, 1f);
                }

            TextureGenerator.GenerateTexture(heightMapData, "heightmap");
        }
    }
}