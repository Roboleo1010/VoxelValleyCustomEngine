using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using OpenToolkit.Graphics.OpenGL4;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Engine.Graphics.Shading;

namespace VoxelValley.Client.Engine.Graphics.Rendering
{
    public abstract class RenderBuffer
    {
        Type type = typeof(RenderBuffer);

        internal int vertexBufferObject = -1;
        internal int vertexArrayObject = -1;
        internal int elementBufferObject = -1;

        internal int vertexOffset = 0;
        internal int indiceOffset = 0;

        internal Shader shader;

        internal List<Mesh> meshes = new List<Mesh>();
        internal ConcurrentBag<Mesh> meshesToAdd;
        internal ConcurrentBag<Mesh> meshesToRemove;

        public RenderBuffer(ShaderManager.ShaderType shaderType)
        {
            this.shader = ShaderManager.GetShader(shaderType);
            
            meshesToAdd = new ConcurrentBag<Mesh>();
            meshesToRemove = new ConcurrentBag<Mesh>();
        }

        internal void AddMesh(Mesh mesh)
        {
            meshesToAdd.Add(mesh);
        }

        internal void RemoveMesh(Mesh mesh)
        {
            meshesToRemove.Add(mesh);
        }

        public abstract void Render();

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

        public virtual void Remove()
        {
            GL.DeleteBuffers(3, new int[] { vertexArrayObject, vertexBufferObject, elementBufferObject });
        }
    }
}