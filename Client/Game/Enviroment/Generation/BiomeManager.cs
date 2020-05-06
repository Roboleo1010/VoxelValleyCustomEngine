using VoxelValley.Client.Game.Enviroment.Generation.Biomes;
using VoxelValley.Client.Game.Enviroment.Generation.Maps;

namespace VoxelValley.Client.Game.Enviroment.Generation
{
    public static class BiomeManager
    {
        static Desert Desert = new Desert();
        static Ice Ice = new Ice();
        static Plains Plains = new Plains();
        static Forest Forest = new Forest();

        // static Biome[,] biomes = new Biome[6, 6] {   
        // //COLDEST COLDER COLD    HOT                HOTTER               HOTTEST
        // {Ice,Tundra,Grassland,   Desert,             Desert,             Desert },               //DRYEST
        // {Ice,Tundra,Grassland,   Desert,             Desert,             Desert },               //DRYER
        // {Ice,Tundra,Woodland,    Woodland,           Savanna,            Savanna },              //DRY
        // {Ice,Tundra,BorealForest,Woodland,           Savanna,            Savanna },              //WET
        // {Ice,Tundra,BorealForest,SeasonalForest,     TropicalRainforest, TropicalRainforest },   //WETTER
        // {Ice,Tundra,BorealForest,TemperateRainforest,TropicalRainforest, TropicalRainforest } }; //WETTEST

        static Biome[,] biomes = new Biome[6, 6] {   
        //COLDEST COLDER COLD        HOT    HOTTER      HOTTEST
        {Ice,   Ice,    Plains,     Desert, Desert,     Desert },       //DRYEST
        {Ice,   Ice,    Plains,     Desert, Desert,     Desert },       //DRYER
        {Ice,   Ice,    Forest,     Forest, Desert,     Desert },       //DRY
        {Ice,   Ice,    Forest,     Forest, Desert,     Desert },       //WET
        {Ice,   Ice,    Forest,     Forest, Forest,     Desert },       //WETTER
        {Ice,   Ice,    Forest,     Forest, Forest,     Plains } };  //WETTEST

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