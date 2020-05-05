namespace VoxelValley.Client.Game.Enviroment.Generation.Biomes
{
    public class Desert : Biome
    {
        public override int GetHeight(int x, int z)
        {
            return GenerationUtilities.MapToWorld(GenerationUtilities.FBMPerlin(x, z, 5, 2, 0.1f));
        }
    }
}