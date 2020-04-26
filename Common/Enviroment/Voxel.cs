using OpenToolkit.Mathematics;

namespace VoxelValley.Game.Enviroment
{
    public class Voxel
    {
        public Vector3 Color { get; set; }

        public Voxel(Vector3 color)
        {
            Color = color;
        }
    }
}