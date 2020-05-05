using VoxelValley.Client.Game.Enviroment.Generation.Maps;

namespace VoxelValley.Client.Game.Enviroment.Generation.Biomes
{
    public static class BiomeManager
    {
        static Desert Desert = new Desert();
        static Ice Ice = new Ice();
        static Desert Tundra = new Desert();
        static Desert Grassland = new Desert();
        static Desert Woodland = new Desert();
        static Desert SeasonalForest = new Desert();
        static Desert TemperateRainforest = new Desert();
        static Desert Savanna = new Desert();
        static Desert TropicalRainforest = new Desert();
        static Desert BorealForest = new Desert();

        static Biome[,] biomes = new Biome[6, 6] {   
        //COLDEST COLDER COLD    HOT                HOTTER               HOTTEST
        {Ice,Tundra,Grassland,   Desert,             Desert,             Desert },               //DRYEST
        {Ice,Tundra,Grassland,   Desert,             Desert,             Desert },               //DRYER
        {Ice,Tundra,Woodland,    Woodland,           Savanna,            Savanna },              //DRY
        {Ice,Tundra,BorealForest,Woodland,           Savanna,            Savanna },              //WET
        {Ice,Tundra,BorealForest,SeasonalForest,     TropicalRainforest, TropicalRainforest },   //WETTER
        {Ice,Tundra,BorealForest,TemperateRainforest,TropicalRainforest, TropicalRainforest } }; //WETTEST

        public static Biome GetBiome(int x, int z)
        {
            float height = HeightMap.GetHeight(x, z);

            //TODO Generate ocean if( height < 20 && Moiste = 5) return Ocean

            HeatMap.HeatType heatType = HeatMap.GetHeatType(x, z, height);
            MoistureMap.MoistureType moistureType = MoistureMap.GetMoistureType(x, z, height);

            return biomes[(int)moistureType, (int)heatType];
        }
    }
}