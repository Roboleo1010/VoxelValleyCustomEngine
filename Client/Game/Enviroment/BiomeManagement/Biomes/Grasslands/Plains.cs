using System.Drawing;
using VoxelValley.Client.Game.Enviroment.Generation;

namespace VoxelValley.Client.Game.Enviroment.BiomeManagement.Biomes
{
    public class Plains : Biome
    {
        public override string Name { get => "Plains"; }
        public override Color Color { get => Color.Lime; }
        public override byte BiomeId { get => 4; }

        public override byte GetHeight(int x, int z)
        {
            return GenerationUtilities.MapToWorldByte(GenerationUtilities.FBMPerlin(x, z, 5, 1, 0.15f));
        }

        internal override Voxel GetVoxel(int x, int y, int z, int height)
        {
            if (y > height)
                return null;
            else if (y == height)
                return VoxelManager.GetVoxel("grass");
            else if (y > height - 4)
                return VoxelManager.GetVoxel("dirt");
            else
                return VoxelManager.GetVoxel("stone");
        }
    }
}