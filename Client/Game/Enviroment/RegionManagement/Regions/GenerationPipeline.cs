using VoxelValley.Client.Game.Enviroment.BiomeManagement;
using VoxelValley.Common;

namespace VoxelValley.Client.Game.Enviroment.RegionManagement.Regions
{
    public static class GenerationPipeline
    {
        public static void Generate(int worldBaseX, int worldBaseZ, ref ushort[,,] voxels)
        {
            Biome[,] biomes = new Biome[CommonConstants.World.chunkSize.X, CommonConstants.World.chunkSize.Z];
            ushort[,] heights = new ushort[CommonConstants.World.chunkSize.X, CommonConstants.World.chunkSize.Z];

            GetBiomes(worldBaseX, worldBaseZ, ref biomes, ref heights, ref voxels);
            GetHeights(worldBaseX, worldBaseZ, ref biomes, ref heights, ref voxels);
            InterpolateBiomes(worldBaseX, worldBaseZ, ref biomes, ref heights, ref voxels); 
            GenerateTerrainComposition(worldBaseX, worldBaseZ, ref biomes, ref heights, ref voxels);
            GenerateFinishers(worldBaseX, worldBaseZ, ref biomes, ref heights, ref voxels);
        }

        static void GetBiomes(int worldBaseX, int worldBaseZ, ref Biome[,] biomes, ref ushort[,] heights, ref ushort[,,] voxels)
        {
            for (ushort x = 0; x < CommonConstants.World.chunkSize.X; x++)
                for (ushort z = 0; z < CommonConstants.World.chunkSize.Z; z++)
                    biomes[x, z] = RegionManager.GetRegion(worldBaseX + x, worldBaseZ + z).GetBiome(worldBaseX + x, worldBaseZ + z);
        }

        static void GetHeights(int worldBaseX, int worldBaseZ, ref Biome[,] biomes, ref ushort[,] heights, ref ushort[,,] voxels)
        {
            for (ushort x = 0; x < CommonConstants.World.chunkSize.X; x++)
                for (ushort z = 0; z < CommonConstants.World.chunkSize.Z; z++)
                    heights[x, z] = biomes[x, z].GetHeight(worldBaseX + x, worldBaseZ + z);
        }

        static void InterpolateBiomes(int worldBaseX, int worldBaseZ, ref Biome[,] biomes, ref ushort[,] heights, ref ushort[,,] voxels)
        {

        }

        static void GenerateTerrainComposition(int worldBaseX, int worldBaseZ, ref Biome[,] biomes, ref ushort[,] heights, ref ushort[,,] voxels)
        {
            for (ushort x = 0; x < CommonConstants.World.chunkSize.X; x++)
                for (ushort y = 0; y < CommonConstants.World.chunkSize.Y; y++)
                    for (ushort z = 0; z < CommonConstants.World.chunkSize.Z; z++)
                        voxels[x, y, z] = biomes[x, z].GetVoxel(worldBaseX + x, y, worldBaseZ + z, heights[x, z]);
        }

        static void GenerateFinishers(int worldBaseX, int worldBaseZ, ref Biome[,] biomes, ref ushort[,] heights, ref ushort[,,] voxels)
        {
            for (ushort x = 0; x < CommonConstants.World.chunkSize.X; x++)
                for (ushort z = 0; z < CommonConstants.World.chunkSize.Z; z++)
                    biomes[x, z].GetFinishers(worldBaseX + x, worldBaseZ + z, x, z, heights[x, z] + 1, ref voxels);
        }
    }
}