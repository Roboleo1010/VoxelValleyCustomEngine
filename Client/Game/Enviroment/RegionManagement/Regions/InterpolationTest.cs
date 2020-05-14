using System;
using System.Drawing;
using VoxelValley.Client.Game.Enviroment.BiomeManagement;

namespace VoxelValley.Client.Game.Enviroment.RegionManagement.Regions
{
    public class InterpolationTest : Region
    {
        public override string Name => "InterpolationTest";
        public override Color Color => Color.Red;

        protected override Biome GetBiome(int x, int z)
        {
            if (Math.Abs(x) % 40 > 20 || Math.Abs(z) % 40 > 20)
                return BiomeReferences.InterpolationTest.Low;
            else
                return BiomeReferences.InterpolationTest.High;
        }
    }
}