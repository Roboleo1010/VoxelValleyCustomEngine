using OpenToolkit.Graphics.OpenGL4;

namespace VoxelValley.Client.Engine.Graphics.Shading
{
    public class UniformInfo
    {
        public string name = "";
        public int address = -1;
        public int size = 0;
        public ActiveUniformType type;
    }
}