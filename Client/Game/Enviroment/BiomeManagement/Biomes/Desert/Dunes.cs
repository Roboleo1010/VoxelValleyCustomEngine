
using System.Drawing;
using VoxelValley.Client.Game.Enviroment.Generation;

namespace VoxelValley.Client.Game.Enviroment.BiomeManagement.Biomes.Desert
{
    public class Dunes : Biome
    {
        public override string Name { get => "Dunes"; }
        public override Color Color { get => Color.Orange; }
        public override byte BiomeId { get => 51; }

        public override short GetHeight(int x, int z)
        {
            return GenerationUtilities.MapToWorldByte(GenerationUtilities.FBMPerlin(x, z, 5, 1.3f, 0.25f));
        }

        internal override ushort GetVoxel(int x, int y, int z, int height)
        {

            if (y > height)
                return VoxelManager.AirVoxel;
            else if (y == height)
                return VoxelManager.GetVoxel("sand").Id;
            else if (y > height - 10)
                return VoxelManager.GetVoxel("sand").Id;
            else
                return VoxelManager.GetVoxel("stone").Id;
        }
    }
}