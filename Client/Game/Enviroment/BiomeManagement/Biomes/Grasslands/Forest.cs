using System;
using System.Drawing;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Game.Enviroment.Generation;

namespace VoxelValley.Client.Game.Enviroment.BiomeManagement.Biomes.Grasslands
{
    public class Forest : Biome
    {
        public override string Name { get => "Forest"; }
        public override Color Color { get => Color.Olive; }
        public override byte BiomeId { get => 1; }

        public override ushort GetHeight(int x, int z)
        {
            return GenerationUtilities.MapToWorldByte(GenerationUtilities.FBMPerlin(x, z, 5, 0.3f, 0.1f));
        }

        internal override ushort GetVoxel(int worldX, int y, int worldZ, ushort height)
        {
            if (y > height)
                return VoxelManager.AirVoxel;
            else if (y == height)
            {
                if (GenerationUtilities.FBMPerlin(worldX + 500, worldZ + 150, 2, 2, 1.5f) > 0.3f)
                    return VoxelManager.GetVoxel("grass").Id;
                else
                    return VoxelManager.GetVoxel("dirt").Id;
            }
            else if (y > height - 4)
                return VoxelManager.GetVoxel("dirt").Id;
            else
                return VoxelManager.GetVoxel("stone").Id;
        }

        internal override void GetFinishers(int worldX, int worldZ, ushort chunkX, ushort chunkZ, int height, ref ushort[,,] voxels)
        {
            // if (GenerationUtilities.Random.NextDouble() > 0.99f &&
            // Chunk.InChunk(chunkX - 2, height, chunkZ - 2) && Chunk.InChunk(chunkX + 2, height + 8, chunkZ + 2))//TODO: Check if free
            // {
            //     ushort log = VoxelManager.GetVoxel("log").Id;
            //     ushort leaf = VoxelManager.GetVoxel("leaf").Id;

            //     //center
            //     voxels[chunkX, height + 1, chunkZ] = log;
            //     voxels[chunkX, height + 2, chunkZ] = log;
            //     voxels[chunkX, height + 3, chunkZ] = log;
            //     voxels[chunkX, height + 4, chunkZ] = log;
            //     voxels[chunkX, height + 5, chunkZ] = log;
            //     voxels[chunkX, height + 6, chunkZ] = log;
            //     voxels[chunkX, height + 7, chunkZ] = leaf;
            //     voxels[chunkX, height + 8, chunkZ] = leaf;

            //     //left
            //     voxels[chunkX - 1, height + 6, chunkZ] = leaf;
            //     voxels[chunkX - 1, height + 7, chunkZ] = leaf;
            //     voxels[chunkX - 2, height + 6, chunkZ] = leaf;
            //     voxels[chunkX - 2, height + 7, chunkZ] = leaf;

            //     //right
            //     voxels[chunkX + 1, height + 6, chunkZ] = leaf;
            //     voxels[chunkX + 1, height + 7, chunkZ] = leaf;
            //     voxels[chunkX + 2, height + 6, chunkZ] = leaf;
            //     voxels[chunkX + 2, height + 7, chunkZ] = leaf;

            //     //top
            //     voxels[chunkX, height + 6, chunkZ - 1] = leaf;
            //     voxels[chunkX, height + 7, chunkZ - 1] = leaf;
            //     voxels[chunkX, height + 6, chunkZ - 2] = leaf;
            //     voxels[chunkX, height + 7, chunkZ - 2] = leaf;

            //     //bottom
            //     voxels[chunkX, height + 6, chunkZ + 1] = leaf;
            //     voxels[chunkX, height + 7, chunkZ + 1] = leaf;
            //     voxels[chunkX, height + 6, chunkZ + 2] = leaf;
            //     voxels[chunkX, height + 7, chunkZ + 2] = leaf;

            //     //top-left
            //     voxels[chunkX - 1, height + 6, chunkZ + 1] = leaf;
            //     voxels[chunkX - 1, height + 7, chunkZ + 1] = leaf;

            //     //top-right
            //     voxels[chunkX + 1, height + 6, chunkZ + 1] = leaf;
            //     voxels[chunkX + 1, height + 7, chunkZ + 1] = leaf;

            //     //bottom-left
            //     voxels[chunkX - 1, height + 6, chunkZ - 1] = leaf;
            //     voxels[chunkX - 1, height + 7, chunkZ - 1] = leaf;

            //     //bottom-right
            //     voxels[chunkX + 1, height + 6, chunkZ - 1] = leaf;
            //     voxels[chunkX + 1, height + 7, chunkZ - 1] = leaf;
            // }
        }
    }
}