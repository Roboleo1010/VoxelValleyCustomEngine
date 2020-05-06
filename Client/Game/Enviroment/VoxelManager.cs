using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using VoxelValley.Common.Diagnostics;
using VoxelValley.Common.Helper;

namespace VoxelValley.Client.Game.Enviroment
{
    public static class VoxelManager
    {
        static Type type = typeof(VoxelManager);
        static Dictionary<string, Voxel> voxels = new Dictionary<string, Voxel>();

        public static void LoadVoxels()
        {
            string[] paths = FileHelper.GetAllFilesOfType("Common/Assets/VoxelTypes/", "*.json");

            foreach (string path in paths)
                LoadVoxel(path);

            if (!voxels.ContainsKey("missing_voxel"))
                Log.Error(type, "Can't find 'missing_voxel'. The game will crash, if a non existent Voxel is requested.");
        }

        static void LoadVoxel(string path)
        {
            List<Voxel> voxels = new List<Voxel>();

            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                voxels = JsonConvert.DeserializeObject<List<Voxel>>(reader.ReadToEnd());
            }

            foreach (Voxel voxel in voxels)
            {
                VoxelManager.voxels.Add(voxel.Name, voxel);
                voxel.Name = string.Empty;
            }

            Log.Info(type, $"Loaded {VoxelManager.voxels.Count} voxels.");
        }

        public static Voxel GetVoxel(string name)
        {
            if (voxels.TryGetValue(name, out Voxel voxelType))
                return voxelType;

            return GetVoxel("missing_voxel");
        }
    }
}