using System;
using System.Collections.Generic;
using VoxelValley.Client.Engine.Graphics.Shading;
using OpenToolkit.Graphics.OpenGL4;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Game;
using VoxelValley.Client.Engine.SceneGraph.Components;

namespace VoxelValley.Client.Engine.Graphics.Rendering
{
    public class MeshRenderBuffer : RenderBuffer
    {
        Type type = typeof(RenderBuffer);

        List<Mesh> meshesToAdd = new List<Mesh>();
        List<Mesh> meshesToRemove = new List<Mesh>();

        List<Vector3> vertexData = new List<Vector3>();
        List<int> indiceData = new List<int>();
        List<Vector3> normalData = new List<Vector3>();
        List<Vector3> colorData = new List<Vector3>();

        int vertexCount = 0;

        public MeshRenderBuffer(ShaderManager.ShaderType shaderType) : base(shaderType) { }

        public void OnMeshChanged(MeshRenderer meshRenderer, Mesh oldMesh, Mesh newMesh)
        {
            if (oldMesh == null && newMesh != null) //Mesh added
            {
                meshesToAdd.Add(newMesh);
            }
            else if (oldMesh != null && newMesh != null && oldMesh != newMesh) //Mesh changed
            {
                meshesToRemove.Add(oldMesh);
                meshesToAdd.Add(newMesh);
            }
            else if (oldMesh != null && newMesh == null) //Mesh removed
            {
                meshesToRemove.Add(oldMesh);
            }
        }

        void UpdateMeshes()
        {
            Mesh[] meshesToAddCopy = meshesToAdd.ToArray();
            Mesh[] meshesToRemoveCopy = meshesToRemove.ToArray();

            if (meshesToAdd.Count > 0)
                foreach (Mesh m in meshesToAddCopy)
                {
                    meshes.Add(m);
                    meshesToAdd.Remove(m);
                }

            if (meshesToRemove.Count > 0)
                foreach (Mesh m in meshesToRemoveCopy)
                {
                    meshes.Add(m);
                    meshesToRemove.Remove(m);
                }

            if (meshesToAddCopy.Length > 0 || meshesToRemoveCopy.Length > 0)
                RecalculateMeshes(meshesToAddCopy, meshesToRemoveCopy);
        }

        void RecalculateMeshes(Mesh[] meshesAdded, Mesh[] meshesRemoved)
        {
            if (meshes == null)
                return;

            if (meshesAdded.Length > 0 && meshesRemoved.Length == 0) //Just mehes added, none removed. Smart Recalculating
            {
                foreach (Mesh m in meshesAdded)
                {
                    vertexData.AddRange(m.GetVertices());
                    indiceData.AddRange(m.GetIndices(vertexCount));
                    normalData.AddRange(m.GetNormals());
                    colorData.AddRange(m.GetColors());

                    vertexCount += m.VertexCount;
                }
            }
            else
            {
                vertexData.Clear();
                indiceData.Clear();
                normalData.Clear();
                colorData.Clear();

                vertexCount = 0;

                foreach (Mesh m in meshes.ToArray())
                {
                    vertexData.AddRange(m.GetVertices());
                    indiceData.AddRange(m.GetIndices(vertexCount));
                    normalData.AddRange(m.GetNormals());
                    colorData.AddRange(m.GetColors());

                    vertexCount += m.VertexCount;
                }
            }

            SendMeshData();
        }

        public override void SendMeshData()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, shader.GetBuffer("vPosition"));
            GL.BufferData(BufferTarget.ArrayBuffer, vertexData.Count * Vector3.SizeInBytes, vertexData.ToArray(), BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(shader.GetAttibute("vPosition"), 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, shader.GetBuffer("vNormal"));
            GL.BufferData(BufferTarget.ArrayBuffer, normalData.Count * Vector3.SizeInBytes, normalData.ToArray(), BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(shader.GetAttibute("vNormal"), 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indiceData.Count * sizeof(int), indiceData.ToArray(), BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, shader.GetBuffer("vColor"));
            GL.BufferData(BufferTarget.ArrayBuffer, colorData.Count * Vector3.SizeInBytes, colorData.ToArray(), BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(shader.GetAttibute("vColor"), 3, VertexAttribPointerType.Float, false, 0, 0);

            UnbindCurrentBuffer();
        }

        public override void Render()
        {
            UpdateMeshes();

            if (meshes.Count == 0)
                return;

            shader.Use();
            GL.BindVertexArray(vertexArrayObject);

            int indiceAt = 0;

            Matrix4 viewMatrix = GameManager.ActiveCamera.GetViewMatrix(); //TODO: Move out
            Matrix4 projectionMatrix = GameManager.ActiveCamera.GetProjectionMatrix(); //TODO: Move out

            GL.Uniform3(shader.GetUniform("directionalLight.direction"), new Vector3(-0.2f, -1.0f, -0.3f)); //TODO: Move out
            GL.Uniform3(shader.GetUniform("directionalLight.ambient"), new Vector3(0.8f, 0.8f, 0.8f)); //TODO: Move out
            GL.Uniform3(shader.GetUniform("directionalLight.diffuse"), new Vector3(0.4f, 0.4f, 0.4f)); //TODO: Move out
            GL.Uniform3(shader.GetUniform("directionalLight.specular"), new Vector3(0.05f, 0.05f, 0.05f)); //TODO: Move out

            GL.Uniform1(shader.GetUniform("numPointLights"), 0);
            GL.Uniform1(shader.GetUniform("numSpotLights"), 0);

            foreach (Mesh m in meshes)
            {
                m.CalculateModelMatrix();

                GL.Uniform3(shader.GetUniform("viewPos"), GameManager.ActiveCamera.ParentGameObject.Transform.Position); //TODO: Move out

                GL.UniformMatrix4(shader.GetUniform("model"), false, ref m.ModelMatrix);
                GL.UniformMatrix4(shader.GetUniform("view"), false, ref viewMatrix); //TODO: Move out
                GL.UniformMatrix4(shader.GetUniform("projection"), false, ref projectionMatrix); //TODO: Move out

                GL.DrawElements(PrimitiveType.Triangles, m.IndiceCount, DrawElementsType.UnsignedInt, indiceAt * sizeof(uint));
                indiceAt += m.IndiceCount;
            }

            UnbindCurrentBuffer();
        }
    }
}