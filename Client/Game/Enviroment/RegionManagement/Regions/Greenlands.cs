using System.Drawing;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Game.Enviroment.BiomeManagement;
using VoxelValley.Client.Game.Enviroment.Generation.Maps;

namespace VoxelValley.Client.Game.Enviroment.RegionManagement.Regions
{
    public class Greenlands : Region
    {
        public override string Name => "Greenlands";
        public override Color Color => Color.Green;

        public Greenlands(Vector2i positionInWorldSpace) : base(positionInWorldSpace)
        {

        }

        protected override byte GetBiome(int x, int z)
        {
            HeightMap.HeightType heightType = HeightMap.GetHeightType(x, z);

            switch (heightType)
            {
                case HeightMap.HeightType.Lowest:
                case HeightMap.HeightType.Lower:
                    return BiomeReferences.Grasslands.Plains.BiomeId;
                case HeightMap.HeightType.Low:
                case HeightMap.HeightType.High:
                    return BiomeReferences.Grasslands.Forest.BiomeId;
                case HeightMap.HeightType.Higher:
                    return BiomeReferences.Grasslands.Hills.BiomeId;
                case HeightMap.HeightType.Highest:
                    return BiomeReferences.Grasslands.Mountains.BiomeId;
                default:
                    return 0;
            }
        }
    }
}