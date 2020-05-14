using System;
using System.Drawing;
using VoxelValley.Client.Game.Enviroment.Generation;

namespace VoxelValley.Client.Game.Enviroment.BiomeManagement.Biomes.Desert
{
    public class FlatDesert : Biome
    {
        public override string Name { get => "Flat Desert"; }
        public override Color Color { get => Color.Yellow; }
        public override byte BiomeId { get => 52; }

        public override ushort GetHeight(int x, int z)
        {
            return GenerationUtilities.MapToWorldByte(GenerationUtilities.FBMPerlin(x, z, 5, 0.4f, 0.15f));
        }

        internal override ushort GetVoxel(int x, int y, int z, ushort height)
        {
            if (y > height)
                return VoxelManager.AirVoxel;
            else if (y == height)
                return VoxelManager.GetVoxel("sand").Id;
            else if (y > height - 4)
                return VoxelManager.GetVoxel("sand").Id;
            else
                return VoxelManager.GetVoxel("stone").Id;
        }

        internal override void GetFinishers(int worldX, int worldZ, ushort chunkX, ushort chunkZ, ushort height, ref ushort[,,] voxels)
        {
            if (GenerationUtilities.Random.NextDouble() > 0.998f)
            {
                ushort cactus = VoxelManager.GetVoxel("cactus").Id;

                voxels[chunkX, height + 1, chunkZ] = cactus;
                voxels[chunkX, height + 2, chunkZ] = cactus;
                voxels[chunkX, height + 3, chunkZ] = cactus;
                voxels[chunkX, height + 4, chunkZ] = cactus;
            }
        }
    }
}