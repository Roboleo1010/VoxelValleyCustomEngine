using OpenToolkit.Mathematics;

namespace VoxelValley.Client.Engine
{
    public static class ClientConstants
    {
        public static class Threading
        {
            public static readonly int MaxThreads = 16;
        }

        public static class Graphics
        {
            public static readonly int ViewDistance = 5;
            public static readonly Vector2i Size = new Vector2i(1280, 720);
            public static readonly int RenderFrequency = 0;
        }
    }
}