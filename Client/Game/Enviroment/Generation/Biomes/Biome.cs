using VoxelValley.Client.Game.Enviroment.Generation.Maps;

namespace VoxelValley.Client.Game.Enviroment.Generation.Biomes
{
    public abstract class Biome
    {
        public abstract int GetHeight(int x, int z); 
    }
}