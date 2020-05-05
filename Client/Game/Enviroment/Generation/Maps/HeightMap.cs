namespace VoxelValley.Client.Game.Enviroment.Generation.Maps
{
    public static class HeightMap
    {
        internal static float GetValue(float x, float z)
        {
            return GenerationUtilities.FBMPerlin(x, z, 6, 1, 1);
        }
    }
}