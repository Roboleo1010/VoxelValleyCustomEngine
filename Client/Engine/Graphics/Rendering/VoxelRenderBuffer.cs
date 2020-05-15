using System;
using System.Collections.Concurrent;
using OpenToolkit.Graphics.OpenGL4;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Engine.Graphics.Shading;
using VoxelValley.Client.Game;

namespace VoxelValley.Client.Engine.Graphics.Rendering
{
    public class VoxelRenderBuffer : RenderBuffer
    {
        Type type = typeof(VoxelRenderBuffer);

        public ConcurrentBag<Mesh> MeshesToAdd;

        public VoxelRenderBuffer(ShaderManager.ShaderType shaderType) : base(shaderType)
        {
            MeshesToAdd = new ConcurrentBag<Mesh>();

            GL.BindBuffer(BufferTarget.ArrayBuffer, shader.GetBuffer("vPosition"));
            GL.BufferData(BufferTarget.ArrayBuffer, 1000000000, IntPtr.Zero, BufferUsageHint.StaticDraw); //1GB

            GL.BindBuffer(BufferTarget.ArrayBuffer, shader.GetBuffer("vNormal"));
            GL.BufferData(BufferTarget.ArrayBuffer, 1000000000, IntPtr.Zero, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, shader.GetBuffer("vColor"));
            GL.BufferData(BufferTarget.ArrayBuffer, 1000000000, IntPtr.Zero, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, 1000000000, IntPtr.Zero, BufferUsageHint.StaticDraw);

            UnbindCurrentBuffer();
        }

        public void AddMesh(Mesh mesh)
        {
            SendMeshSubData(mesh.GetVertices(),
                            mesh.GetIndices(vertexOffset),
                            mesh.GetNormals(),
                            mesh.GetColors());
        }

        public void SendMeshSubData(Vector3[] vertexData, int[] indiceData, Vector3[] normalData, Vector3[] colorData)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, shader.GetBuffer("vPosition"));
            GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)vertexOffsetInBytes, vertexData.Length * Vector3.SizeInBytes, vertexData);
            GL.VertexAttribPointer(shader.GetAttibute("vPosition"), 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, shader.GetBuffer("vNormal"));
            GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)vertexOffsetInBytes, normalData.Length * Vector3.SizeInBytes, normalData);
            GL.VertexAttribPointer(shader.GetAttibute("vNormal"), 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, shader.GetBuffer("vColor"));
            GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)vertexOffsetInBytes, colorData.Length * Vector3.SizeInBytes, colorData);
            GL.VertexAttribPointer(shader.GetAttibute("vColor"), 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferSubData(BufferTarget.ElementArrayBuffer, (IntPtr)indiceOffsetInBytes, indiceData.Length * sizeof(int), indiceData);

            UnbindCurrentBuffer();

            vertexOffsetInBytes += vertexData.Length * Vector3.SizeInBytes;
            vertexOffset += vertexData.Length;
            indiceOffsetInBytes += indiceData.Length * sizeof(int);
            indiceOffset += indiceData.Length;
        }

        public override void Render()
        {
            if (MeshesToAdd.Count > 0 && MeshesToAdd.TryTake(out Mesh mesh))
            {
                AddMesh(mesh);
                meshes.Add(mesh);
            }

            if (meshes.Count == 0)
                return;

            int indiceAt = 0;

            shader.Use();
            GL.BindVertexArray(vertexArrayObject);

            //TODO: Move out
            Matrix4 viewMatrix = GameManager.ActiveCamera.GetViewMatrix();
            Matrix4 projectionMatrix = GameManager.ActiveCamera.GetProjectionMatrix();

            CalculateLighting();

            foreach (Mesh m in meshes)
            {
                m.CalculateModelMatrix();

                GL.Uniform3(shader.GetUniform("viewPos"), GameManager.ActiveCamera.ParentGameObject.Transform.Position);

                GL.UniformMatrix4(shader.GetUniform("model"), false, ref m.ModelMatrix);
                GL.UniformMatrix4(shader.GetUniform("view"), false, ref viewMatrix);
                GL.UniformMatrix4(shader.GetUniform("projection"), false, ref projectionMatrix);

                GL.DrawElements(PrimitiveType.Triangles, m.IndiceCount, DrawElementsType.UnsignedInt, indiceAt * sizeof(uint));
                indiceAt += m.IndiceCount;
            }

            UnbindCurrentBuffer();
        }
    }
}