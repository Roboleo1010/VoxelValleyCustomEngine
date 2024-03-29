using System.Drawing;
using VoxelValley.Common.Enviroment.Generation;

namespace VoxelValley.Common.Enviroment.BiomeManagement.Biomes.Grasslands
{
    public class Plains : Biome
    {
        public override string Name { get => "Plains"; }
        public override Color Color { get => Color.Lime; }
        public override byte BiomeId { get => 4; }

        public override ushort GetHeight(int x, int z)
        {
            return GenerationUtilities.MapToWorldByte(GenerationUtilities.FBMPerlin(x, z, 5, 1, 0.15f));
        }

        internal override ushort GetVoxel(int x, int y, int z, ushort height)
        {
            if (y > height)
                return VoxelManager.AirVoxel;
            else if (y == height)
                return VoxelManager.GetVoxel("grass").Id;
            else if (y > height - 4)
                return VoxelManager.GetVoxel("dirt").Id;
            else
                return VoxelManager.GetVoxel("stone").Id;
        }
    }
}