using System;
using OpenToolkit.Mathematics;

namespace VoxelValley.Common.Helper
{
    public static class CoordinateHelper
    {
        public static Vector3i ConvertFromChunkSpaceToWorldSpace(Vector3i chunkSpacePos)
        {
            return new Vector3i(
                        chunkSpacePos.X * CommonConstants.World.chunkSize.X,
                        chunkSpacePos.Y * CommonConstants.World.chunkSize.Y,
                        chunkSpacePos.Z * CommonConstants.World.chunkSize.Z);
        }

        public static (Vector3i chunk, Vector3i voxel) ConvertFromWorldSpaceToVoxelSpace(Vector3 worldSpacePos)
        {
            Vector3i chunk = new Vector3i((int)worldSpacePos.X / CommonConstants.World.chunkSize.X,
                                          (int)worldSpacePos.Y / CommonConstants.World.chunkSize.Y,
                                          (int)worldSpacePos.Z / CommonConstants.World.chunkSize.Z);

            Vector3i voxel = new Vector3i(Math.Abs((int)worldSpacePos.X % CommonConstants.World.chunkSize.X),
                                          Math.Abs((int)worldSpacePos.Y % CommonConstants.World.chunkSize.Y),
                                          Math.Abs((int)worldSpacePos.Z % CommonConstants.World.chunkSize.Z));

            return (chunk, voxel);
        }
    }
}