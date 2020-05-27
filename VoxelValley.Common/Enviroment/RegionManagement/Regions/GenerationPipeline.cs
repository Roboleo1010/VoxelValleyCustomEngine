using VoxelValley.Common.Enviroment.BiomeManagement;
using VoxelValley.Common.Mathematics;

namespace VoxelValley.Common.Enviroment.RegionManagement.Regions
{
    public static class GenerationPipeline
    {
        public static void Generate(int worldBaseX, int worldBaseZ, ref ushort[,,] voxels)
        {
            Biome[,] biomes = new Biome[CommonConstants.World.ChunkSize.X, CommonConstants.World.ChunkSize.Z];
            ushort[,] heights = new ushort[CommonConstants.World.ChunkSize.X, CommonConstants.World.ChunkSize.Z];

            GetBiomes(worldBaseX, worldBaseZ, ref biomes, ref heights, ref voxels);
            GetHeights(worldBaseX, worldBaseZ, ref biomes, ref heights, ref voxels);
            InterpolateBiomes(worldBaseX, worldBaseZ, ref biomes, ref heights, ref voxels);
            GenerateTerrainComposition(worldBaseX, worldBaseZ, ref biomes, ref heights, ref voxels);
            GenerateFinishers(worldBaseX, worldBaseZ, ref biomes, ref heights, ref voxels);
        }

        static void GetBiomes(int worldBaseX, int worldBaseZ, ref Biome[,] biomes, ref ushort[,] heights, ref ushort[,,] voxels)
        {
            for (ushort x = 0; x < CommonConstants.World.ChunkSize.X; x++)
                for (ushort z = 0; z < CommonConstants.World.ChunkSize.Z; z++)
                    biomes[x, z] = RegionManager.GetRegion(worldBaseX + x, worldBaseZ + z).GetBiome(worldBaseX + x, worldBaseZ + z);
        }

        static void GetHeights(int worldBaseX, int worldBaseZ, ref Biome[,] biomes, ref ushort[,] heights, ref ushort[,,] voxels)
        {
            for (ushort x = 0; x < CommonConstants.World.ChunkSize.X; x++)
                for (ushort z = 0; z < CommonConstants.World.ChunkSize.Z; z++)
                    heights[x, z] = biomes[x, z].GetHeight(worldBaseX + x, worldBaseZ + z);
        }

        static void InterpolateBiomes(int worldBaseX, int worldBaseZ, ref Biome[,] biomes, ref ushort[,] heights, ref ushort[,,] voxels)
        {
            byte interpolationLength = 6;
            byte interpolationLengthHalfed = (byte)(interpolationLength / 2);

            for (ushort x = 0; x < CommonConstants.World.ChunkSize.X; x++)
                for (ushort z = 0; z < CommonConstants.World.ChunkSize.Z; z++)
                    if (x - interpolationLengthHalfed > 0 && x + interpolationLengthHalfed < CommonConstants.World.ChunkSize.X &&
                        z - interpolationLengthHalfed > 0 && z + interpolationLengthHalfed < CommonConstants.World.ChunkSize.Z &&
                        (biomes[x, z] != biomes[x - 1, z] || biomes[x, z] != biomes[x, z - 1]))
                    {
                        ushort heightBL = heights[x - interpolationLengthHalfed, z - interpolationLengthHalfed];
                        ushort heightBR = heights[x + interpolationLengthHalfed, z - interpolationLengthHalfed];
                        ushort heightTL = heights[x - interpolationLengthHalfed, z + interpolationLengthHalfed];
                        ushort heightTR = heights[x + interpolationLengthHalfed, z + interpolationLengthHalfed];

                        for (int ix = -interpolationLengthHalfed; ix < interpolationLengthHalfed; ix++)
                        {
                            ushort heightTop = (ushort)MathHelper.InterpolateLinear(heightTL, heightTR, (ix / (float)interpolationLength) + 0.5f);
                            ushort heightBottom = (ushort)MathHelper.InterpolateLinear(heightBL, heightBR, (ix / (float)interpolationLength) + 0.5f);

                            for (int iz = -interpolationLengthHalfed; iz < interpolationLengthHalfed; iz++)
                            {
                                ushort heightCenter = (ushort)MathHelper.InterpolateLinear(heightTop, heightBottom, (iz / (float)interpolationLength) + 0.5f);
                                heights[x + ix, z + iz] = heightCenter;
                            }
                        }
                    }
        }

        static void GenerateTerrainComposition(int worldBaseX, int worldBaseZ, ref Biome[,] biomes, ref ushort[,] heights, ref ushort[,,] voxels)
        {
            for (ushort x = 0; x < CommonConstants.World.ChunkSize.X; x++)
                for (ushort y = 0; y < CommonConstants.World.ChunkSize.Y; y++)
                    for (ushort z = 0; z < CommonConstants.World.ChunkSize.Z; z++)
                        voxels[x, y, z] = biomes[x, z].GetVoxel(worldBaseX + x, y, worldBaseZ + z, heights[x, z]);
        }

        static void GenerateFinishers(int worldBaseX, int worldBaseZ, ref Biome[,] biomes, ref ushort[,] heights, ref ushort[,,] voxels)
        {
            for (ushort x = 0; x < CommonConstants.World.ChunkSize.X; x++)
                for (ushort z = 0; z < CommonConstants.World.ChunkSize.Z; z++)
                    biomes[x, z].GetFinishers(worldBaseX + x, worldBaseZ + z, x, z, heights[x, z] + 1, ref voxels);
        }
    }
}