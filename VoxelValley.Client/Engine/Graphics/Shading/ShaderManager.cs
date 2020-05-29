using System;
using System.Collections.Generic;
using System.Linq;
using VoxelValley.Common.Diagnostics;

namespace VoxelValley.Client.Engine.Graphics.Shading
{
    public static class ShaderManager
    {
        static Type type = typeof(ShaderManager);
        static Dictionary<ShaderType, Shader> shaders = new Dictionary<ShaderType, Shader>();

        public enum ShaderType
        {
            VOXEL,
            DEBUG
        }

        public static void LoadShaders()
        {
            foreach (string name in Enum.GetNames(typeof(ShaderType)))
            {
                Enum.TryParse(typeof(ShaderType), name, out object type); //FIXME: Besser machen
                LoadShader($"Assets/Shaders/{name}", (ShaderType)type);
            }

            Log.Info(type, $"Loaded {shaders.Count} shaders.");
        }

        public static void LoadShader(string path, ShaderType type)
        {
            shaders.Add(type, new Shader($"{path}/shader.vert", $"{path}/shader.frag"));
        }

        public static Shader GetShader(ShaderType type)
        {
            if (shaders.TryGetValue(type, out Shader shader))
                return shader;

            Log.Warn(ShaderManager.type, $"Can't load Shader {type.ToString()}");
            return null;
        }

        public static void RemoveAllShaders()
        {
            while (shaders.Count > 0)
            {
                shaders.Values.ElementAt(0).Remove();
                shaders.Remove(shaders.Keys.ElementAt(0));
            }
        }
    }
}