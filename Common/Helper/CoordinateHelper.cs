
using OpenToolkit.Mathematics;
using VoxelValley.Game;

namespace VoxelValley.Common.Helper
{
    public static class CoordinateHelper
    {
        /// <summary>
        /// Converts from ChunkSpace (0,0,1) to WorldSpace (0,0,1 * ChunkSize)
        /// </summary>
        /// <param name="chunkSpacePosition">Position in ChunkSpace</param>
        /// <returns></returns>
        public static Vector3i ConvertFromChunkSpaceToWorldSpace(Vector3i chunkSpacePosition)
        {
            return new Vector3i(
                        chunkSpacePosition.X * Constants.World.chunkSize.X,
                        chunkSpacePosition.Y * Constants.World.chunkSize.Y,
                        chunkSpacePosition.Z * Constants.World.chunkSize.Z);
        }

        /// <summary>
        /// Converts from WorldSpace (0,0,16) to ChunkSpace (0,0,16 / ChunkSize)
        /// </summary>
        /// <param name="worldSpacePosition">Position in WorldSpace</param>
        /// <returns></returns>
        public static Vector3i ConvertFromWorldSpaceToChunkSpace(Vector3i worldSpacePosition)
        {
            return new Vector3i(
                       worldSpacePosition.X / Constants.World.chunkSize.X,
                       worldSpacePosition.Y / Constants.World.chunkSize.Y,
                       worldSpacePosition.Z / Constants.World.chunkSize.Z);
        }
    }
}