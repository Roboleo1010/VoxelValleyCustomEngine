using System;
using System.Collections.Generic;
using ColorConsole = Colorful.Console;
using System.IO;
using Newtonsoft.Json;
using System.Drawing;
using System.Globalization;

namespace VoxelValley.Tools.ModelConverter
{
    public abstract class ModelParser
    {
        protected string InputPath { get; private set; }
        protected string OutputPath { get; private set; }
        protected string Name { get; private set; }

        protected Voxel[] Voxels;
        protected Spawn[] Spawns;
        protected Dictionary<Color, string> voxelLookup;

        public ModelParser(string inputPath, string outputPath, string name)
        {
            InputPath = inputPath;
            OutputPath = outputPath;
            Name = name;

            voxelLookup = new Dictionary<Color, string>();
        }

        public abstract void Parse();

        public virtual void GetSpawns()
        {
            List<Spawn> spawnList = new List<Spawn>();

            bool addAnother = false;

            do
            {
                Console.WriteLine($"Which Biome spawns {Name}?");
                string biome = Console.ReadLine();

                Console.WriteLine($"Whats the spawn chance? eg. 0.005");

                float.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out float chance);

                spawnList.Add(new Spawn(biome, chance));

                Console.WriteLine("Add another Spawn (Y/N): ");
                addAnother = Console.ReadLine().ToLower() == "y";

            }
            while (addAnother);

            Spawns = spawnList.ToArray();
        }

        public virtual void ReplaceVoxelLookUps()
        {
            foreach (Voxel voxel in Voxels)
            {
                if (!voxelLookup.TryGetValue(voxel.color, out string voxelName))
                {
                    ColorConsole.WriteLine($"Specify Voxel for Color", voxel.color);
                    voxelName = Console.ReadLine();
                    voxelLookup.Add(voxel.color, voxelName);
                }

                voxel.voxel = voxelName;
            }
        }

        public virtual void WriteFile()
        {
            Structure structure = new Structure(Name, Voxels, Spawns);

            using (StreamWriter writer = new StreamWriter(OutputPath))
            {
                writer.Write(JsonConvert.SerializeObject(structure));
            }

            Console.WriteLine("Sucsessfully created json file!");
        }
    }
}