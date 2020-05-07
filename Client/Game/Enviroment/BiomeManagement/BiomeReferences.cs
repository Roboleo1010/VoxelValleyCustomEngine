using VoxelValley.Client.Game.Enviroment.BiomeManagement.Biomes;

namespace VoxelValley.Client.Game.Enviroment.BiomeManagement
{
    public static class BiomeReferences
    {
        public static class Grasslands
        {
            public static Forest Forest = new Forest();
            public static Hills Hills = new Hills();
            public static Mountains Mountains = new Mountains();
            public static Plains Plains = new Plains();
        }

        public static class Empty
        {
            public static BiomeManagement.Biomes.Empty EmptyBiome = new BiomeManagement.Biomes.Empty();
        }
    }
}