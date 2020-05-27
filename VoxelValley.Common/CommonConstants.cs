using OpenToolkit.Mathematics;

namespace VoxelValley.Common
{
    public static class CommonConstants
    {
        public static class World
        {
            public static readonly Vector3i ChunkSize = new Vector3i(128, 255, 128);
            public static readonly int DrawDistance = 3;
            public static float Gravity = 0.1f;
        }
    }
}