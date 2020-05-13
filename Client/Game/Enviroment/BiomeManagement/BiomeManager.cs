using System;
using System.Collections.Generic;

namespace VoxelValley.Client.Game.Enviroment.BiomeManagement
{
    public static class BiomeManager
    {
        static Type type = typeof(BiomeManager);
        static Dictionary<byte, Biome> biomes = new Dictionary<byte, Biome>(); //TODO: Use array and indx with biomeId

        public static void LoadBiomes() //TODO: Load Biomes dynamicly :https://stackoverflow.com/questions/79693/getting-all-types-in-a-namespace-via-reflection
        {
            AddBiome(BiomeReferences.Empty);
            AddBiome(BiomeReferences.Grasslands.Forest);
            AddBiome(BiomeReferences.Grasslands.Hills);
            AddBiome(BiomeReferences.Grasslands.Mountains);
            AddBiome(BiomeReferences.Grasslands.Plains);
            AddBiome(BiomeReferences.InterpolationTest.High);
            AddBiome(BiomeReferences.InterpolationTest.Low);
            AddBiome(BiomeReferences.Desert.Oasis);
            AddBiome(BiomeReferences.Desert.FlatDesert);
            AddBiome(BiomeReferences.Desert.Dunes);
        }

        static void AddBiome(Biome biome)
        {
            biomes.Add(biome.BiomeId, biome);
        }

        public static Biome GetBiome(byte biomeId)
        {
            if (biomes.TryGetValue(biomeId, out Biome biome))
                return biome;

            return BiomeReferences.Empty;
        }
    }
}