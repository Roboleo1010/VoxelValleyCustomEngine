using OpenToolkit.Mathematics;

namespace VoxelValley.Common
{
    public static class CommonConstants
    {
        public static readonly string Version = "0.0.3 - Game World"; //Taken from Trello Board https://trello.com/c/aG27Uvyj/7-component-system

        public static class World
        {
            public static readonly Vector3i chunkSize = new Vector3i(64, 64, 64);
            public static float Gravity = -0.02f;
        }
    }
}
