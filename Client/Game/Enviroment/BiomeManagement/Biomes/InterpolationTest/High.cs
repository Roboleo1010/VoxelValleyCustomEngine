using System.Drawing;

namespace VoxelValley.Client.Game.Enviroment.BiomeManagement.Biomes
{
    public class High : Biome
    {
        public override string Name => "High";

        public override Color Color => Color.Red;

        public override byte BiomeId => 11;

        public override short GetHeight(int x, int z)
        {
            return 20;
        }

        internal override ushort GetVoxel(int x, int y, int z, int height)
        {
            if (y < height)
                return VoxelManager.GetVoxel("debug_red").Id;
            else
                return VoxelManager.AirVoxel;
        }

        internal override void GetFinishers(int worldX, int worldZ, int height, ref ushort[] voxels)
        {

        }
    }
}