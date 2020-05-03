using System;
using System.Collections.Generic;
using System.Linq;
using VoxelValley.Client.Engine.Graphics.Shading;
using VoxelValley.Common.Diagnostics;

namespace VoxelValley.Client.Engine.Graphics.Rendering
{
    public static class RenderBufferManager
    {
        static Type type = typeof(RenderBufferManager);
        static Dictionary<MeshRenderBufferType, MeshRenderBuffer> meshRenderBuffers = new Dictionary<MeshRenderBufferType, MeshRenderBuffer>();

        public enum MeshRenderBufferType
        {
            VOXEL
        };

        public static void CreateMeshRenderBuffers()
        {
            foreach (string name in Enum.GetNames(typeof(MeshRenderBufferType)))
            {
                Enum.TryParse(typeof(MeshRenderBufferType), name, out object type); //FIXME: Besser machen
                AddMeshRenderBuffer((MeshRenderBufferType)type);
            }

            Log.Info(type, $"Created {meshRenderBuffers.Count} render buffers.");
        }

        static void AddMeshRenderBuffer(MeshRenderBufferType type)
        {
            MeshRenderBuffer renderBuffer = new MeshRenderBuffer(ShaderManager.GetShader(type));
            meshRenderBuffers.Add(type, renderBuffer);
        }

        public static RenderBuffer[] GetBuffers()
        {
            return meshRenderBuffers.Values.ToArray();
        }

        public static MeshRenderBuffer GetBufferMeshRenderBuffer(MeshRenderBufferType type)
        {
            if (meshRenderBuffers.TryGetValue(type, out MeshRenderBuffer buffer))
                return buffer;

            return null;
        }

        public static void RemoveBuffer(MeshRenderBufferType type)
        {
            RenderBuffer buffer = GetBufferMeshRenderBuffer(type);
            buffer.Remove();
            meshRenderBuffers.Remove(type);
        }

        public static void RemoveAllBuffers()
        {
            // string[] names = renderBuffers.Keys.ToArray(); //TODO: Cleanup

            // foreach (string name in names)
            //     RemoveBuffer(name);
        }
    }
}