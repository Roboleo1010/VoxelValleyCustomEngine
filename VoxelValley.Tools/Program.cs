using VoxelValley.Tools.ModelConverter;

namespace VoxelValley.ToolsTools
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                System.Console.WriteLine("Please specify a valid Tool. Tools are: Model Converter");
                return;
            }

            if (args[0].ToLower() == "modelconverter")
                ModelConverter.Start();
        }
    }
}
