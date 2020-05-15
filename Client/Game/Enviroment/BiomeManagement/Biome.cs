using System.Collections.Generic;
using System.Drawing;
using VoxelValley.Client.Game.Enviroment.Generation;
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
        internal virtual void GetFinishers(int worldX, int worldZ, ushort chunkX, ushort chunkZ, int height, ref ushort[,,] voxels)
        {
            List<Structure> structures = StructureManager.GetStructures(Name);
            if (structures != null)
                foreach (Structure structure in structures)
                    if (GenerationUtilities.Random.NextDouble() > 0.998f)
                    {
                        for (ushort x = 0; x < structure.Dimension.X; x++)
                            for (ushort y = 0; y < structure.Dimension.Y; y++)
                                for (ushort z = 0; z < structure.Dimension.Z; z++)
                                    if (structure.Voxels[x, y, z] != 0 && Chunk.InChunk(chunkX + x + structure.Origin.X, height + y + structure.Origin.Y, chunkZ + z + structure.Origin.Z))
                                        voxels[chunkX + x + structure.Origin.X, height + y + structure.Origin.Y, chunkZ + z + structure.Origin.Z] = structure.Voxels[x, y, z];
                    }
        }
    }
}