using System.Drawing;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Game.Enviroment.BiomeManagement;
using VoxelValley.Common;

namespace VoxelValley.Client.Game.Enviroment.RegionManagement
{
    public abstract class Region
    {
        //Must have Properties for every Region Type
        public abstract string Name { get; }
        public abstract Color Color { get; }

        public virtual ushort[] Generate(Vector2i positionInWorldSpace)
        {
            Biome biome = SetBiomeType(positionInWorldSpace);
            ushort height = SetBiomeHeights(positionInWorldSpace, biome);
            ushort[] voxels = GenerateTerrainComposition(positionInWorldSpace, biome, height);

            return voxels;
        }

        #region Generation Pipeline

        protected virtual Biome SetBiomeType(Vector2i positionInWorldSpace)
        {
            return GetBiome(positionInWorldSpace.X, positionInWorldSpace.Y);
        }

        protected virtual ushort SetBiomeHeights(Vector2i positionInWorldSpace, Biome biome)
        {
            return biome.GetHeight(positionInWorldSpace.X, positionInWorldSpace.Y);
        }

        protected virtual ushort[] GenerateTerrainComposition(Vector2i positionInWorldSpace, Biome biome, ushort height)
        {
            ushort[] voxels = new ushort[CommonConstants.World.chunkSize.Y];

            for (int y = 0; y < CommonConstants.World.chunkSize.Y; y++)
                voxels[y] = biome.GetVoxel(positionInWorldSpace.X, y, positionInWorldSpace.Y, height);

            return voxels;
        }

        #endregion

        protected abstract Biome GetBiome(int x, int z);
    }
}