namespace VoxelValley.Common.Enviroment
{
    public class CommonChunk
    {
        public static bool InChunk(int x, int y, int z)
        {
            return (x > 0 && y > 0 && z > 0 &&
                x < CommonConstants.World.ChunkSize.X &&
                y < CommonConstants.World.ChunkSize.Y &&
                z < CommonConstants.World.ChunkSize.Z);
        }
    }
}