using System;
using OpenToolkit.Graphics.OpenGL4;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Engine.Graphics.Shading;
using VoxelValley.Client.Engine.SceneGraph.Components;
using VoxelValley.Common.Mathematics;
using VoxelValley.Common.Diagnostics;

namespace VoxelValley.Client.Engine.Graphics.Rendering
{
    public class DebugRenderBuffer : RenderBuffer
    {
        Type type = typeof(DebugRenderBuffer);

        public DebugRenderBuffer(ShaderManager.ShaderType shaderType) : base(shaderType)
        {
            //Calculate Space reuqirements
            int averageVerticesPerMesh = 24;
            int meshCount = 200;
            int vertexBufferSize = averageVerticesPerMesh * Vertex.SizeInBytes * meshCount;
            int elementBufferSize = averageVerticesPerMesh * 3 * sizeof(int) * meshCount;

            Log.Info(type, $"Set DebugRB VertexBuffer to { vertexBufferSize / 1000 } KB");
            Log.Info(type, $"Set DebugRB ElementBuffer to { elementBufferSize / 1000 } KB");

            //vertexBufferObject
            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertexBufferSize, IntPtr.Zero, BufferUsageHint.StaticDraw);

            //elementBufferObject
            elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, elementBufferSize, IntPtr.Zero, BufferUsageHint.StaticDraw);

            //vertexArrayObject
            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexArrayObject);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);

            GL.EnableVertexAttribArray(shader.GetAttibute("vPosition"));
            GL.VertexAttribPointer(shader.GetAttibute("vPosition"), 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, 0);

            GL.EnableVertexAttribArray(shader.GetAttibute("vColor"));
            GL.VertexAttribPointer(shader.GetAttibute("vColor"), 4, VertexAttribPointerType.UnsignedByte, false, Vertex.SizeInBytes, Vector3.SizeInBytes);

            GL.EnableVertexAttribArray(shader.GetAttibute("vNormal"));
            GL.VertexAttribPointer(shader.GetAttibute("vNormal"), 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, Vector3.SizeInBytes + Vector4b.SizeInBytes);

            UnbindCurrentBuffer();
        }

        void SendMeshSubData(Vertex[] vertices, int[] indices)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)(vertexOffset * Vertex.SizeInBytes), vertices.Length * Vertex.SizeInBytes, vertices);
            vertexOffset += vertices.Length;

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferSubData(BufferTarget.ElementArrayBuffer, (IntPtr)(indiceOffset * sizeof(int)), indices.Length * sizeof(int), indices);
            indiceOffset += indices.Length;

            UnbindCurrentBuffer();
        }

        public override void Render()
        {
            if (meshesToAdd.Count > 0 && meshesToAdd.TryTake(out Mesh mesh))
            {
                meshes.Add(mesh);
                SendMeshSubData(mesh.GetVertices(), mesh.GetIndices(vertexOffset));
            }

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