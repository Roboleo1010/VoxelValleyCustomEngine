using System;
using System.Collections.Generic;
using OpenToolkit.Graphics.OpenGL4;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Engine.Graphics.Shading;

namespace VoxelValley.Client.Engine.Graphics.Rendering
{
    public abstract class RenderBuffer
    {
        Type type = typeof(RenderBuffer);

        //Buffer indices
        internal int elementBufferObject = -1;
        internal int vertexArrayObject = -1;

        internal Shader shader;
        internal List<Mesh> meshes = new List<Mesh>();

        public RenderBuffer(ShaderManager.ShaderType shaderType)
        {
            this.shader = ShaderManager.GetShader(shaderType);

            elementBufferObject = GL.GenBuffer();
            vertexArrayObject = GL.GenVertexArray();

            GL.BindVertexArray(vertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            shader.EnableVertexAttribArrays();
        }

        public abstract void Add(Mesh mesh);

        public abstract void Render();

        public void Remove()
        {
            GL.DeleteBuffer(elementBufferObject);
            GL.DeleteBuffer(vertexArrayObject);
        }

        internal virtual void CalculateLighting()
        {
            GL.Uniform3(shader.GetUniform("directionalLight.direction"), new Vector3(-0.2f, -1.0f, -0.3f));
            GL.Uniform3(shader.GetUniform("directionalLight.ambient"), new Vector3(0.8f, 0.8f, 0.8f));
            GL.Uniform3(shader.GetUniform("directionalLight.diffuse"), new Vector3(0.4f, 0.4f, 0.4f));
            GL.Uniform3(shader.GetUniform("directionalLight.specular"), new Vector3(0.05f, 0.05f, 0.05f));

            GL.Uniform1(shader.GetUniform("numPointLights"), 0);
            GL.Uniform1(shader.GetUniform("numSpotLights"), 0);
        }

        internal void UnbindCurrentBuffer()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}