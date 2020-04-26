using OpenToolkit.Mathematics;

namespace VoxelValley.Common.Enviroment
{
    public class VoxelType
    {
        public string Name { get; set; }
        public Vector3 Color { get; set; }

        public VoxelType(string name, Vector3 color)
        {
            this.Name = name;
            this.Color = color;
        }
    }
}