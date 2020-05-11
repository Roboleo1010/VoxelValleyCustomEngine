using System;
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

        public Region(Vector2i centerPosInWorldSpace, bool[,] regionCover)
        {
            this.centerPosInWorldSpace = centerPosInWorldSpace;
            this.regionCover = regionCover;

            biomeId = new byte[regionCover.GetLength(0), regionCover.GetLength(1)];
            heightData = new short[regionCover.GetLength(0), regionCover.GetLength(1)];
            voxelColumns = new Voxel[regionCover.GetLength(0), regionCover.GetLength(1)][];

            interpolationDistanceHalfed = interpolationDistance / 2;
        }

        public virtual void Generate()
        {
            SetBiomeTypes();
            SetBiomeHeights();
            InterpolateBiomes();
            InterpolateRegions();
            GenerateTerrainComposition();
        }

        protected virtual void SetBiomeTypes()
        {
            Color[,] biomeColors = new Color[regionCover.GetLength(0), regionCover.GetLength(1)];

            for (int x = 0; x < regionCover.GetLength(0); x++)
                for (int z = 0; z < regionCover.GetLength(1); z++)
                    if (IsRegionCover(x, z))
                    {
                        biomeId[x, z] = GetBiome(x, z);
                        biomeColors[x, z] = BiomeManager.GetBiome(biomeId[x, z]).Color;
                    }

            TextureGenerator.GenerateTexture(biomeColors, "BiomeMap");
        }

        protected abstract byte GetBiome(int x, int z);
        protected virtual void SetBiomeHeights()
        {
            float[,] heightColors = new float[regionCover.GetLength(0), regionCover.GetLength(1)];

            for (int x = 0; x < regionCover.GetLength(0); x++)
                for (int z = 0; z < regionCover.GetLength(1); z++)
                    if (IsRegionCover(x, z))
                    {
                        heightData[x, z] = BiomeManager.GetBiome(biomeId[x, z]).GetHeight(centerPosInWorldSpace.X + x, centerPosInWorldSpace.Y + z);
                        heightColors[x, z] = (float)heightData[x, z] / 255;
                    }

            TextureGenerator.GenerateTexture(heightColors, "HeightMap");
        }

        protected virtual void InterpolateBiomes()
        {
            //Find Biome Borders
            for (int x = 0; x < regionCover.GetLength(0); x++)
                for (int z = 0; z < regionCover.GetLength(1); z++)
                    if (IsRegionCover(x, z))
                    {
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

        protected bool IsBiomeBorder(int x, int z, int xOffset, int zOffset)
        {
            if (biomeId[x, z] != 0)
                if (biomeId[x, z] != biomeId[x + xOffset, z + zOffset])
                    return true;

            return false;
        }

        public bool IsRegionCover(int x, int z)
        {
            if (x < 0 || x > regionCover.GetLength(0) || z < 0 || z > regionCover.GetLength(1))
                return false;

            return regionCover[x, z];
        }

        public bool IsRegionCover(Vector2i pos)
        {
            return IsRegionCover(pos.X, pos.Y);
        }

        protected virtual void InterpolateRegions()
        {
            //TODO: InterpolateRegions
        }

        protected virtual void GenerateTerrainComposition()
        {
            for (int x = 0; x < regionCover.GetLength(0); x++)
                for (int z = 0; z < regionCover.GetLength(1); z++)
                    if (IsRegionCover(x, z))
                        voxelColumns[x, z] = BiomeManager.GetBiome(biomeId[x, z]).GetVoxelColumn(centerPosInWorldSpace.X + x, centerPosInWorldSpace.Y + z, heightData[x, z]);
        }

        internal Voxel[] GetVoxelColumn(int worldPosX, int worldPosZ)
        {
            Vector2i translatedPosition = GetTranslatedPosition(worldPosX, worldPosZ);

            if (!IsRegionCover(translatedPosition.X, translatedPosition.Y))
                return BiomeReferences.Empty.GetVoxelColumn(0, 0, 0);
            else
                return voxelColumns[translatedPosition.X, translatedPosition.Y];
        }

        public Vector2i GetTranslatedPosition(int worldPosX, int worldPosZ)
        {
            int translatedPositionX = worldPosX + centerPosInWorldSpace.X;
            int translatedPositionZ = worldPosZ + centerPosInWorldSpace.Y;

            return new Vector2i(translatedPositionX, translatedPositionZ);
        }
    }
}