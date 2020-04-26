using System.Collections.Generic;
using System.Linq;
using VoxelValley.Client.Engine.Graphics.Shading;

namespace VoxelValley.Client.Engine.Graphics.Rendering
{
    public static class RenderBufferManager
    {
        static Dictionary<string, RenderBuffer> renderBuffers = new Dictionary<string, RenderBuffer>();

        public static RenderBuffer AddBuffer(string name, string shadername)
        {
            RenderBuffer renderBuffer = new RenderBuffer(ShaderManager.GetShader(shadername));
            renderBuffers.Add(name, renderBuffer);
            return renderBuffer;
        }

        public static RenderBuffer[] GetBuffers()
        {
            return renderBuffers.Values.ToArray();
        }

        public static RenderBuffer GetBuffer(string name)
        {
            if (renderBuffers.TryGetValue(name, out RenderBuffer buffer))
                return buffer;

            return null;
        }

        public static void RemoveBuffer(string name)
        {
            RenderBuffer buffer = GetBuffer(name);
            buffer.Remove();
            renderBuffers.Remove(name);
        }

        public static void RemoveAllBuffers()
        {
            string[] names = renderBuffers.Keys.ToArray();

            foreach (string name in names)
                RemoveBuffer(name);
        }
    }
}