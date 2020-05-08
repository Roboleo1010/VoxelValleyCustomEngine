using OpenToolkit.Mathematics;
using VoxelValley.Client.Game.Enviroment.Generation;
using VoxelValley.Client.Game.Enviroment.RegionManagement.Regions;
using VoxelValley.Common.Diagnostics;

namespace VoxelValley.Client.Game.Enviroment.RegionManagement
{
    public static class RegionManager
    {
        public static void GenerateRegion(int worldPosX, int worldPosZ)
        {
            VoronoiDiagram voronoi = new VoronoiDiagram(new Vector2i(worldPosX, worldPosZ), new Vector2i(3, 3), 200, 200 / 2);
            voronoi.GenerateDiagram();

            bool[,] regionCover = new bool[voronoi.Diagram.GetLength(0), voronoi.Diagram.GetLength(1)]; //region to generate = 1, otzers = 0

            for (int x = 0; x < voronoi.Diagram.GetLength(0); x++)
                for (int y = 0; y < voronoi.Diagram.GetLength(1); y++)
                    if (voronoi.Diagram[x, y] == voronoi.Regions[1, 1])
                        regionCover[x, y] = true;
                    else
                        regionCover[x, y] = false;

            //Determine Biome type //TODO seeded

            Greenlands region = new Greenlands(voronoi.Seeds[1, 1], regionCover);
            region.Generate();


            TextureGenerator.GenerateTexture(voronoi.GetColors(), "RegionMap");
        }
    }
}