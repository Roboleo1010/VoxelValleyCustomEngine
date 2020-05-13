using System.Drawing;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Game.Enviroment.BiomeManagement;
using VoxelValley.Common;

namespace VoxelValley.Client.Game.Enviroment.RegionManagement
{
    public abstract class Region
    {
        //Must have Properties for every Reagion Type
        public abstract string Name { get; }
        public abstract Color Color { get; }

        protected Vector2i worldPosition;

        //Region Data
        protected byte biomeId;
        protected short height;
        protected ushort[] voxels;


        public Region(Vector2i positionInWorldSpace)
        {
            this.worldPosition = positionInWorldSpace;
        }

        public virtual void Generate()
        {
            voxels = new ushort[CommonConstants.World.chunkSize.Y];

            SetBiomeType();
            SetBiomeHeights();
            InterpolateBiomes();
            InterpolateRegions();
            GenerateTerrainComposition();
            GenerateFinishers();
            OptimizeRegion();
        }
        #region  abstract Region Functions

        protected abstract byte GetBiome(int x, int z);

        #endregion

        #region Generation Pipeline

        protected virtual void SetBiomeType()
        {
            biomeId = GetBiome(worldPosition.X, worldPosition.Y);
        }

        protected virtual void SetBiomeHeights()
        {
            height = BiomeManager.GetBiome(biomeId).GetHeight(worldPosition.X, worldPosition.Y);
        }

        protected virtual void InterpolateBiomes()
        {

        }

        protected virtual void InterpolateRegions()
        {

        }

        protected virtual void GenerateTerrainComposition()
        {
            for (int y = 0; y < CommonConstants.World.chunkSize.Y; y++)
                voxels[y] = BiomeManager.GetBiome(biomeId).GetVoxel(worldPosition.X, y, worldPosition.Y, height);
        }

        protected virtual void GenerateFinishers()
        {

        }

        protected virtual void OptimizeRegion()
        {

        }

        #endregion

        internal ushort[] GetVoxelColumn()
        {
            return voxels;
        }
    }
}