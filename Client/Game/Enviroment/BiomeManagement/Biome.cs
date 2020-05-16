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

        protected List<StructureSpawn> structureSpawns;

        public Biome()
        {
            structureSpawns = StructureManager.GetStructures(Name);
        }

        public abstract ushort GetHeight(int x, int z);
        internal abstract ushort GetVoxel(int x, int y, int z, ushort height);
        internal virtual void GetFinishers(int worldX, int worldZ, ushort chunkX, ushort chunkZ, int height, ref ushort[,,] voxels)
        {
            if (structureSpawns != null)
                foreach (StructureSpawn structureSpawn in structureSpawns)
                {
                    if (GenerationUtilities.Random.NextDouble() < structureSpawn.Chance)
                    {
                        for (ushort x = 0; x < structureSpawn.Structure.Dimension.X; x++)
                            for (ushort y = 0; y < structureSpawn.Structure.Dimension.Y; y++)
                                for (ushort z = 0; z < structureSpawn.Structure.Dimension.Z; z++)
                                    if (structureSpawn.Structure.Voxels[x, y, z] != 0 && Chunk.InChunk(chunkX + x + structureSpawn.Structure.Origin.X, height + y + structureSpawn.Structure.Origin.Y, chunkZ + z + structureSpawn.Structure.Origin.Z))
                                        voxels[chunkX + x + structureSpawn.Structure.Origin.X, height + y + structureSpawn.Structure.Origin.Y, chunkZ + z + structureSpawn.Structure.Origin.Z] = structureSpawn.Structure.Voxels[x, y, z];
                    }
                }
        }
    }
}