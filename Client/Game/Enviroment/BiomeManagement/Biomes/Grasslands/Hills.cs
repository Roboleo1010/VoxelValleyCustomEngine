
using System.Drawing;
using VoxelValley.Client.Game.Enviroment.Generation;

namespace VoxelValley.Client.Game.Enviroment.BiomeManagement.Biomes
{
    public class Hills : Biome
    {
        public override string Name { get => "Hills"; }
        public override Color Color { get => Color.Green; }

        public override int GetHeight(int x, int z)
        {
            return GenerationUtilities.MapToWorld(GenerationUtilities.FBMPerlin(x, z, 5, 2, 0.4f));
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