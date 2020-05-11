using System;
using System.Drawing;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Game.Enviroment.Generation;

namespace VoxelValley.Client.Game.Enviroment.BiomeManagement.Biomes
{
    public class Forest : Biome
    {
        public override string Name { get => "Forest"; }
        public override Color Color { get => Color.Olive; }
        public override byte BiomeId { get => 1; }

        Random r = new Random(); //TODO: Noise

        public override short GetHeight(int x, int z)
        {
            return GenerationUtilities.MapToWorldByte(GenerationUtilities.FBMPerlin(x, z, 5, 0.3f, 0.1f));
        }

        internal override Voxel GetVoxel(int x, int y, int z, int height)
        {
            if (y > height)
                return null;
            else if (y == height)
            {
                if (r.NextDouble() > 0.5f)
                    return VoxelManager.GetVoxel("grass");
                else
                    return VoxelManager.GetVoxel("dirt");
            }
            else if (y > height - 4)
                return VoxelManager.GetVoxel("dirt");
            else
                return VoxelManager.GetVoxel("stone");
        }
    }
}