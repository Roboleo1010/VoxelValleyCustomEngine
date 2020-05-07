using System.Drawing;
using VoxelValley.Client.Game.Enviroment.BiomeManagement.Biomes;
using VoxelValley.Client.Game.Enviroment.Generation;

namespace VoxelValley.Client.Game.Enviroment.BiomeManagement
{
    public static class BiomeManager
    {
        static Forest Forest = new Forest();

        // static Biome[,] biomes = new Biome[6, 6] {   
        // //COLDEST COLDER COLD    HOT                HOTTER               HOTTEST
        // {Ice,Tundra,Grassland,   Desert,             Desert,             Desert },               //DRYEST
        // {Ice,Tundra,Grassland,   Desert,             Desert,             Desert },               //DRYER
        // {Ice,Tundra,Woodland,    Woodland,           Savanna,            Savanna },              //DRY
        // {Ice,Tundra,BorealForest,Woodland,           Savanna,            Savanna },              //WET
        // {Ice,Tundra,BorealForest,SeasonalForest,     TropicalRainforest, TropicalRainforest },   //WETTER
        // {Ice,Tundra,BorealForest,TemperateRainforest,TropicalRainforest, TropicalRainforest } }; //WETTEST

        public static Biome GetBiome(int x, int z)
        {
            // float height = HeightMap.GetHeight(x, z);

            //TODO Generate ocean if( height < 20 && Moiste = 5) return Ocean

            // HeatMap.HeatType heatType = HeatMap.GetHeatType(x, z, height);
            // MoistureMap.MoistureType moistureType = MoistureMap.GetMoistureType(x, z, height);

            // float biomeNoise = GenerationUtilities.FBMCellular(x, z, 1, 0.25f, 1f);

            // if (biomeNoise < 0.2f)
            //     return Ice;
            // else if (biomeNoise < 0.4f)
            //     return Forest;
            // else if (biomeNoise < 0.6f)
            //     return Plains;
            // else if (biomeNoise < 0.8f)
            //     return Forest;
            // else
            //     return Mountains;


            return Forest;

            // return biomes[(int)moistureType, (int)heatType];

            //return biomes[0, 0];
        }

        public static Color GetBiomeColor(Biome biome)
        {
            return biome.Color;
        }
    }
}