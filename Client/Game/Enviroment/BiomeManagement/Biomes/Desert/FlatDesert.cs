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

        public override short GetHeight(int x, int z)
        {
            return GenerationUtilities.MapToWorldByte(GenerationUtilities.FBMPerlin(x, z, 5, 0.4f, 0.15f));
        }

        internal override ushort GetVoxel(int x, int y, int z, int height)
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

        internal override void GetFinishers(int worldX, int worldZ, int height, ref ushort[] voxels)
        {
            Random r = new Random();

            if (r.NextDouble() > 0.995f)
            {
                ushort cactusId = VoxelManager.GetVoxel("cactus").Id;

                voxels[height + 1] = cactusId;
                voxels[height + 2] = cactusId;
                voxels[height + 3] = cactusId;
                voxels[height + 4] = cactusId;
            }
        }
    }
}