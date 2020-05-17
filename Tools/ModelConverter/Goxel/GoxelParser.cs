using System;
using System.Drawing;
using System.IO;

namespace VoxelValley.Tools.ModelConverter
{
    public class GoxelParser : ModelParser
    {
        string[] goxVoxels;

        public GoxelParser(string inputPath, string outputPath, string name) : base(inputPath, outputPath, name) { }

        public override void Parse()
        {
            GetData();
            ConvertToVoxels();
        }

        void GetData()
        {
            Console.WriteLine("Loading Goxel Data...");

            string[] goxData = File.ReadAllLines(InputPath);

            goxVoxels = new string[goxData.Length - 3];

            int cleanedIndex = 0;
            for (int i = 3; i < goxData.Length; i++)
            {
                goxVoxels[cleanedIndex] = goxData[i];
                cleanedIndex++;
            }
        }

        void ConvertToVoxels()
        {
            Voxel[] voxels = new Voxel[goxVoxels.Length];

            for (int i = 0; i < goxVoxels.Length; i++)
            {
                string[] data = goxVoxels[i].Split(" ");

                voxels[i] = new Voxel(ColorTranslator.FromHtml("#" + data[3].ToUpper()), new int[] { int.Parse(data[0]), int.Parse(data[2]), int.Parse(data[1]) });
            }

            Console.WriteLine($"Loaded {voxels.Length} voxels");

            Voxels = voxels;
        }
    }
}