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
        static Dictionary<ushort, Voxel> voxels = new Dictionary<ushort, Voxel>();

        static Dictionary<string, Voxel> voxelLookUp = new Dictionary<string, Voxel>();

        static ushort voxelIndex = 0;
        static ushort missingVoxel;
        public static ushort AirVoxel;

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
            {
                voxel.Id = voxelIndex;

                VoxelManager.voxels.Add(voxel.Id, voxel);
                VoxelManager.voxelLookUp.Add(voxel.Name, voxel);

                voxelIndex++;
            }

            Log.Info(type, $"Loaded {VoxelManager.voxels.Count} voxels.");
        }

        public static Voxel GetVoxel(string name)
        {
            if (voxelLookUp.TryGetValue(name, out Voxel voxelType))
                return voxelType;

            return GetVoxel("missing_voxel");
        }

        public static Voxel GetVoxel(ushort id)
        {
            if (voxels.TryGetValue(id, out Voxel voxelType))
                return voxelType;

            return GetVoxel("missing_voxel");
        }
    }
}