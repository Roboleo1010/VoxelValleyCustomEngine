using OpenToolkit.Mathematics;

namespace VoxelValley.Client.Engine
{
    public static class ClientConstants
    {
        public static class Threading
        {
            public static readonly int MaxThreads = 128;
        }

        public static class Graphics
        {
            public static readonly int RenderDistance = 5; //for generating
            public static readonly int ViewDistance = 8; //until unloaded
            public static readonly Vector2i Size = new Vector2i(1920, 1080);
            public static readonly int RenderFrequency = 0;
        }
    }
}