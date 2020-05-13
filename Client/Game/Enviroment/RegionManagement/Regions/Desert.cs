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

        protected override byte GetBiome(int x, int z)
        {
            HeightMap.HeightType heightType = HeightMap.GetHeightType(x, z);

            switch (heightType)
            {
                case HeightMap.HeightType.Lowest:
                    return BiomeReferences.Desert.Oasis.BiomeId;
                case HeightMap.HeightType.Lower:
                case HeightMap.HeightType.Low:
                case HeightMap.HeightType.High:
                    return BiomeReferences.Desert.FlatDesert.BiomeId;
                case HeightMap.HeightType.Higher:
                case HeightMap.HeightType.Highest:
                    return BiomeReferences.Desert.Dunes.BiomeId;
                default:
                    return 0;
            }
        }
    }
}