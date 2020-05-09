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
        protected byte[,] heightData;
        protected Voxel[,][] voxelColumns;

        public Region(Vector2i centerPosInWorldSpace, bool[,] regionCover)
        {
            this.centerPosInWorldSpace = centerPosInWorldSpace;
            this.regionCover = regionCover;

            biomeId = new byte[regionCover.GetLength(0), regionCover.GetLength(1)];
            heightData = new byte[regionCover.GetLength(0), regionCover.GetLength(1)];
            voxelColumns = new Voxel[regionCover.GetLength(0), regionCover.GetLength(1)][];
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
            //FIXME: Draw Biome Map
            Color[,] biomeColors = new Color[regionCover.GetLength(0), regionCover.GetLength(1)];

            for (int x = 0; x < regionCover.GetLength(0); x++)
                for (int z = 0; z < regionCover.GetLength(1); z++)
                    if (regionCover[x, z] == true)
                    {
                        biomeId[x, z] = GetBiome(x, z);
                        biomeColors[x, z] = BiomeManager.GetBiome(biomeId[x, z]).Color; //FIXME: Just for testing
                    }

            TextureGenerator.GenerateTexture(biomeColors, "BiomeMap");
        }

        protected abstract byte GetBiome(int x, int z);
        protected virtual void SetBiomeHeights()
        {
            //FIXME: Draw Height Map
            float[,] heightColors = new float[regionCover.GetLength(0), regionCover.GetLength(1)];

            for (int x = 0; x < regionCover.GetLength(0); x++)
                for (int z = 0; z < regionCover.GetLength(1); z++)
                    if (regionCover[x, z] == true)
                    {
                        heightData[x, z] = BiomeManager.GetBiome(biomeId[x, z]).GetHeight(centerPosInWorldSpace.X + x, centerPosInWorldSpace.Y + z);
                        heightColors[x, z] = (float)heightData[x, z] / 255;
                    }

            TextureGenerator.GenerateTexture(heightColors, "HeightMap");
        }

        protected virtual void InterpolateBiomes()
        {
            //TODO: InterpolateBiomes
        }

        protected virtual void InterpolateRegions()
        {
            //TODO: InterpolateRegions
        }

        protected virtual void GenerateTerrainComposition()
        {
            for (int x = 0; x < regionCover.GetLength(0); x++)
                for (int z = 0; z < regionCover.GetLength(1); z++)
                    if (regionCover[x, z] == true)
                        voxelColumns[x, z] = BiomeManager.GetBiome(biomeId[x, z]).GetVoxelColumn(centerPosInWorldSpace.X + x, centerPosInWorldSpace.Y + z, heightData[x, z]);
        }

        internal Voxel[] GetVoxelColumn(int worldPosX, int worldPosZ)
        {
            int translatedPositionX = worldPosX + centerPosInWorldSpace.X;
            int translatedPositionZ = worldPosZ + centerPosInWorldSpace.Y;

            if (translatedPositionX < 0 || translatedPositionX >= regionCover.GetLength(0) ||
                translatedPositionZ < 0 || translatedPositionZ >= regionCover.GetLength(1) ||
                voxelColumns[translatedPositionX, translatedPositionZ] == null)
                return BiomeReferences.Empty.GetVoxelColumn(0, 0, 0);

            return voxelColumns[translatedPositionX, translatedPositionZ];
        }
    }
}