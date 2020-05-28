using OpenToolkit.Mathematics;

namespace VoxelValley.Common
{
    public static class CommonConstants
    {
        public static class Simulation
        {
            public static int TicksPerSecond = 30;
            public static int MsPerTick = 1000 / TicksPerSecond;
        }
        
        public static class World
        {
            public static readonly Vector3i ChunkSize = new Vector3i(128, 255, 128);
            public static readonly int DrawDistance = 3;
            public static float Gravity = 0.1f;
        }
    }
}