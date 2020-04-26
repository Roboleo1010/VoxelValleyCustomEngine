using VoxelValley.Engine.Core.Helper;

namespace VoxelValley.Game.Helper
{
    public static class CoordinateHelper
    {
        /// <summary>
        /// Converts from ChunkSpace (0,0,1) to WorldSpace (0,0,1 * ChunkSize)
        /// </summary>
        /// <param name="chunkSpacePosition">Position in ChunkSpace</param>
        /// <returns></returns>
        public static Vector3Int ConvertFromChunkSpaceToWorldSpace(Vector3Int chunkSpacePosition)
        {
            return new Vector3Int(
                        chunkSpacePosition.X * Constants.World.chunkSize.X,
                        chunkSpacePosition.Y * Constants.World.chunkSize.Y,
                        chunkSpacePosition.Z * Constants.World.chunkSize.Z);
        }

        /// <summary>
        /// Converts from WorldSpace (0,0,16) to ChunkSpace (0,0,16 / ChunkSize)
        /// </summary>
        /// <param name="worldSpacePosition">Position in WorldSpace</param>
        /// <returns></returns>
        public static Vector3Int ConvertFromWorldSpaceToChunkSpace(Vector3Int worldSpacePosition)
        {
            return new Vector3Int(
                       worldSpacePosition.X / Constants.World.chunkSize.X,
                       worldSpacePosition.Y / Constants.World.chunkSize.Y,
                       worldSpacePosition.Z / Constants.World.chunkSize.Z);
        }
    }
}