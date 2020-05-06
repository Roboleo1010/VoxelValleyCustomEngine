using System.Drawing;

namespace VoxelValley.Client.Game.Enviroment.Generation.Biomes
{
    public class Ice : Biome
    {
        public override string Name { get => "Ice"; }
        public override Color Color { get => Color.White; }

        public override int GetHeight(int x, int z)
        {
            return GenerationUtilities.MapToWorld(GenerationUtilities.FBMPerlin(x, z, 5, 2, 0.2f));
        }

        internal override Voxel GetVoxel(int y, int height)
        {
            if (y > height)
                return null;
            else if (y == height)
                return VoxelManager.GetVoxel("snow");
            else if (y > height - 4)
                return VoxelManager.GetVoxel("dirt");
            else
                return VoxelManager.GetVoxel("stone");
        }
    }
}