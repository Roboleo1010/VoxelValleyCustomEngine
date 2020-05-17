using System;
using System.IO;

namespace VoxelValley.Tools.ModelConverter
{
    public class GoxelParser : ModelParser
    {
        string[] goxVoxels;

        public GoxelParser(string inputPath, string outputPath, string name) : base(inputPath, outputPath, name) { }

        public override void Parse()
        {
            GetGoxVoxels();
            ConvertToVoxels();
        }

        void GetGoxVoxels()
        {
            Console.WriteLine("Loading Goxel data...");

            string[] goxData = File.ReadAllLines(InputPath);

            goxVoxels = new string[goxData.Length - 3];

            int cleanedIndex = 0;
            for (int i = 3; i < goxData.Length; i++)
            {
                goxVoxels[cleanedIndex] = goxData[i];
                cleanedIndex++;
            }
        }

        private void ConvertToVoxels()
        {
            Voxel[] voxels = new Voxel[goxVoxels.Length];

            for (int i = 0; i < goxVoxels.Length; i++)
            {
                string[] data = goxVoxels[i].Split(" ");

                voxels[i] = new Voxel(data[3], new int[] { int.Parse(data[0]), int.Parse(data[2]), int.Parse(data[1]) });
            }

            Console.WriteLine($"Loaded {voxels.Length} voxels");

            Voxels = voxels;
        }
    }
}