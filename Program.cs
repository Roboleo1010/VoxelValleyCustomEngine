using System;
using VoxelValley.Client.Engine;
using VoxelValley.Common;
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
                Log.Info(type, $"Starting Voxel Valley {CommonConstants.Version}");
                StartClient();
            }
            else if (args[0] == "Gox2Json")
            {
                Log.Info(type, $"Starting Gox2Json");
                Gox2Json converter = new Gox2Json();
            }
            else if (args[0] == "Magica2Json")
            {
                Log.Info(type, $"Starting Magica2Json");
                Magica2Json converter = new Magica2Json();
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
