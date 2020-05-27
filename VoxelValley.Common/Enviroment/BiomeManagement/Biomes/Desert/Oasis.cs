using System.Drawing;
using VoxelValley.Common.Enviroment.Generation;

namespace VoxelValley.Common.Enviroment.BiomeManagement.Biomes.Desert
{
    public class Oasis : Biome
    {
        public override string Name { get => "Oasis"; }
        public override Color Color { get => Color.Blue; }
        public override byte BiomeId { get => 50; }

        public override ushort GetHeight(int x, int z)
        {
            return GenerationUtilities.MapToWorldByte(GenerationUtilities.FBMPerlin(x, z, 5, 0.3f, 0.1f));
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
    }
}