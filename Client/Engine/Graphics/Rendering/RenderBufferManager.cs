using System;
using System.Collections.Generic;
using System.Linq;
using static VoxelValley.Client.Engine.Graphics.Shading.ShaderManager;

namespace VoxelValley.Client.Engine.Graphics.Rendering
{
    public static class RenderBufferManager
    {
        static Type type = typeof(RenderBufferManager);
        static Dictionary<ShaderType, RenderBuffer> renderBuffers = new Dictionary<ShaderType, RenderBuffer>();

        public static void CreateRenderBuffers()
        {
            renderBuffers.Add(ShaderType.VOXEL, new MeshRenderBuffer(ShaderType.VOXEL));
        }

        public static RenderBuffer[] GetBuffers()
        {
            return renderBuffers.Values.ToArray();
        }

        public static RenderBuffer GetBuffer(ShaderType type)
        {
            if (renderBuffers.TryGetValue(type, out RenderBuffer buffer))
                return buffer;

            return null;
        }

        public static void RemoveAllBuffers()
        {
            while (renderBuffers.Count > 0)
            {
                renderBuffers.Values.ElementAt(0).Remove();
                renderBuffers.Remove(renderBuffers.Keys.ElementAt(0));
            }
        }
    }
}