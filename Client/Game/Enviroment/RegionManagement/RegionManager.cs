using OpenToolkit.Mathematics;
using VoxelValley.Client.Game.Enviroment.Generation;
using VoxelValley.Client.Game.Enviroment.RegionManagement.Regions;
using VoxelValley.Common.Diagnostics;

namespace VoxelValley.Client.Game.Enviroment.RegionManagement
{
    public static class RegionManager
    {
        public static bool CurrentlyGenerating = false;

        static Region activeRegion;

        public static void GenerateRegion(Vector2i worldPos)
        {
            CurrentlyGenerating = true;

            // VoronoiDiagram voronoi = new VoronoiDiagram(worldPos, new Vector2i(3, 3), 400, 400 / 2);
            // voronoi.GenerateDiagram();

            // bool[,] regionCover = new bool[voronoi.Diagram.GetLength(0), voronoi.Diagram.GetLength(1)]; //region to generate = 1, otzers = 0

            // int minX = int.MaxValue;
            // int maxX = int.MinValue;
            // int minZ = int.MaxValue;
            // int maxZ = int.MinValue;

            // for (int x = 0; x < voronoi.Diagram.GetLength(0); x++)
            //     for (int z = 0; z < voronoi.Diagram.GetLength(1); z++)
            //         if (voronoi.Diagram[x, z] == voronoi.Regions[1, 1])
            //         {
            //             regionCover[x, z] = true;

            //             if (x < minX)
            //                 minX = x;
            //             if (x > maxX)
            //                 maxX = x;
            //             if (z < minZ)
            //                 minZ = z;
            //             if (z > maxZ)
            //                 maxZ = z;
            //         }
            //         else
            //             regionCover[x, z] = false;

            // bool[,] regionCoverShrinked = new bool[maxX - minX, maxZ - minZ];

            // for (int x = 0; x < maxX - minX; x++)
            //     for (int z = 0; z < maxZ - minZ; z++)
            //         regionCoverShrinked[x, z] = regionCover[x + minX, z + minZ];

            // Vector2i voronoiCenterInWorldSpace = voronoi.Seeds[1, 1] - new Vector2i(minX, minZ) + worldPos;

            //Determine Region type 
            //activeRegion = new Greenlands(voronoiCenterInWorldSpace, regionCoverShrinked);  //TODO seeded/ Heat / Moisture Map

            //TextureGenerator.GenerateTexture(voronoi.GetColors(), "RegionMap");

            int size = 250;

            bool[,] regionCover = new bool[size, size];

            for (int x = 0; x < size; x++)
                for (int z = 0; z < size; z++)
                    regionCover[x, z] = true;


            //Determine Region type 
            activeRegion = new Greenlands(new Vector2i(0, 0), regionCover);  //TODO seeded
            activeRegion.Generate();
            activeRegion.Save();

            CurrentlyGenerating = false;
        }

        public static Region GetRegion(int worldPosX, int worldPosZ)
        {
            if (activeRegion == null)
                GenerateRegion(new Vector2i(worldPosX, worldPosZ));

            // if (activeRegion.IsRegionCoverInLocalSpace(activeRegion.FromWorldToLocalSpaceX(worldPosX),
            //                                            activeRegion.FromWorldToLocalSpaceZ(worldPosZ)))
            return activeRegion;
            // else
            // {
            //     GenerateRegion(new Vector2i(worldPosX, worldPosZ));
            //     return activeRegion;
            // }
        }
    }
}