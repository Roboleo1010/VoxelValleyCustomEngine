using OpenToolkit.Mathematics;

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
                        chunkSpacePosition.X * CommonConstants.World.chunkSize.X,
                        chunkSpacePosition.Y * CommonConstants.World.chunkSize.Y,
                        chunkSpacePosition.Z * CommonConstants.World.chunkSize.Z);
        }

        /// <summary>
        /// Converts from WorldSpace (0,0,16) to ChunkSpace (0,0,16 / ChunkSize)
        /// </summary>
        /// <param name="worldSpacePosition">Position in WorldSpace</param>
        /// <returns></returns>
        public static Vector3i ConvertFromWorldSpaceToChunkSpace(Vector3i worldSpacePosition)
        {
            return new Vector3i(
                       worldSpacePosition.X / CommonConstants.World.chunkSize.X,
                       worldSpacePosition.Y / CommonConstants.World.chunkSize.Y,
                       worldSpacePosition.Z / CommonConstants.World.chunkSize.Z);
        }
    }
}