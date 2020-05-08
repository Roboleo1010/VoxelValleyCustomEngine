using System;
using System.Collections.Generic;

namespace VoxelValley.Client.Game.Enviroment.BiomeManagement
{
    public static class BiomeManager
    {
        static Type type = typeof(BiomeManager);
        static Dictionary<byte, Biome> biomes = new Dictionary<byte, Biome>();

        public static void LoadBiomes()
        {
            AddBiome(BiomeReferences.Grasslands.Forest);
            AddBiome(BiomeReferences.Grasslands.Hills);
            AddBiome(BiomeReferences.Grasslands.Mountains);
            AddBiome(BiomeReferences.Grasslands.Plains);
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