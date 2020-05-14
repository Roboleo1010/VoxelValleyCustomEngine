using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using VoxelValley.Common.Helper;

namespace VoxelValley.Client.Game.Enviroment.Structures
{
    public static class StructureManager
    {
        static Type type = typeof(StructureManager);

        static Dictionary<string, List<Structure>> structures;

        static StructureManager()
        {
            structures = new Dictionary<string, List<Structure>>();
        }

        public static void LoadStructures()
        {
            string[] paths = FileHelper.GetAllFilesOfType("Common/Assets/Structures/", "*.json");

            foreach (string path in paths)
                LoadStructure(path);
        }

        static void LoadStructure(string path)
        {
            Structure structure;

            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                structure = JsonConvert.DeserializeObject<Structure>(reader.ReadToEnd());
            }

            foreach (Spawn spawn in structure.Spawns)
            {
                if (structures.TryGetValue(spawn.Biome, out List<Structure> structuresInBiome))
                    structuresInBiome.Add(structure);
                else
                {
                    structuresInBiome = new List<Structure>();
                    structuresInBiome.Add(structure);
                    structures.Add(spawn.Biome, structuresInBiome);
                }
            }

            structure.Spawns = null;
        }

        public static List<Structure> GetStructures(string biomeName)
        {
            if (structures.TryGetValue(biomeName, out List<Structure> structuresForBiome))
                return structuresForBiome;

            return null;
        }
    }
}