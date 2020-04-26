using OpenToolkit.Mathematics;

namespace VoxelValley.Game
{
    public static class Constants
    {
        public static class Game
        {
            public static readonly string Version = "0.0.1 - Techdemo"; //Major.Minor.Bugfix - Alias
            public static readonly int ViewDistance = 5;
        }
        public static class World
        {
            public static readonly Vector3i chunkSize = new Vector3i(128, 32, 128);
        }
    }
}
