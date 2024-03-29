
using System.Drawing;
using VoxelValley.Common.Enviroment.Generation;

namespace VoxelValley.Common.Enviroment.BiomeManagement.Biomes.Grasslands
{
    public class Mountains : Biome
    {
        public override string Name { get => "Mountains"; }
        public override Color Color { get => Color.Gray; }
        public override byte BiomeId { get => 3; }

        public override ushort GetHeight(int x, int z)
        {
            return GenerationUtilities.MapToWorldByte(GenerationUtilities.FBMPerlin(x, z, 5, 1, 0.8f));
        }

        internal override ushort GetVoxel(int x, int y, int z, ushort height)
        {
            if (y > height)
                return VoxelManager.AirVoxel;
            else
                return VoxelManager.GetVoxel("stone").Id;
        }
    }
}