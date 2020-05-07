
using System.Drawing;

namespace VoxelValley.Client.Game.Enviroment.Generation.Biomes
{
    public class Mountains : Biome
    {
        public override string Name { get => "Mountains"; }
        public override Color Color { get => Color.Gray; }

        public override int GetHeight(int x, int z)
        {
            return GenerationUtilities.MapToWorld(GenerationUtilities.FBMPerlin(x, z, 5, 1, 0.8f));
        }

        internal override Voxel GetVoxel(int x, int y, int z, int height)
        {
            if (y > height)
                return null;
            else
                return VoxelManager.GetVoxel("stone");
        }
    }
}