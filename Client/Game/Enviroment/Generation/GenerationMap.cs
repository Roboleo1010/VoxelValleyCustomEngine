using OpenToolkit.Mathematics;

namespace VoxelValley.Client.Game.Enviroment.Generation
{
    public class GenerationMap
    {
        float[,] data;
        Vector2i size;

        public GenerationMap(Vector2i size)
        {
            this.size = size;
        }
    }
}