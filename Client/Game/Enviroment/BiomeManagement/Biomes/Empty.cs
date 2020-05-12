using System.Drawing;

namespace VoxelValley.Client.Game.Enviroment.BiomeManagement.Biomes
{
    public class Empty : Biome
    {
        public override string Name => "Empty";

        public override Color Color => Color.White;

        public override byte BiomeId => 0;

        public override short GetHeight(int x, int z)
        {
            return 0;
        }

        internal override int GetVoxel(int x, int y, int z, int height)
        {
           return VoxelManager.AirVoxel;
        }
    }
}