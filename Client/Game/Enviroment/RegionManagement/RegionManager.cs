using OpenToolkit.Mathematics;
using VoxelValley.Client.Game.Enviroment.BiomeManagement;
using VoxelValley.Client.Game.Enviroment.Generation;
using VoxelValley.Common;

namespace VoxelValley.Client.Game.Enviroment.RegionManagement
{
    public static class RegionManager
    {
        public static ushort[,,] GetChunk(int worldBaseX, int worldBaseZ)
        {
            Biome[,] biomes = new Biome[CommonConstants.World.chunkSize.X, CommonConstants.World.chunkSize.Z];
            ushort[,] heights = new ushort[CommonConstants.World.chunkSize.X, CommonConstants.World.chunkSize.Z];
            ushort[,,] voxels = new ushort[CommonConstants.World.chunkSize.X, CommonConstants.World.chunkSize.Y, CommonConstants.World.chunkSize.Z];

            for (ushort x = 0; x < CommonConstants.World.chunkSize.X; x++)
                for (ushort z = 0; z < CommonConstants.World.chunkSize.Z; z++)
                {
                    Vector2i posInWorld = new Vector2i(worldBaseX + x, worldBaseZ + z);

                    Region region = GetRegion(posInWorld);

                    Biome biome = region.SetBiomeType(posInWorld);
                    ushort height = region.SetBiomeHeights(posInWorld, biome);
                    ushort[] voxelColumn = region.GenerateTerrainComposition(posInWorld, biome, height);

                    for (ushort y = 0; y < CommonConstants.World.chunkSize.Y; y++)
                        voxels[x, y, z] = voxelColumn[y];

                    biomes[x, z] = biome;
                    heights[x, z] = height;
                }

            //Generate finishers
            for (ushort x = 0; x < CommonConstants.World.chunkSize.X; x++)
                for (ushort z = 0; z < CommonConstants.World.chunkSize.Z; z++)
                    biomes[x, z].GetFinishers(worldBaseX + x, worldBaseZ + z, x, z, heights[x, z] + 1, ref voxels);

            return voxels;
        }

        static Region GetRegion(Vector2i posInWorld)
        {
            if (GenerationUtilities.Cellular(posInWorld.X, posInWorld.Y, 0.6f, 1.4f) > 0.5f)
                return RegionReferences.Greenlands;
            else
                return RegionReferences.Desert;
        }
    }
}