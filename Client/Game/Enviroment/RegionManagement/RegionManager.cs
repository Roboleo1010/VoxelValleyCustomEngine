using OpenToolkit.Mathematics;
using VoxelValley.Client.Game.Enviroment.Generation;
using VoxelValley.Client.Game.Enviroment.RegionManagement.Regions;
using VoxelValley.Common.Diagnostics;

namespace VoxelValley.Client.Game.Enviroment.RegionManagement
{
    public static class RegionManager
    {
        static Region activeRegion;

        public static void GenerateRegion(Vector2i worldPos)
        {
            VoronoiDiagram voronoi = new VoronoiDiagram(worldPos, new Vector2i(3, 3), 400, 400 / 2);
            voronoi.GenerateDiagram();

            bool[,] regionCover = new bool[voronoi.Diagram.GetLength(0), voronoi.Diagram.GetLength(1)]; //region to generate = 1, otzers = 0

            for (int x = 0; x < voronoi.Diagram.GetLength(0); x++)
                for (int y = 0; y < voronoi.Diagram.GetLength(1); y++)
                    if (voronoi.Diagram[x, y] == voronoi.Regions[1, 1])
                        regionCover[x, y] = true;
                    else
                        regionCover[x, y] = false;

            //Determine Biome type //TODO seeded

            Vector2i voronoiCenter = voronoi.Seeds[1, 1];
            Vector2i voronoiCenterInWorldSopace = voronoiCenter - worldPos;

            activeRegion = new Greenlands(voronoiCenterInWorldSopace, regionCover);

            TextureGenerator.GenerateTexture(voronoi.GetColors(), "RegionMap");
        }

        public static Region GetRegion(int worldPosX, int worldPosZ)
        {
            return activeRegion; //TODO: Implement
        }
    }
}