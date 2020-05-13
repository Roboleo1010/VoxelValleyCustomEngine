using System;
using System.Drawing;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Game.Enviroment.Generation;

namespace VoxelValley.Client.Game.Enviroment.BiomeManagement.Biomes.Grasslands
{
    public class Forest : Biome
    {
        public override string Name { get => "Forest"; }
        public override Color Color { get => Color.Olive; }
        public override byte BiomeId { get => 1; }

        Random r = new Random(); //TODO: Noise

        public override ushort GetHeight(int x, int z)
        {
            return GenerationUtilities.MapToWorldByte(GenerationUtilities.FBMPerlin(x, z, 5, 0.3f, 0.1f));
        }

        internal override ushort GetVoxel(int x, int y, int z, ushort height)
        {
            if (y > height)
                return VoxelManager.AirVoxel;
            else if (y == height)
            {
                if (r.NextDouble() > 0.5f)
                    return VoxelManager.GetVoxel("grass").Id;
                else
                    return VoxelManager.GetVoxel("dirt").Id;
            }
            else if (y > height - 4)
                return VoxelManager.GetVoxel("dirt").Id;
            else
                return VoxelManager.GetVoxel("stone").Id;
        }

        internal override void GetFinishers(int worldX, int worldZ, ushort height, ref ushort[] voxels)
        {

        }
    }
}