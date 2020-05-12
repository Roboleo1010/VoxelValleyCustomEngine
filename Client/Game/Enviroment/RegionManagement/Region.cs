using System.Drawing;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Game.Enviroment.BiomeManagement;
using VoxelValley.Common.Diagnostics;

namespace VoxelValley.Client.Game.Enviroment.RegionManagement
{
    public abstract class Region
    {
        public abstract string Name { get; }
        public abstract Color Color { get; }

        protected Vector2i centerPosInWorldSpace;
        protected bool[,] regionCover;
        protected byte[,] biomeId;
        protected short[,] heightData;
        protected Voxel[,][] voxelColumns;

        protected int interpolationDistance = 10;
        private int interpolationDistanceHalfed;

        protected int regionWidth; //x
        protected int regionHeight; //z

        public Region(Vector2i centerPosInWorldSpace, bool[,] regionCover)
        {
            this.centerPosInWorldSpace = centerPosInWorldSpace;
            this.regionCover = regionCover;

            regionWidth = regionCover.GetLength(0);
            regionHeight = regionCover.GetLength(1);

            interpolationDistanceHalfed = interpolationDistance / 2;
        }

        public virtual void Generate()
        {
            biomeId = new byte[regionWidth, regionHeight];
            heightData = new short[regionWidth, regionHeight];
            voxelColumns = new Voxel[regionWidth, regionHeight][];

            SetBiomeTypes();
            SetBiomeHeights();
            InterpolateBiomes();
            InterpolateRegions();
            GenerateTerrainComposition();
            GenerateFinishers();
        }

        public virtual void Save()
        {

        }

        #region Generation Pipeline

        protected virtual void SetBiomeTypes()
        {
            Color[,] biomeColors = new Color[regionWidth, regionHeight];

            for (short localX = 0; localX < regionWidth; localX++)
                for (short localZ = 0; localZ < regionHeight; localZ++)
                    if (IsRegionCoverInLocalSpace(localX, localZ))
                    {
                        int worldX = FromLocalToWorldSpaceX(localX);
                        int worldZ = FromLocalToWorldSpaceZ(localZ);

                        biomeId[localX, localZ] = GetBiome(worldX, worldZ);
                        biomeColors[localX, localZ] = BiomeManager.GetBiome(biomeId[localX, localZ]).Color;
                    }

            TextureGenerator.GenerateTexture(biomeColors, "BiomeMap");
        }

        protected abstract byte GetBiome(int x, int z);

        protected virtual void SetBiomeHeights()
        {
            float[,] heightColors = new float[regionWidth, regionHeight];

            for (int localX = 0; localX < regionWidth; localX++)
                for (int localZ = 0; localZ < regionHeight; localZ++)
                    if (IsRegionCoverInLocalSpace(localX, localZ))
                    {
                        int worldX = FromLocalToWorldSpaceX(localX);
                        int worldZ = FromLocalToWorldSpaceZ(localZ);

                        heightData[localX, localZ] = BiomeManager.GetBiome(biomeId[localX, localZ]).GetHeight(worldX, worldZ);
                        heightColors[localX, localZ] = (float)heightData[localX, localZ] / 255;
                    }

            TextureGenerator.GenerateTexture(heightColors, "HeightMap");
        }

        protected virtual void InterpolateBiomes()
        {
            //Find Biome Borders
            for (int localX = 0; localX < regionWidth; localX++)
                for (int localZ = 0; localZ < regionHeight; localZ++)
                    if (IsRegionCoverInLocalSpace(localX, localZ))
                    {
                        int worldX = FromLocalToWorldSpaceX(localX);
                        int worldZ = FromLocalToWorldSpaceZ(localZ);

                        // if (IsBiomeBorder(x, z, 0, 1))    //Top - Bottom
                        // {
                        //     short heightFirst = short.MinValue;
                        //     short heightSecond = short.MinValue;

                        //     //Get First
                        //     for (int i = 0; i < interpolationDistance; i++)
                        //         if (regionCover[x, z + i])
                        //         {
                        //             heightFirst = heightData[x, z + i];
                        //             break;
                        //         }

                        //     if (heightFirst == short.MinValue)
                        //         heightFirst = heightData[x, z];

                        //     //Get Second
                        //     for (int i = -interpolationDistance; i < 0; i++)
                        //         if (regionCover[x, z - i])
                        //         {
                        //             heightSecond = heightData[x, z - i];
                        //             break;
                        //         }

                        //     if (heightSecond == short.MinValue)
                        //         heightSecond = heightData[x, z];

                        //     for (int offset = -interpolationDistance; offset < interpolationDistance; offset++)
                        //     {
                        //         float by = (float)(offset + interpolationDistance) / (float)(interpolationDistance * 2);
                        //         heightData[x, z + offset] = (byte)Common.Helper.MathHelper.InterpolateLinear(heightFirst, heightSecond, by);
                        //     }
                        // }

                        // if (IsBiomeBorder(x, z, 1, 0))    //Right - Left
                        // {
                        //     short heightFirst = short.MinValue;
                        //     short heightSecond = short.MinValue;

                        //     //Get First
                        //     for (int i = 0; i < interpolationDistance; i++)
                        //         if (regionCover[x + i, z])
                        //         {
                        //             heightFirst = heightData[x + i, z];
                        //             break;
                        //         }

                        //     if (heightFirst == short.MinValue)
                        //         heightFirst = heightData[x, z];

                        //     //Get Second
                        //     for (int i = -interpolationDistance; i < 0; i++)
                        //         if (regionCover[x - i, z])
                        //         {
                        //             heightSecond = heightData[x - i, z];
                        //             break;
                        //         }

                        //     if (heightSecond == short.MinValue)
                        //         heightSecond = heightData[x, z];

                        //     for (int offset = -interpolationDistance; offset < interpolationDistance; offset++)
                        //     {
                        //         float by = (float)(offset + interpolationDistance) / (float)(interpolationDistance * 2);
                        //         heightData[x + offset, z] = (byte)Common.Helper.MathHelper.InterpolateLinear(heightFirst, heightSecond, by);
                        //     }
                        // }
                    }
        }

        protected virtual void InterpolateRegions()
        {
            //TODO: InterpolateRegions
        }

        protected virtual void GenerateTerrainComposition()
        {
            for (int localX = 0; localX < regionWidth; localX++)
                for (int localZ = 0; localZ < regionHeight; localZ++)
                    if (IsRegionCoverInLocalSpace(localX, localZ))
                    {
                        int worldX = FromLocalToWorldSpaceX(localX);
                        int worldZ = FromLocalToWorldSpaceZ(localZ);

                        voxelColumns[localX, localZ] = BiomeManager.GetBiome(biomeId[localX, localZ])
                                                                   .GetVoxelColumn(worldX, worldZ, heightData[localX, localZ]);
                    }
        }

        protected virtual void GenerateFinishers()
        {
            for (int localX = 0; localX < regionWidth; localX++)
                for (int localZ = 0; localZ < regionHeight; localZ++)
                    if (IsRegionCoverInLocalSpace(localX, localZ))
                    {
                        int worldX = FromLocalToWorldSpaceX(localX);
                        int worldZ = FromLocalToWorldSpaceZ(localZ);
                    }
        }

        #endregion

        #region  Biome Helper
        protected bool IsBiomeBorder(int x, int z, int xOffset, int zOffset)
        {
            if (biomeId[x, z] != 0)
                if (biomeId[x, z] != biomeId[x + xOffset, z + zOffset])
                    return true;

            return false;
        }
        #endregion

        public bool IsRegionCoverInLocalSpace(int x, int z)
        {
            if (x < 0 || x >= regionWidth || z < 0 || z >= regionHeight)
                return false;

            return regionCover[x, z];
        }

        internal Voxel[] GetVoxelColumn(int worldPosX, int worldPosZ)
        {
            Vector2i translatedPosition = new Vector2i(FromWorldToLocalSpaceX(worldPosX), FromWorldToLocalSpaceZ(worldPosZ));

            if (IsRegionCoverInLocalSpace(translatedPosition.X, translatedPosition.Y))
                return voxelColumns[translatedPosition.X, translatedPosition.Y];
            else
                return BiomeReferences.Empty.GetVoxelColumn(0, 0, 0);
        }

        #region  Coordinate Conversion helper
        public int FromWorldToLocalSpaceX(int xInWorldSpace)
        {
            return xInWorldSpace + centerPosInWorldSpace.X;
        }

        public int FromWorldToLocalSpaceZ(int zInWorldSpace)
        {
            return zInWorldSpace + centerPosInWorldSpace.Y;
        }

        protected int FromLocalToWorldSpaceX(int xInLocalSpace)
        {
            return xInLocalSpace + centerPosInWorldSpace.X;
        }

        protected int FromLocalToWorldSpaceZ(int zInLocalSpace)
        {
            return zInLocalSpace + centerPosInWorldSpace.Y;
        }
        #endregion
    }
}