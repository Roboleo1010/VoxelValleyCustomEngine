using Newtonsoft.Json;

namespace VoxelValley.Client.Game.Enviroment.Structures
{
    public class Spawn
    {
        public string Biome { get; private set; }
        public float Chance { get; private set; }

        [JsonConstructor]
        public Spawn(string biome, float chance)
        {
            Biome = biome;
            Chance = chance;
        }
    }
}