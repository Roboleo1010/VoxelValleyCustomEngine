using System;
using System.Collections.Generic;
using System.Linq;
using VoxelValley.Client.Engine.Graphics.Shading;

namespace VoxelValley.Client.Engine.Graphics.Rendering
{
    public static class RenderBufferManager
    {
        static Type type = typeof(RenderBufferManager);
        static Dictionary<ShaderManager.ShaderType, RenderBuffer> renderBuffers = new Dictionary<ShaderManager.ShaderType, RenderBuffer>();

        public static void CreateRenderBuffers()
        {
            renderBuffers.Add(ShaderManager.ShaderType.VOXEL, new VoxelRenderBuffer(ShaderManager.ShaderType.VOXEL));
            // renderBuffers.Add(ShaderManager.ShaderType.DEBUG, new DebugRenderBuffer(ShaderManager.ShaderType.DEBUG));
        }

        public static RenderBuffer[] GetBuffers()
        {
            return renderBuffers.Values.ToArray();
        }

        public static RenderBuffer GetBuffer(ShaderManager.ShaderType type)
        {
            if (renderBuffers.TryGetValue(type, out RenderBuffer buffer))
                return buffer;

            return null;
        }

        public static void RemoveAllBuffers()
        {
            foreach (RenderBuffer buffer in renderBuffers.Values)
                buffer.Remove();
        }
    }
}