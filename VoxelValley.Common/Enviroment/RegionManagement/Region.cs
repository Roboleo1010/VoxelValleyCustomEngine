using System.Drawing;
using VoxelValley.Common.Enviroment.BiomeManagement;

namespace VoxelValley.Common.Enviroment.RegionManagement
{
    public abstract class Region
    {
        public abstract string Name { get; }
        public abstract Color Color { get; }

        public abstract Biome GetBiome(int x, int z);
    }
}