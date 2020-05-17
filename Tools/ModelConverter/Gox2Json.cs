using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using ColorConsole = Colorful.Console;

namespace VoxelValley.Tools.ModelConverter
{
    public class Gox2Json
    {
        string path;

        Dictionary<string, string> voxelLookup;
        string[] goxVoxels;

        string modelName;
        Voxel[] voxels;
        Spawn[] spawns;

        public Gox2Json()
        {
            voxelLookup = new Dictionary<string, string>();

            Console.WriteLine("Welcome to Gox2Json.");

            Console.WriteLine("Please specify path (no extention):");
            path = Console.ReadLine();

            Console.WriteLine("Please specify model name:");
            modelName = Console.ReadLine().ToLower();

            goxVoxels = GetGoxVoxels();
            voxels = ConvertToVoxels();

            ReplaceVoxelLookUps();

            GetSpawns();

            WriteJson();

            Console.WriteLine("Press any key to Exit..");
            Console.Read();
        }

        string[] GetGoxVoxels()
        {
            Console.WriteLine("Loading gox data...");

            string[] goxData = File.ReadAllLines(path + ".txt");

            string[] cleanedGoxData = new string[goxData.Length - 3];


            int cleanedIndex = 0;
            for (int i = 3; i < goxData.Length; i++)
            {
                cleanedGoxData[cleanedIndex] = goxData[i];
                cleanedIndex++;
            }

            return cleanedGoxData;
        }

        private Voxel[] ConvertToVoxels()
        {
            Voxel[] voxels = new Voxel[goxVoxels.Length];

            for (int i = 0; i < goxVoxels.Length; i++)
            {
                string[] data = goxVoxels[i].Split(" ");

                voxels[i] = new Voxel(data[3], new int[] { int.Parse(data[0]), int.Parse(data[2]), int.Parse(data[1]) });
            }

            Console.WriteLine($"Loaded {voxels.Length} voxels");

            return voxels;
        }

        private void ReplaceVoxelLookUps()
        {
            foreach (Voxel voxel in voxels)
            {
                if (!voxelLookup.TryGetValue(voxel.voxel, out string voxelName))
                {
                    ColorConsole.WriteLine($"Specify Voxel for {voxel.voxel}", ColorTranslator.FromHtml("#" + voxel.voxel.ToUpper()));
                    voxelName = Console.ReadLine();
                    voxelLookup.Add(voxel.voxel, voxelName);
                }

                voxel.voxel = voxelName;
            }
        }

        private void GetSpawns()
        {
            List<Spawn> spawnList = new List<Spawn>();

            bool addAnother = false;

            do
            {
                Console.WriteLine($"Which Biome spawns {modelName}?");
                string biome = Console.ReadLine();

                Console.WriteLine($"Whats the spawn chance? eg. 0.005");
                float chance = float.Parse(Console.ReadLine());

                spawnList.Add(new Spawn(biome, chance));

                Console.WriteLine("Add another Spawn (Y/N): ");
                addAnother = Console.ReadLine().ToLower() == "y";

            }
            while (addAnother);

            spawns = spawnList.ToArray();
        }

        private void WriteJson()
        {
            Structure structure = new Structure(modelName, voxels, spawns);

            using (StreamWriter writer = new StreamWriter(path + ".json"))
            {
                writer.Write(JsonConvert.SerializeObject(structure));
            }

            Console.WriteLine("Sucsessfully created json file!");
        }
    }
}