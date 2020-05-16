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

        static Dictionary<string, List<StructureSpawn>> structures;

        static StructureManager()
        {
            structures = new Dictionary<string, List<StructureSpawn>>();
        }

        public static void LoadStructures()
        {
            string[] paths = FileHelper.GetAllFilesOfType("Common/Assets/Structures/", "*.json");

            foreach (string path in paths)
                LoadStructure(path);

            //TODO: Sort structures
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