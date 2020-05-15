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
    }
}