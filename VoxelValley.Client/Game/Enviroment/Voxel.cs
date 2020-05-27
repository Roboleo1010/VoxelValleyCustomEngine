using VoxelValley.Engine.Mathematics;

namespace VoxelValley.Client.Game.Enviroment
{
    public class Voxel
    {
        public ushort Id;
        public string Name;
        public Vector4b Color;

        public Voxel(string name, int[] color)
        {
            Name = name;
            Color = new Vector4b((byte)color[0], (byte)color[1], (byte)color[2], 255);
        }
    }
}