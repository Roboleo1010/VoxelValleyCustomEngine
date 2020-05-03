using System;
using System.Collections.Generic;
using VoxelValley.Common.Diagnostics;
using static VoxelValley.Client.Engine.Graphics.Rendering.RenderBufferManager;

namespace VoxelValley.Client.Engine.Graphics.Shading
{
    public static class ShaderManager
    {
        static Type type = typeof(ShaderManager);
        static Dictionary<MeshRenderBufferType, Shader> shaders = new Dictionary<MeshRenderBufferType, Shader>();

        public static void LoadShaders()
        {
            foreach (string name in Enum.GetNames(typeof(MeshRenderBufferType)))
            {
                Enum.TryParse(typeof(MeshRenderBufferType), name, out object type); //FIXME: Besser machen
                LoadShader($"Client/Assets/Shaders/{name}", (MeshRenderBufferType)type);
            }

            Log.Info(type, $"Loaded {shaders.Count} sahders.");
        }

        public static void LoadShader(string path, MeshRenderBufferType type)
        {
            shaders.Add(type, new Shader($"{path}/shader.vert", $"{path}/shader.frag"));
        }

        public static Shader GetShader(MeshRenderBufferType type)
        {
            if (shaders.TryGetValue(type, out Shader shader))
                return shader;

            Log.Warn(ShaderManager.type, $"Can't load Shader {type.ToString()}");
            return null;
        }

        public static void RemoveAllShaders()
        {
            // throw new NotImplementedException(); TODO Shader Clanup
        }
    }
}