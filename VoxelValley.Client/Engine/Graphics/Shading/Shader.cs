using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenToolkit.Graphics.OpenGL4;

namespace VoxelValley.Client.Engine.Graphics.Shading
{
    public class Shader
    {
        int programID = -1;
        int vertShaderID = -1;
        int fragShaderID = -1;
        int attributeCount = 0;
        int uniformCount = 0;

        Dictionary<string, AttributeInfo> attributes = new Dictionary<string, AttributeInfo>();
        Dictionary<string, UniformInfo> uniforms = new Dictionary<string, UniformInfo>();

        public Shader()
        {
            programID = GL.CreateProgram();
        }

        public Shader(string vertShader, string fragShader, bool fromFile = true)
        {
            programID = GL.CreateProgram();

            if (fromFile)
            {
                LoadShaderFromFile(vertShader, ShaderType.VertexShader);
                LoadShaderFromFile(fragShader, ShaderType.FragmentShader);
            }
            else
            {
                LoadShaderFromString(vertShader, ShaderType.VertexShader);
                LoadShaderFromString(fragShader, ShaderType.FragmentShader);
            }

            Link();
        }

        #region  Loading
        int LoadShader(string source, ShaderType type)
        {
            int shaderAddress = GL.CreateShader(type);
            GL.ShaderSource(shaderAddress, source);
            GL.CompileShader(shaderAddress);
            GL.AttachShader(programID, shaderAddress);

            if (type == ShaderType.VertexShader)
                vertShaderID = shaderAddress;
            else if (type == ShaderType.FragmentShader)
                fragShaderID = shaderAddress;

            WriteShaderInfoLog(shaderAddress);

            return shaderAddress;
        }

        public int LoadShaderFromString(string source, ShaderType type)
        {
            return LoadShader(source, type);
        }

        public int LoadShaderFromFile(string path, ShaderType type)
        {
            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                return LoadShader(reader.ReadToEnd(), type);
            }
        }
        #endregion

        void Link()
        {
            GL.LinkProgram(programID);
            WriteProgramInfoLog(programID);

            GL.GetProgram(programID, GetProgramParameterName.ActiveAttributes, out attributeCount);
            GL.GetProgram(programID, GetProgramParameterName.ActiveUniforms, out uniformCount);

            //Gather Attribute Metatdata
            for (int i = 0; i < attributeCount; i++)
            {
                AttributeInfo info = new AttributeInfo();

                GL.GetActiveAttrib(programID, i, 256, out int length, out info.size, out info.type, out info.name);
                info.address = GL.GetAttribLocation(programID, info.name);

                attributes.Add(info.name, info);
            }

            //Gather Uniform Metatdata
            for (int i = 0; i < uniformCount; i++)
            {
                UniformInfo info = new UniformInfo();

                GL.GetActiveUniform(programID, i, 256, out int length, out info.size, out info.type, out info.name);
                info.address = GL.GetUniformLocation(programID, info.name);

                uniforms.Add(info.name, info);
            }

            GL.DetachShader(programID, vertShaderID);
            GL.DeleteShader(vertShaderID);

            GL.DetachShader(programID, fragShaderID);
            GL.DeleteShader(fragShaderID);
        }

        public void Use()
        {
            GL.UseProgram(programID);
        }


        #region Get Locations

        public int GetAttibute(string name)
        {
            if (attributes.ContainsKey(name))
                return attributes[name].address;
            else
                return -1;
        }

        public int GetUniform(string name)
        {
            if (uniforms.ContainsKey(name))
                return uniforms[name].address;
            else
                return -1;
        }
        #endregion

        #region  Write Info Logs
        void WriteShaderInfoLog(int shaderAddress)
        {
            string shaderCompileInfo = GL.GetShaderInfoLog(shaderAddress);
            if (!string.IsNullOrEmpty(shaderCompileInfo))
                Console.WriteLine(shaderCompileInfo);
        }

        void WriteProgramInfoLog(int programID)
        {
            string shaderCompileInfo = GL.GetProgramInfoLog(programID);
            if (!string.IsNullOrEmpty(shaderCompileInfo))
                Console.WriteLine(shaderCompileInfo);
        }
        #endregion

        public void Remove()
        {
            GL.DeleteProgram(programID);
        }
    }
}