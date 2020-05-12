using OpenToolkit.Mathematics;

namespace VoxelValley.Client.Game.Enviroment
{
    public class Voxel
    {
        public int Id;
        public string Name;
        public Vector3 Color;

        public Voxel(string name, int[] color)
        {
            Id = name.GetHashCode();
            Name = name;
            Color = new Vector3((float)color[0] / 255, (float)color[1] / 255, (float)color[2] / 255);
        }
    }
}