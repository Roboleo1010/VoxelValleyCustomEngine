using OpenToolkit.Mathematics;

namespace VoxelValley.Client.Engine
{
    public static class ClientConstants
    {
        public static class Threading
        {
            public static readonly int MaxThreads = 32;
        }

        public static class Graphics
        {
            public static readonly int RenderDistance = 3; //for generating
            public static readonly Vector2i Size = new Vector2i(1920, 1080);
            public static readonly int RenderFrequency = 0;
            public static class CameraType
            {
                public static readonly string PlayerFirstPerson = "Player_First_Person";
                public static readonly string PlayerThirdPerson = "Player_Third_Person";
                public static readonly string Debug = "Debug";
            }
        }
    }
}