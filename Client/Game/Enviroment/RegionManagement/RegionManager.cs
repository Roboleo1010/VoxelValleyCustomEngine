using VoxelValley.Client.Game.Enviroment.Generation;
using VoxelValley.Client.Game.Enviroment.RegionManagement.Regions;

namespace VoxelValley.Client.Game.Enviroment.RegionManagement
{
    public static class RegionManager
    {
        static Greenlands Greenlands = new Greenlands();

        public static void GenerateRegion(int x, int z)
        {

        }

        public static Region GetRegion(int x, int z)
        {
            float region = GenerationUtilities.FBMCellular(x, z, 1, 0.25f, 1f);
            return Greenlands;
        }
    }
}