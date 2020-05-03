using System;
using System.Collections.Generic;
using OpenToolkit.Graphics.OpenGL4;
using VoxelValley.Client.Engine.Graphics.Shading;

namespace VoxelValley.Client.Engine.Graphics.Rendering
{
    public abstract class RenderBuffer
    {
        Type type = typeof(RenderBuffer);

        internal int elementBufferObject = -1;
        internal int vertexArrayObject = -1;
        internal Shader shader;
        internal List<Mesh> meshes = new List<Mesh>();

        public RenderBuffer(Shader shader)
        {
            this.shader = shader;

            elementBufferObject = GL.GenBuffer();
            vertexArrayObject = GL.GenVertexArray();

            GL.BindVertexArray(vertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            shader.EnableVertexAttribArrays();
        }

        public abstract void Prepare();

        public abstract void Render();

        public void Remove()
        {
            GL.DeleteBuffer(elementBufferObject);
            GL.DeleteBuffer(vertexArrayObject);
        }

        internal void UnbindCurrentBuffer()
        {
             GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}