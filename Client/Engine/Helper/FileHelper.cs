using System;
using System.IO;
using Newtonsoft.Json;
using VoxelValley.Common.Diagnostics;

namespace VoxelValley.Engine.Helper
{
    public static class FileHelper
    {
        static Type type = typeof(FileHelper);

        public static string[] GetAllDirectorys(string path, string filter)
        {
            if (Directory.Exists(path))
                return Directory.GetDirectories(path, filter);

            return new string[0];
        }

        public static string[] GetAllFilesOfType(string path, string filter)
        {
            if (Directory.Exists(path))
                return Directory.GetFiles(path, filter);

            return new string[0];
        }

        public static string GetFileNameWithoutExtention(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        public static void WriteToFileJson(string directory, string filename, string extension, object jsonDataObject)
        {
            WriteToFile(directory, filename, extension, JsonConvert.SerializeObject(jsonDataObject));
        }

        static void WriteToFile(string directory, string filename, string extension, string text)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            File.WriteAllText($"{directory}/{filename}.{extension}", text);

            Log.Info(type, $"Wrote file {directory}/{filename}.{extension}");
        }
    }
}