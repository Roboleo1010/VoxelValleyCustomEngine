using System.Collections.Generic;
using System.Drawing;
using VoxelValley.Client.Game.Enviroment.Structures;

namespace VoxelValley.Client.Game.Enviroment.BiomeManagement
{
    public abstract class Biome
    {
        public abstract string Name { get; }
        public abstract Color Color { get; }
        public abstract byte BiomeId { get; } //TODO: Namespaced

        protected List<Structure> structures;

        public Biome()
        {
            structures = StructureManager.GetStructures(Name);
        }

        public abstract ushort GetHeight(int x, int z);
        internal abstract ushort GetVoxel(int x, int y, int z, ushort height);
        internal abstract void GetFinishers(int worldX, int worldZ, ushort chunkX, ushort chunkZ, int height, ref ushort[,,] voxels);
    }
}