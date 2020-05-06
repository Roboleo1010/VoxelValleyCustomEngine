
using System.Drawing;

namespace VoxelValley.Client.Game.Enviroment.Generation.Biomes
{
    public class Desert : Biome
    {
        public override string Name { get => "Desert"; }
        public override Color Color { get => Color.Yellow; }

        public override int GetHeight(int x, int z)
        {
            return GenerationUtilities.MapToWorld(GenerationUtilities.FBMPerlin(x, z, 5, 0.8f, 0.1f));
        }

        internal override Voxel GetVoxel(int y, int height)
        {
            if (y > height)
                return null;
            else if (y == height)
                return VoxelManager.GetVoxel("sand");
            else if (y > height - 4)
                return VoxelManager.GetVoxel("sand");
            else
                return VoxelManager.GetVoxel("stone");
        }
    }
}