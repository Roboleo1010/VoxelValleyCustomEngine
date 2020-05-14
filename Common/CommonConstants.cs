using OpenToolkit.Mathematics;

namespace VoxelValley.Common
{
    public static class CommonConstants
    {
        public static readonly string Version = "0.0.3 - Game World";

        public static class World
        {
            public static readonly Vector3i chunkSize = new Vector3i(128, 255, 128);
            public static float Gravity = 0.1f;
        }
    }
}
