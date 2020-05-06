namespace VoxelValley.Client.Game.Enviroment.Generation.Maps
{
    /// <summary>
    /// The height map is just used for the Heat & Moisture map.static Every Biome has it's own Generator
    /// </summary>
    public static class HeightMap
    {
        internal static float GetHeight(float x, float z)
        {
            return GenerationUtilities.FBMPerlin(x, z, 3, 0.45f, 1);
        }
    }
}