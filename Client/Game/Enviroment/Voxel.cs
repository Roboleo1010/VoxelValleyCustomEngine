using OpenToolkit.Mathematics;

namespace VoxelValley.Client.Game.Enviroment
{
    public class Voxel
    {
        public Vector3 Color;
        public string Name;

        public Voxel(string name, int[] color)
        {
            Name = name;
            Color = new Vector3((float)color[0] / 255, (float)color[1] / 255, (float)color[2] / 255);
        }
    }
}