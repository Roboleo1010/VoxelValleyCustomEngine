using System.Drawing;

namespace VoxelValley.Client.Game.Enviroment.BiomeManagement.Biomes.InterpolationTest
{
    public class Low : Biome
    {
        public override string Name => "Empty";

        public override Color Color => Color.Blue;

        public override byte BiomeId => 10;

        public override ushort GetHeight(int x, int z)
        {
            return 5;
        }

        internal override ushort GetVoxel(int x, int y, int z, ushort height)
        {
            if (y < height)
                return VoxelManager.GetVoxel("debug_blue").Id;
            else
                return VoxelManager.AirVoxel;
        }

        internal override void GetFinishers(int worldX, int worldZ, ushort chunkX, ushort chunkZ, ushort height, ref ushort[,,] voxels)
        {

        }
    }
}