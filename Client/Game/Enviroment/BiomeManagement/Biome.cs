using System.Drawing;

namespace VoxelValley.Client.Game.Enviroment.BiomeManagement
{
    public abstract class Biome
    {
        public abstract string Name { get; }
        public abstract Color Color { get; }
        public abstract byte BiomeId { get; } //TODO: Namespaced
        public abstract short GetHeight(int x, int z);
        internal abstract ushort GetVoxel(int x, int y, int z, int height);
        internal abstract void GetFinishers(int worldX, int worldZ, int height, ref ushort[] voxels);
    }
}