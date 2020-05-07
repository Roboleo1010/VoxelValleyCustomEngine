using System.Drawing;
using VoxelValley.Client.Game.Enviroment.BiomeManagement;

namespace VoxelValley.Client.Game.Enviroment.RegionManagement.Regions
{
    public class Empty : Region
    {
        public override string Name => "Empty";
        public override Color Color => Color.Yellow;

        public override Biome GetBiome(int x, int z)
        {
            return BiomeReferences.Empty.EmptyBiome;
        }
    }
}