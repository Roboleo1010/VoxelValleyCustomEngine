using System;
using VoxelValley.Client.Engine;
using VoxelValley.Common;
using VoxelValley.Common.Diagnostics;

namespace VoxelValley
{
    class Program
    {
        private static Type type = typeof(Program);

        static void Main(string[] args)
        {
            Log.Info(type, $"Starting Voxel Valley {CommonConstants.Version}");

            StartClient();
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
