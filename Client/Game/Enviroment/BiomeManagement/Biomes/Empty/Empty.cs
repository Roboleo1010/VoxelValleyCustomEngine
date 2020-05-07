using System;
using System.Drawing;
using VoxelValley.Client.Game.Enviroment.Generation;

namespace VoxelValley.Client.Game.Enviroment.BiomeManagement.Biomes
{
    public class Empty : Biome
    {
        public override string Name { get => "Empty"; }
        public override Color Color { get => Color.White; }

        Random r = new Random(); //TODO: Noise

        public override int GetHeight(int x, int z)
        {
            return GenerationUtilities.MapToWorld(GenerationUtilities.FBMPerlin(x, z, 5, 2, 0.1f));
        }

        internal override Voxel GetVoxel(int x, int y, int z, int height)
        {

            return null;
        }
    }
}