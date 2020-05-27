using System.Drawing;
using VoxelValley.Client.Game.Enviroment.BiomeManagement;

namespace VoxelValley.Client.Game.Enviroment.RegionManagement
{
    public abstract class Region
    {
        public abstract string Name { get; }
        public abstract Color Color { get; }

        public abstract Biome GetBiome(int x, int z);
    }
}