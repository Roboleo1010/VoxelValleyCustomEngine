using System.IO;

namespace VoxelValley.Engine.Core.Helper
{
    public static class FileHelper
    {
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
    }
}