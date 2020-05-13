
using System.Drawing;
using VoxelValley.Client.Game.Enviroment.Generation;

namespace VoxelValley.Client.Game.Enviroment.BiomeManagement.Biomes.Grasslands
{
    public class Hills : Biome
    {
        public override string Name { get => "Hills"; }
        public override Color Color { get => Color.Green; }
        public override byte BiomeId { get => 2; }

        public override ushort GetHeight(int x, int z)
        {
            return GenerationUtilities.MapToWorldByte(GenerationUtilities.FBMPerlin(x, z, 5, 2, 0.4f));
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

        internal override void GetFinishers(int worldX, int worldZ, ushort height, ref ushort[] voxels)
        {

        }
    }
}