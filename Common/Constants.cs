using VoxelValley.Engine.Core.Helper;

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
            public static readonly Vector3Int chunkSize = new Vector3Int(128,32,128);
        }
    }
}
