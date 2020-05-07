
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

        internal override Voxel GetVoxel(int x, int y, int z, int height)
        {
            Voxel structure = GetStructure(x, y, z, height);

            if (structure != null)
                return structure;

            if (y > height)
                return null;
            else if (y == height)
                return VoxelManager.GetVoxel("sand");
            else if (y > height - 4)
                return VoxelManager.GetVoxel("sand");
            else
                return VoxelManager.GetVoxel("stone");
        }

        internal Voxel GetStructure(int x, int y, int z, int height)
        {
            if (y == height + 1 && GenerationUtilities.FBMPerlin(x, z, 20, 2, 2) == 1)
                return VoxelManager.GetVoxel("cactus");

            return null;
        }
    }
}