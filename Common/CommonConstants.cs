using OpenToolkit.Mathematics;

namespace VoxelValley.Common
{
    public static class CommonConstants
    {
        public static readonly string Version = "0.0.1 - Basics"; //Taken from Trello Board https://trello.com/c/aG27Uvyj/7-component-system

        public static class World
        {
            public static readonly Vector3i chunkSize = new Vector3i(128, 32, 128);
        }
    }
}
