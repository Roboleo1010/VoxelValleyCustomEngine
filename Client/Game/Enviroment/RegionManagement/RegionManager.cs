using VoxelValley.Client.Game.Enviroment.Generation;
using VoxelValley.Client.Game.Enviroment.RegionManagement.Regions;

namespace VoxelValley.Client.Game.Enviroment.RegionManagement
{
    public static class RegionManager
    {
        public static ushort[,,] GetChunk(int worldBaseX, int worldBaseZ)
        {
            ushort[,,] voxels = new ushort[ClientConstants.World.ChunkSize.X, ClientConstants.World.ChunkSize.Y, ClientConstants.World.ChunkSize.Z];

            GenerationPipeline.Generate(worldBaseX, worldBaseZ, ref voxels);

            return voxels;
        }

        public static Region GetRegion(int worldX, int worldZ)
        {
            if (GenerationUtilities.Cellular(worldX, worldZ, 0.6f, 1.4f) > 0.5f)
                return RegionReferences.Greenlands;
            else
                return RegionReferences.Desert;
        }
    }
}