using System;
using System.Collections.Generic;
using OpenToolkit.Mathematics;
using OpenToolkit.Graphics.OpenGL;
using VoxelValley.Client.Engine.Graphics.Shading;
using VoxelValley.Client.Game;
using VoxelValley.Common.SceneGraph.Components;

namespace VoxelValley.Client.Engine.Graphics.Rendering
{
    public class RenderBuffer
    {
        Type type = typeof(RenderBuffer);
        int ibo_elements = -1;
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
            ibo_elements = GL.GenBuffer();
            this.shader = shader;
            meshes = new List<Mesh>();
            meshesToAdd = new List<Mesh>();
            meshesToRemove = new List<Mesh>();
        }

        internal void AddMesh(Mesh mesh) //TODO Auslagern
        {
            meshesToAdd.Add(mesh);
        }

        internal void RemoveMesh(Mesh mesh) //TODO Auslagern
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

        void RecalculateMeshes()
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

            vertexData = vertices.ToArray(); //TODO Performace Just Update Changing Buffers
            indiceData = indices.ToArray();
            colorData = colors.ToArray();
            normalData = normals.ToArray();
        }

        public void Prepare()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, shader.GetBuffer("vPosition"));
            GL.BufferData(BufferTarget.ArrayBuffer, vertexData.Length * Vector3.SizeInBytes, vertexData, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(shader.GetAttibute("vPosition"), 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, shader.GetBuffer("vColor"));
            GL.BufferData(BufferTarget.ArrayBuffer, colorData.Length * Vector3.SizeInBytes, colorData, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(shader.GetAttibute("vColor"), 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, shader.GetBuffer("vNormal"));
            GL.BufferData(BufferTarget.ArrayBuffer, normalData.Length * Vector3.SizeInBytes, normalData, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(shader.GetAttibute("vNormal"), 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo_elements);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indiceData.Length * sizeof(int), indiceData, BufferUsageHint.StaticDraw);

            shader.Use();

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); //Unbind current buffer
        }

        public void Render(float aspect)
        {
            if (meshes.Count == 0)
                return;

            shader.EnableVertexAttribArrays();

            int indiceAt = 0;

            Camera camera = ReferencePointer.Player.GetCamera();

            Matrix4 viewProjectionMatrix = camera.GetViewMatrix() * Matrix4.CreatePerspectiveFieldOfView(1.3f, aspect, camera.NearClippingPane, camera.FarClippingPane);

            foreach (Mesh m in meshes)
            {
                m.CalculateModelMatrix();
                m.ViewProjectionMatrix = viewProjectionMatrix;
                m.ModelViewProjectionMatrix = m.ModelMatrix * m.ViewProjectionMatrix;

                GL.UniformMatrix4(shader.GetUniform("modelview"), false, ref m.ModelViewProjectionMatrix);

                GL.DrawElements(PrimitiveType.Triangles, m.IndiceCount, DrawElementsType.UnsignedInt, indiceAt * sizeof(uint));
                indiceAt += m.IndiceCount;
            }

            shader.DisableVertexAttribArrays();
        }

        public void Remove()
        {
            GL.DeleteBuffer(ibo_elements);
        }
    }
}