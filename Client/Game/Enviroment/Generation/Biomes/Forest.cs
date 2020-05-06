using System;
using System.Drawing;

namespace VoxelValley.Client.Game.Enviroment.Generation.Biomes
{
    public class Forest : Biome
    {
        public override string Name { get => "Forest"; }
        public override Color Color { get => Color.Olive; }

        Random r = new Random(); //TODO: Noise

        public override int GetHeight(int x, int z)
        {
            return GenerationUtilities.MapToWorld(GenerationUtilities.FBMPerlin(x, z, 5, 2, 0.1f));
        }

        internal override Voxel GetVoxel(int y, int height)
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