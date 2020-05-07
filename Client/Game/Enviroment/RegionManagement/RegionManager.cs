using VoxelValley.Client.Game.Enviroment.Generation;
using VoxelValley.Client.Game.Enviroment.RegionManagement.Regions;

namespace VoxelValley.Client.Game.Enviroment.RegionManagement
{
    public static class RegionManager
    {
        static float firstRegion = float.MaxValue;

        static Greenlands Greenlands = new Greenlands();
        static Empty Empty = new Empty();

        static byte t = byte.MaxValue;
        public static void GenerateRegion(int x, int z)
        {



            // //Get Bounds
            // //Get Type
            // //Get Bioms
            // //Get Biome Bounds
            // //get structures
            // //Build data

        }

        public static Region GetRegion(int x, int z)
        {
            float region = GenerationUtilities.FBMCellular(x, z, 1, 0.25f, 1f);

            if (firstRegion == float.MaxValue)
                firstRegion = region;

            if (region == firstRegion)
                return Greenlands;
            else
                return Empty;
        }
    }
}