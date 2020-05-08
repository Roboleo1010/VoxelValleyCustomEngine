using System.Drawing;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Game.Enviroment.BiomeManagement;

namespace VoxelValley.Client.Game.Enviroment.RegionManagement
{
    public abstract class Region
    {
        public abstract string Name { get; }
        public abstract Color Color { get; }

        protected Vector2i posInWorld;
        protected bool[,] regionCover;
        protected byte[,] biomeId;
        protected byte[,] heightData;
        protected Voxel[,][] voxelColumns;

        public Region(Vector2i posInWorld, bool[,] regionCover)
        {
            this.posInWorld = posInWorld;
            this.regionCover = regionCover;

            biomeId = new byte[regionCover.GetLength(0), regionCover.GetLength(1)];
            heightData = new byte[regionCover.GetLength(0), regionCover.GetLength(1)];
            voxelColumns = new Voxel[regionCover.GetLength(0), regionCover.GetLength(1)][];

            for (int x = 0; x < regionCover.GetLength(0); x++)
                for (int z = 0; z < regionCover.GetLength(1); z++)
                    voxelColumns[x, z] = new Voxel[byte.MaxValue];
        }

        public virtual void Generate()
        {
            SetBiomeTypes();
            SetBiomeHeights();
        }

        protected virtual void SetBiomeTypes()
        {
            for (int x = 0; x < regionCover.GetLength(0); x++)
                for (int z = 0; z < regionCover.GetLength(1); z++)
                    if (regionCover[x, z] == true)
                        biomeId[x, z] = GetBiome(x, z);
        }

        protected abstract byte GetBiome(int x, int z);
        protected virtual void SetBiomeHeights()
        {
            for (int x = 0; x < regionCover.GetLength(0); x++)
                for (int z = 0; z < regionCover.GetLength(1); z++)
                    heightData[x, z] = BiomeManager.GetBiome(biomeId[x, z]).GetHeight(posInWorld.X + x, posInWorld.Y + z);
        }
    }
}