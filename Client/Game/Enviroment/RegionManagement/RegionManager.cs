using OpenToolkit.Mathematics;
using VoxelValley.Client.Game.Enviroment.Generation;
using VoxelValley.Client.Game.Enviroment.RegionManagement.Regions;
using VoxelValley.Common;

namespace VoxelValley.Client.Game.Enviroment.RegionManagement
{
    public static class RegionManager
    {
        public static ushort[,,] GetChunk(int worldBaseX, int worldBaseZ)
        {
            ushort[,,] voxels = new ushort[CommonConstants.World.chunkSize.X, CommonConstants.World.chunkSize.Y, CommonConstants.World.chunkSize.Z];

            for (int localX = 0; localX < CommonConstants.World.chunkSize.X; localX++)
                for (int localZ = 0; localZ < CommonConstants.World.chunkSize.Z; localZ++)
                {
                    int worldX = worldBaseX + localX;
                    int worldZ = worldBaseZ + localZ;

                    ushort[] voxelColumn = GetRegion(worldX, worldZ).Generate(new Vector2i(worldX, worldZ));

                    for (int y = 0; y < CommonConstants.World.chunkSize.Y; y++)
                        voxels[localX, y, localZ] = voxelColumn[y];
                }

            return voxels;
        }

        static Region GetRegion(int worldX, int worldZ)
        {
            if (GenerationUtilities.FBMCellular(worldX, worldZ, 1, 0.6f, 1.4f) > 0.5f)
                return RegionReferences.Greenlands;
            else
                return RegionReferences.Desert;
        }
    }
}