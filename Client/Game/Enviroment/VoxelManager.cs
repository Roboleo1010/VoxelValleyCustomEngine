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

        static Dictionary<int, Voxel> voxels = new Dictionary<int, Voxel>();
        static int missingVoxel;
        public static int AirVoxel;

        public static void LoadVoxels()
        {
            string[] paths = FileHelper.GetAllFilesOfType("Common/Assets/VoxelTypes/", "*.json");

            foreach (string path in paths)
                LoadVoxelJson(path);

            missingVoxel = GetVoxel("missing_voxel").Id;
            AirVoxel = GetVoxel("air").Id;
        }

        static void LoadVoxelJson(string path)
        {
            List<Voxel> voxels = new List<Voxel>();

            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                voxels = JsonConvert.DeserializeObject<List<Voxel>>(reader.ReadToEnd());
            }

            foreach (Voxel voxel in voxels)
                VoxelManager.voxels.Add(voxel.Id, voxel);

            Log.Info(type, $"Loaded {VoxelManager.voxels.Count} voxels.");
        }

        public static Voxel GetVoxel(int id)
        {
            if (voxels.TryGetValue(id, out Voxel voxelType))
                return voxelType;

            return GetVoxel("missingVoxel");
        }

        public static Voxel GetVoxel(string name)
        {
            return GetVoxel(name.GetHashCode());
        }
    }
}