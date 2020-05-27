using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using VoxelValley.Common.Helper;

namespace VoxelValley.Common.Enviroment.Structures
{
    public static class StructureManager
    {
        static Type type = typeof(StructureManager);

        static Dictionary<string, List<StructureSpawn>> structures;

        static StructureManager()
        {
            structures = new Dictionary<string, List<StructureSpawn>>();
        }

        public static void LoadStructures()
        {
            string[] paths = FileHelper.GetAllFilesOfType("Assets/Structures/", "*.json");

            foreach (string path in paths)
                LoadStructure(path);

            //Order by Structure Size
            Dictionary<string, List<StructureSpawn>> sortedDictionary = new Dictionary<string, List<StructureSpawn>>();
            foreach (KeyValuePair<string, List<StructureSpawn>> kvp in structures)
            {
                List<StructureSpawn> sortedList = kvp.Value.OrderByDescending(s => s.Structure.Voxels.GetLength(0) + s.Structure.Voxels.GetLength(2)).ToList();
                sortedDictionary.Add(kvp.Key, sortedList);
            }

            structures = sortedDictionary;
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
                if (structures.TryGetValue(spawn.Biome, out List<StructureSpawn> structureSpawnsInBiome))
                    structureSpawnsInBiome.Add(new StructureSpawn(structure, spawn));
                else
                {
                    structureSpawnsInBiome = new List<StructureSpawn>();
                    structureSpawnsInBiome.Add(new StructureSpawn(structure, spawn));
                    structures.Add(spawn.Biome, structureSpawnsInBiome);
                }
            }

            structure.Spawns = null;
        }

        public static List<StructureSpawn> GetStructures(string biomeName)
        {
            if (structures.TryGetValue(biomeName, out List<StructureSpawn> structureSpawnsInBiome))
                return structureSpawnsInBiome;

            return null;
        }
    }
}