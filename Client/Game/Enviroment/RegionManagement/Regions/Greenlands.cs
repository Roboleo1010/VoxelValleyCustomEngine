using System.Drawing;
using VoxelValley.Client.Game.Enviroment.BiomeManagement;
using VoxelValley.Client.Game.Enviroment.Generation.Maps;

namespace VoxelValley.Client.Game.Enviroment.RegionManagement.Regions
{
    public class Greenlands : Region
    {
        public override string Name => "Greenlands";
        public override Color Color => Color.Green;

        public override Biome GetBiome(int x, int z)
        {
            HeightMap.HeightType heightType = HeightMap.GetHeightType(x, z);

            switch (heightType)
            {
                case HeightMap.HeightType.Lowest:
                case HeightMap.HeightType.Lower:
                    return BiomeReferences.Grasslands.Plains;
                case HeightMap.HeightType.Low:
                case HeightMap.HeightType.High:
                    return BiomeReferences.Grasslands.Forest;
                case HeightMap.HeightType.Higher:
                    return BiomeReferences.Grasslands.Hills;
                case HeightMap.HeightType.Highest:
                    return BiomeReferences.Grasslands.Mountains;
            }

            return null;
        }
    }
}