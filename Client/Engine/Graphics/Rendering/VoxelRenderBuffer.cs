using System;
using System.Collections.Concurrent;
using OpenToolkit.Graphics.OpenGL4;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Engine.Graphics.Shading;
using VoxelValley.Client.Engine.SceneGraph.Components;
using VoxelValley.Client.Game;
using VoxelValley.Client.Game.Graphics;
using VoxelValley.Common.Mathematics;

namespace VoxelValley.Client.Engine.Graphics.Rendering
{
    public class VoxelRenderBuffer : RenderBuffer
    {
        Type type = typeof(VoxelRenderBuffer);

        int vPositionOffset;
        int vNormalOffset;
        int vColorOffset;
        int indiceOffset;

        ConcurrentBag<Mesh> meshesToAdd;

        public VoxelRenderBuffer(ShaderManager.ShaderType shaderType) : base(shaderType)
        {
            meshesToAdd = new ConcurrentBag<Mesh>();

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

        public override void Add(Mesh mesh)
        {
            meshesToAdd.Add(mesh);
        }

        void AddMeshToBuffer(Mesh mesh)
        {
            meshes.Add(mesh);

            SendMeshSubData(mesh.GetVertices(),
                            mesh.GetIndices(vPositionOffset),
                            mesh.GetNormals(),
                            mesh.GetColors());
        }

        void SendMeshSubData(Vector3[] vertexData, int[] indiceData, Vector3[] normalData, Vector4b[] colorData)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, shader.GetBuffer("vPosition"));
            GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)(vPositionOffset * Vector3.SizeInBytes), vertexData.Length * Vector3.SizeInBytes, vertexData);
            GL.VertexAttribPointer(shader.GetAttibute("vPosition"), 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, shader.GetBuffer("vNormal"));
            GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)(vNormalOffset * Vector3.SizeInBytes), normalData.Length * Vector3.SizeInBytes, normalData);
            GL.VertexAttribPointer(shader.GetAttibute("vNormal"), 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, shader.GetBuffer("vColor"));
            GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)(vColorOffset * Vector4b.SizeInBytes), colorData.Length * Vector4b.SizeInBytes, colorData);
            GL.VertexAttribPointer(shader.GetAttibute("vColor"), 4, VertexAttribPointerType.UnsignedByte, false, 0, 0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferSubData(BufferTarget.ElementArrayBuffer, (IntPtr)(indiceOffset * sizeof(int)), indiceData.Length * sizeof(int), indiceData);

            UnbindCurrentBuffer();

            vPositionOffset += vertexData.Length;
            vNormalOffset += normalData.Length;
            vColorOffset += colorData.Length;
            indiceOffset += indiceData.Length;
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

           CalculateLighting();

            foreach (Mesh m in meshes)
            {
                m.CalculateModelMatrix();

                GL.Uniform3(shader.GetUniform("viewPos"), activeCamera.ParentGameObject.Transform.Position);

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