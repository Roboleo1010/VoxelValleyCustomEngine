using System;
using VoxelValley.Client.Engine;
using VoxelValley.Common.Diagnostics;
using VoxelValley.Tools.ModelConverter;

namespace VoxelValley
{
    class Program
    {
        private static Type type = typeof(Program);

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Log.Info(type, $"Starting Voxel Valley");
                StartClient();
            }
            else if (args[0] == "ConvertModel")
            {
                Log.Info(type, $"Starting ConvertModel");
                ModelConverter.Start();
            }
        }

        static void StartClient()
        {
            try
            {
                EngineManager.OpenWindow();
            }
            catch (Exception ex)
            {
                Log.Fatal(typeof(Program), "", ex);
            }
        }

        static void StartServer()
        {
            try
            {
                throw new NotImplementedException(); //TODO
            }
            catch (Exception ex)
            {
                Log.Fatal(typeof(Program), "", ex);
            }
        }
    }
}
