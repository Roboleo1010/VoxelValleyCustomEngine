using OpenToolkit.Graphics.OpenGL;

namespace VoxelValley.Engine.Graphics.Shading
{
    public class UniformInfo
    {
        public string name = "";
        public int address = -1;
        public int size = 0;
        public ActiveUniformType type;
    }
}