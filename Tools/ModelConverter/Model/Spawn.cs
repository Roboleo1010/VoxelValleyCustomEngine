namespace VoxelValley.Tools.ModelConverter
{
    public class Spawn
    {
        public string biome { get; private set; }
        public float chance { get; private set; }

        public Spawn(string biome, float chance)
        {
            this.biome = biome;
            this.chance = chance;
        }
    }
}