
using System.Drawing;
using VoxelValley.Client.Game.Enviroment.Generation;

namespace VoxelValley.Client.Game.Enviroment.BiomeManagement.Biomes
{
    public class Mountains : Biome
    {
        public override string Name { get => "Mountains"; }
        public override Color Color { get => Color.Gray; }
        public override byte BiomeId { get => 3; }

        public override short GetHeight(int x, int z)
        {
            return GenerationUtilities.MapToWorldByte(GenerationUtilities.FBMPerlin(x, z, 5, 1, 0.8f));
        }

        internal override int GetVoxel(int x, int y, int z, int height)
        {
            if (y > height)
               return VoxelManager.AirVoxel;
            else
                return VoxelManager.GetVoxel("stone").Id;
        }
    }
}