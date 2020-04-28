using System;
using System.Collections.Generic;
using OpenToolkit.Mathematics;
using OpenToolkit.Graphics.OpenGL4;
using VoxelValley.Client.Engine.Graphics.Shading;
using VoxelValley.Client.Game;

namespace VoxelValley.Client.Engine.Graphics.Rendering
{
    public class RenderBuffer
    {
        Type type = typeof(RenderBuffer);

        int elementBufferObject = -1;
        int vertexArrayObject = -1;

        List<Mesh> meshes;
        Shader shader;

        //Mesh Data
        Vector3[] vertexData = new Vector3[0];
        int[] indiceData = new int[0];
        Vector3[] colorData = new Vector3[0];
        Vector3[] normalData = new Vector3[0];

        List<Mesh> meshesToAdd;
        List<Mesh> meshesToRemove;

        public RenderBuffer(Shader shader)
        {
            this.shader = shader;

            elementBufferObject = GL.GenBuffer();
            vertexArrayObject = GL.GenVertexArray();

            GL.BindVertexArray(vertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            shader.EnableVertexAttribArrays();

            meshes = new List<Mesh>();
            meshesToAdd = new List<Mesh>();
            meshesToRemove = new List<Mesh>();
        }

        #region  Mesh Building
        internal void AddMesh(Mesh mesh)
        {
            meshesToAdd.Add(mesh);
        }

        internal void RemoveMesh(Mesh mesh)
        {
            meshesToRemove.Add(mesh);
        }

        public void UpdateMeshes()
        {
            if (meshesToRemove.Count > 0 || meshesToAdd.Count > 0)
            {
                while (meshesToRemove.Count > 0)
                {
                    meshes.Add(meshesToRemove[0]);
                    meshesToRemove.RemoveAt(0);
                }

                while (meshesToAdd.Count > 0)
                {
                    meshes.Add(meshesToAdd[0]);
                    meshesToAdd.RemoveAt(0);
                }

                RecalculateMeshes();
            }
        }

        void RecalculateMeshes() //TODO: Optimize!
        {
            List<Vector3> vertices = new List<Vector3>();
            List<int> indices = new List<int>();
            List<Vector3> colors = new List<Vector3>();
            List<Vector3> normals = new List<Vector3>();

            int vertexCount = 0;

            foreach (Mesh m in meshes)
            {
                vertices.AddRange(m.GetVertices());
                indices.AddRange(m.GetIndices(vertexCount));
                colors.AddRange(m.GetColors());
                normals.AddRange(m.GetNormals());

                vertexCount += m.VertexCount;
            }

            vertexData = vertices.ToArray();
            indiceData = indices.ToArray();
            colorData = colors.ToArray();
            normalData = normals.ToArray();

            Prepare();
        }
        #endregion

        public void Prepare()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, shader.GetBuffer("vPosition"));
            GL.BufferData(BufferTarget.ArrayBuffer, vertexData.Length * Vector3.SizeInBytes, vertexData, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(shader.GetAttibute("vPosition"), 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, shader.GetBuffer("vColor"));
            GL.BufferData(BufferTarget.ArrayBuffer, colorData.Length * Vector3.SizeInBytes, colorData, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(shader.GetAttibute("vColor"), 3, VertexAttribPointerType.Float, false, 0, 0);

            // GL.BindBuffer(BufferTarget.ArrayBuffer, shader.GetBuffer("vNormal"));
            // GL.BufferData(BufferTarget.ArrayBuffer, normalData.Length * Vector3.SizeInBytes, normalData, BufferUsageHint.StaticDraw);
            // GL.VertexAttribPointer(shader.GetAttibute("vNormal"), 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indiceData.Length * sizeof(int), indiceData, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); //Unbind current buffer
        }

        public void Render()
        {
            if (meshes.Count == 0)
                return;

            shader.Use();
            GL.BindVertexArray(vertexArrayObject);

            int indiceAt = 0;

            Matrix4 viewMatrix = GameManager.ActiveCamera.GetViewMatrix();
            Matrix4 projectionMatrix = GameManager.ActiveCamera.GetProjectionMatrix();

            foreach (Mesh m in meshes)
            {
                m.CalculateModelMatrix();

                GL.UniformMatrix4(shader.GetUniform("model"), false, ref m.ModelMatrix);
                GL.UniformMatrix4(shader.GetUniform("view"), false, ref viewMatrix);
                GL.UniformMatrix4(shader.GetUniform("projection"), false, ref projectionMatrix);

                GL.DrawElements(PrimitiveType.Triangles, m.IndiceCount, DrawElementsType.UnsignedInt, indiceAt * sizeof(uint));
                indiceAt += m.IndiceCount;
            }

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); //Unbind current buffer
        }

        public void Remove()
        {
            GL.DeleteBuffer(elementBufferObject);
            GL.DeleteBuffer(vertexArrayObject);
        }
    }
}