using System;

namespace VoxelValley.Tools.ModelConverter
{
    public static class ModelConverter
    {
        public static void Start()
        {
            Console.WriteLine("Welcome to Model Convert.");

            Console.WriteLine("Please specify input path with extention:");
            string inputPath = Console.ReadLine();
            Console.WriteLine("Please specify output with extention (.json):");
            string outputPath = Console.ReadLine();

            if (string.IsNullOrEmpty(outputPath))
                outputPath = inputPath.Split(".")[0] + ".json";

            Console.WriteLine("Please specify model Name:");
            string name = Console.ReadLine().ToLower();

            ModelParser parser;

            if (inputPath.Contains(".txt"))
                parser = new GoxelParser(inputPath, outputPath, name);
            else if (inputPath.Contains(".vox"))
                parser = new MagicaVoxelParser(inputPath, outputPath, name);
            else
            {
                Console.WriteLine("Unknown file Format. Please use MagicaVoxel (.vox) or Goxel (.txt)");
                return;
            }

            parser.Parse();
            parser.GetSpawns();

            parser.ReplaceVoxelLookUps();
            parser.WriteFile();

            Console.WriteLine("Press Return to Exit..");
            Console.ReadLine();
        }
    }
}