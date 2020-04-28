using FastNoise;
using VoxelValley.Common;
using VoxelValley.Common.Helper;

namespace VoxelValley.Client.Game.Enviroment.Generation
{
    public static class WorldGenerator
    {
        static FastNoise.FastNoise generator;

        static WorldGenerator()
        {
            generator = new FastNoise.FastNoise(123); //TODO: Seed
            generator.SetFrequency(0.02f); // größer = steiler, kleiner = flacher
        }

        public static int GetHeight(int x, int z)
        {
            float height = MathHelper.Map(0, CommonConstants.World.chunkSize.Y, -1, 1, generator.GetPerlin(x, z));
            return (int)height;
        }
    }
}