using System.Drawing;
using VoxelValley.Common;

namespace VoxelValley.Client.Game.Enviroment.BiomeManagement
{
    public abstract class Biome
    {
        public abstract string Name { get; }
        public abstract Color Color { get; }
        public abstract byte BiomeId { get; }
        public abstract short GetHeight(int x, int z);
        internal abstract ushort GetVoxel(int x, int y, int z, int height);

    }
}