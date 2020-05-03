using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using VoxelValley.Common.Diagnostics;
using VoxelValley.Common.Helper;

namespace VoxelValley.Client.Game.Enviroment
{
    public static class VoxelTypeManager
    {
        static Type type = typeof(VoxelTypeManager);
        static Dictionary<string, VoxelType> voxelTypes = new Dictionary<string, VoxelType>();

        public static void LoadVoxelTypes()
        {
            string[] paths = FileHelper.GetAllFilesOfType("Common/Assets/VoxelTypes/", "*.json");

            foreach (string path in paths)
                LoadVoxelType(path);
        }

        static void LoadVoxelType(string path)
        {
            List<VoxelType> types = new List<VoxelType>();

            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                types = JsonConvert.DeserializeObject<List<VoxelType>>(reader.ReadToEnd());
            }

            foreach (VoxelType type in types)
            {
                voxelTypes.Add(type.Name, type);
                type.Name = string.Empty;
            }

            Log.Info(type, $"Loaded {voxelTypes.Count} voxels.");
        }

        public static VoxelType GetVoxelType(string name)
        {
            if (voxelTypes.TryGetValue(name, out VoxelType voxelType))
                return voxelType;

            Log.Warn(type, $"Can't get Voxel Type '{name}'");
            return null;
        }
    }
}