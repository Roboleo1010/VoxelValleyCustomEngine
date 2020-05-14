using System;
using System.Drawing;
using System.Linq;
using VoxelValley.Client.Game.Enviroment.Generation;
using VoxelValley.Client.Game.Enviroment.Structures;
using VoxelValley.Common;

namespace VoxelValley.Client.Game.Enviroment.BiomeManagement.Biomes.Desert
{
    public class FlatDesert : Biome
    {
        public override string Name { get => "FlatDesert"; }
        public override Color Color { get => Color.Yellow; }
        public override byte BiomeId { get => 52; }

        public override ushort GetHeight(int x, int z)
        {
            return GenerationUtilities.MapToWorldByte(GenerationUtilities.FBMPerlin(x, z, 5, 0.4f, 0.15f));
        }

        internal override ushort GetVoxel(int x, int y, int z, ushort height)
        {
            if (y > height)
                return VoxelManager.AirVoxel;
            else if (y == height)
                return VoxelManager.GetVoxel("sand").Id;
            else if (y > height - 4)
                return VoxelManager.GetVoxel("sand").Id;
            else
                return VoxelManager.GetVoxel("stone").Id;
        }

        internal override void GetFinishers(int worldX, int worldZ, ushort chunkX, ushort chunkZ, int height, ref ushort[,,] voxels)
        {
            Structure structure = StructureManager.GetStructures(Name).ElementAt(0);

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