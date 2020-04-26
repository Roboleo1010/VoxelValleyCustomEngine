using System;
using System.Collections.Generic;
using System.IO;
using VoxelValley.Common.Diagnostics;
using VoxelValley.Common.Helper;

namespace VoxelValley.Client.Engine.Graphics.Shading
{
    public static class ShaderManager
    {
        static Type type = typeof(ShaderManager);
        static Dictionary<string, Shader> shaders;

        static ShaderManager()
        {
            shaders = new Dictionary<string, Shader>();
        }

        public static void LoadShaders()
        {
            string[] directories = FileHelper.GetAllDirectorys("Client/Engine/Assets/Shaders", "*");

            foreach (string directory in directories)
            {
                FileInfo fileInfo = new FileInfo(directory);
                LoadShader(fileInfo.FullName, fileInfo.Name);
            }
        }

        public static void LoadShader(string path, string name)
        {
            shaders.Add(name, new Shader($"{path}/shader.vert", $"{path}/shader.frag"));
        }

        public static Shader GetShader(string shaderName)
        {
            if (shaders.TryGetValue(shaderName, out Shader shader))
                return shader;

            Log.Warn(type, $"Can't load Shader {shaderName}");
            return null;
        }
    }
}