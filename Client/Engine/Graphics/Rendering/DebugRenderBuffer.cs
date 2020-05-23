using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using OpenToolkit.Graphics.OpenGL4;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Engine.Graphics.Shading;
using VoxelValley.Client.Engine.SceneGraph.Components;
using VoxelValley.Engine.Mathematics;

namespace VoxelValley.Client.Engine.Graphics.Rendering
{
    public class DebugRenderBuffer : RenderBuffer
    {
        Type type = typeof(DebugRenderBuffer);

        ConcurrentBag<Mesh> meshesToAdd;

        public DebugRenderBuffer(ShaderManager.ShaderType shaderType) : base(shaderType)
        {
            meshesToAdd = new ConcurrentBag<Mesh>();
        }

        public override void Add(Mesh mesh)
        {
            meshesToAdd.Add(mesh);
        }

        void AddMeshToBuffer(Mesh mesh)
        {
            meshes.Add(mesh);

            List<Vector3> vertexData = new List<Vector3>();
            List<int> indiceData = new List<int>();
            List<Vector4b> colorData = new List<Vector4b>();

            int vertexCount = 0;

            foreach (Mesh m in meshes.ToArray())
            {
                vertexData.AddRange(m.GetVertices());
                indiceData.AddRange(m.GetIndices(vertexCount));
                colorData.AddRange(m.GetColors());

                vertexCount += m.VertexCount;
            }

            SendMeshData(vertexData.ToArray(), indiceData.ToArray(), colorData.ToArray());
        }

        void SendMeshData(Vector3[] vertexData, int[] indiceData, Vector4b[] colorData)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, shader.GetBuffer("vPosition"));
            GL.BufferData(BufferTarget.ArrayBuffer, vertexData.Length * Vector3.SizeInBytes, vertexData, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(shader.GetAttibute("vPosition"), 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indiceData.Length * sizeof(int), indiceData, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, shader.GetBuffer("vColor"));
            GL.BufferData(BufferTarget.ArrayBuffer, colorData.Length * Vector4b.SizeInBytes, colorData, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(shader.GetAttibute("vColor"), 4, VertexAttribPointerType.UnsignedByte, false, 0, 0);

            UnbindCurrentBuffer();
        }

        public override void Render()
        {
            if (meshesToAdd.Count > 0 && meshesToAdd.TryTake(out Mesh mesh))
                AddMeshToBuffer(mesh);

            if (meshes.Count == 0)
                return;

            int indiceAt = 0;

            shader.Use();
            GL.BindVertexArray(vertexArrayObject);

            Camera activeCamera = CameraManager.GetActiveCamera();

            Matrix4 viewMatrix = activeCamera.GetViewMatrix();
            Matrix4 projectionMatrix = activeCamera.GetProjectionMatrix();

            foreach (Mesh m in meshes)
            {
                m.CalculateModelMatrix();

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