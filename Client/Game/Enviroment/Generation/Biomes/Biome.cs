using VoxelValley.Common;

namespace VoxelValley.Client.Game.Enviroment.Generation.Biomes
{
    public abstract class Biome
    {
        public abstract string Name { get; }

        public abstract int GetHeight(int x, int z);
        internal abstract Voxel GetVoxel(int y, int height);

        public Voxel[] GetVoxelColumn(int x, int z)
        {
            int height = GetHeight(x, z);
            Voxel[] voxelColumn = new Voxel[CommonConstants.World.chunkSize.Y]; //0 = lowest

            for (int y = 0; y < CommonConstants.World.chunkSize.Y; y++) //generate from Bottom to top
                voxelColumn[y] = GetVoxel(y, height);

            return voxelColumn;
        }
    }
}