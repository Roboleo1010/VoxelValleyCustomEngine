using System.Drawing;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Game.Enviroment.BiomeManagement;
using VoxelValley.Common;

namespace VoxelValley.Client.Game.Enviroment.RegionManagement
{
    public abstract class Region
    {
        public abstract string Name { get; }
        public abstract Color Color { get; }


        #region Generation Pipeline
        public virtual Biome SetBiomeType(Vector2i posInWolrd)
        {
            return GetBiome(posInWolrd.X, posInWolrd.Y);
        }

        public virtual ushort SetBiomeHeights(Vector2i posInWolrd, Biome biome)
        {
            return biome.GetHeight(posInWolrd.X, posInWolrd.Y);
        }

        public virtual ushort[] GenerateTerrainComposition(Vector2i posInWolrd, Biome biome, ushort height)
        {
            ushort[] voxels = new ushort[CommonConstants.World.chunkSize.Y];

            for (int y = 0; y < CommonConstants.World.chunkSize.Y; y++)
                voxels[y] = biome.GetVoxel(posInWolrd.X, y, posInWolrd.Y, height);

            return voxels;
        }
        #endregion

        protected abstract Biome GetBiome(int x, int z);
    }
}