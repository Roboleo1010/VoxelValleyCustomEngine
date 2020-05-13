using System.Drawing;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Game.Enviroment.BiomeManagement;
using VoxelValley.Client.Game.Enviroment.Generation.Maps;

namespace VoxelValley.Client.Game.Enviroment.RegionManagement.Regions
{
    public class Desert : Region
    {
        public override string Name => "Desert";
        public override Color Color => Color.Yellow;

        public Desert(Vector2i positionInWorldSpace) : base(positionInWorldSpace)
        {

        }

        protected override Biome GetBiome(int x, int z)
        {
            HeightMap.HeightType heightType = HeightMap.GetHeightType(x, z);

            switch (heightType)
            {
                case HeightMap.HeightType.Lowest:
                    return BiomeReferences.Desert.Oasis;
                case HeightMap.HeightType.Lower:
                case HeightMap.HeightType.Low:
                case HeightMap.HeightType.High:
                    return BiomeReferences.Desert.FlatDesert;
                case HeightMap.HeightType.Higher:
                case HeightMap.HeightType.Highest:
                    return BiomeReferences.Desert.Dunes;
                default:
                    return BiomeReferences.Empty;
            }
        }
    }
}