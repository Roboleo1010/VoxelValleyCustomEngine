using VoxelValley.Client.Game.Enviroment.BiomeManagement.Biomes;
using VoxelValley.Client.Game.Enviroment.BiomeManagement.Biomes.Desert;
using VoxelValley.Client.Game.Enviroment.BiomeManagement.Biomes.Grasslands;
using VoxelValley.Client.Game.Enviroment.BiomeManagement.Biomes.InterpolationTest;

namespace VoxelValley.Client.Game.Enviroment.BiomeManagement
{
    public static class BiomeReferences
    {
        public static Empty Empty = new Empty();

        public static class Grasslands
        {
            public static Forest Forest = new Forest();
            public static Hills Hills = new Hills();
            public static Mountains Mountains = new Mountains();
            public static Plains Plains = new Plains();
        }

        public static class Desert
        {
            public static FlatDesert FlatDesert = new FlatDesert();
            public static Dunes Dunes = new Dunes();
            public static Oasis Oasis = new Oasis();
        }

        public static class InterpolationTest
        {
            public static High High = new High();
            public static Low Low = new Low();
        }
    }
}