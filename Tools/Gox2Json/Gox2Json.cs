using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using VoxelValley.Tools.Gox2Json.Model;

namespace VoxelValley.Tools.Gox2Json
{
    public class Gox2Json
    {
        string goxPath;
        string jsonPath;

        Dictionary<string, string> voxelLookup;
        string[] goxVoxels;

        string modelName;
        Voxel[] voxels;
        Spawn[] spawns;

        public Gox2Json()
        {
            voxelLookup = new Dictionary<string, string>();

            Console.WriteLine("Welcome to Gox2Json.");

            Console.WriteLine("Please specify .txt path:");
            goxPath = Console.ReadLine();

            Console.WriteLine("Please specify output path:");
            jsonPath = Console.ReadLine();

            Console.WriteLine("Please specify model name:");
            modelName = Console.ReadLine().ToLower();

            goxVoxels = GetGoxVoxels();
            voxels = ConvertToVoxels();

            ReplaceVoxelLookUps();

            GetSpawns();

            WriteJson();

            Console.WriteLine("Press any key to Exit..");
            Console.ReadLine();
        }

        string[] GetGoxVoxels()
        {
            Console.WriteLine("Loading gox data...");

            string[] goxData = File.ReadAllLines(goxPath);

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

                voxels[i] = new Voxel(data[3], new int[] { int.Parse(data[0]), int.Parse(data[1]), int.Parse(data[2]) });
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
                    Console.WriteLine($"Specify Voxel for {voxel.voxel}");

                    voxelName = Console.ReadLine();

                    voxelLookup.Add(voxel.voxel, voxelName);
                }

                voxel.voxel = voxelName;
            }
        }

        private void GetSpawns()
        {
            spawns = new Spawn[] { };
        }

        private void WriteJson()
        {
            Structure structure = new Structure(modelName, voxels, spawns);

            using (StreamWriter writer = new StreamWriter(jsonPath))
            {
                writer.Write(JsonConvert.SerializeObject(structure));
            }

            Console.WriteLine("Sucsessfully created json Structure!");
        }
    }
}