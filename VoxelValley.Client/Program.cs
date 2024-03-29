﻿using System;
using VoxelValley.Client.Engine;
using VoxelValley.Common.Diagnostics;

namespace VoxelValley.Client
{
    class Program
    {
        private static Type type = typeof(Program);

        static void Main(string[] args)
        {
            StartClient();
        }

        static void StartClient()
        {
            try
            {
                Log.Info(type, $"Starting Voxel Valley");
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
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                Log.Fatal(typeof(Program), "", ex);
            }
        }
    }
}
