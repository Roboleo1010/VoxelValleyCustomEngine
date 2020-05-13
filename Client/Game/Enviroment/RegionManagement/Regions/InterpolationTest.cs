using System.Drawing;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Game.Enviroment.BiomeManagement;

namespace VoxelValley.Client.Game.Enviroment.RegionManagement.Regions
{
    public class InterpolationTest : Region
    {
        public override string Name => "InterpolationTest";
        public override Color Color => Color.Red;

        public InterpolationTest(Vector2i positionInWorldSpace) : base(positionInWorldSpace)
        {

        }

        protected override byte GetBiome(int x, int z)
        {
            if (x % 40 > 20 || z % 40 > 20)
                return BiomeReferences.InterpolationTest.Low.BiomeId;
            else
                return BiomeReferences.InterpolationTest.High.BiomeId;
        }
    }
}