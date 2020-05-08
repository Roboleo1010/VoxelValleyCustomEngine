using System.Drawing;
using VoxelValley.Common;

namespace VoxelValley.Client.Game.Enviroment.BiomeManagement
{
    public abstract class Biome
    {
        public abstract string Name { get; }
        public abstract Color Color { get; }
        public abstract byte BiomeId { get; }
        public abstract byte GetHeight(int x, int z);
        internal abstract Voxel GetVoxel(int x, int y, int z, int height);

        public Voxel[] GetVoxelColumn(int x, int z, int height)
        {
            Voxel[] voxelColumn = new Voxel[CommonConstants.World.chunkSize.Y]; //0 = lowest

            for (int y = 0; y < CommonConstants.World.chunkSize.Y; y++) //generate from Bottom to top
                voxelColumn[y] = GetVoxel(x, y, z, height);

            return voxelColumn;
        }
    }
}