namespace VoxelValley.Client.Game.Enviroment.Generation.Biomes
{
    public class Ice : Biome
    {
        public Ice()
        {

        }

        public override int GetHeight(int x, int z)
        {
            return 10;
        }
    }
}