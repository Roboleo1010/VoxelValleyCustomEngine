using System.Collections.Generic;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Engine.Graphics;

namespace VoxelValley.Client.Game.Enviroment
{
    public class ChunkMesh : Mesh
    {
        Vector3[] vertexData;
        Vector3[] colorData;
        Vector3[] normalData;

        //Just Temporary
        List<Vector3> vertices;
        List<Vector3> colors;
        List<Vector3> normals;
        List<int> indices;

        Chunk parentChunk;

        public ChunkMesh(Chunk parentChunk)
        {
            this.parentChunk = parentChunk;
            ParentGameObject = parentChunk.gameObject;
        }

        public void Create()
        {
            vertices = new List<Vector3>();
            colors = new List<Vector3>();
            normals = new List<Vector3>();
            indices = new List<int>();

            for (int x = 0; x < parentChunk.voxels.GetLength(0); x++)
                for (int z = 0; z < parentChunk.voxels.GetLength(2); z++)
                    for (int y = 0; y < parentChunk.voxels.GetLength(1); y++)
                        if (parentChunk.voxels[x, y, z] != null)
                            GetMeshData(x, y, z);

            vertexData = vertices.ToArray();
            VertexCount = vertices.Count;
            vertices = null;

            colorData = colors.ToArray();
            ColorCount = colors.Count;
            colors = null;

            normalData = normals.ToArray();
            NormalCount = normals.Count;
            normals = null;

            IndiceCount = indices.Count;
        }

        void GetMeshData(int x, int y, int z)
        {
            int addedVertices = 0;
            int indiceOffset = vertices.Count;

            //Left
            if (HasToRenderSide(x, y, z - 1))
            {
                vertices.Add(new Vector3(x + 0, y + 0, z + 0));
                vertices.Add(new Vector3(x + 1, y + 1, z + 0));
                vertices.Add(new Vector3(x + 1, y + 0, z + 0));
                vertices.Add(new Vector3(x + 0, y + 1, z + 0));

                indices.AddRange(new int[] { indiceOffset + 0, indiceOffset + 1, indiceOffset + 2, indiceOffset + 0, indiceOffset + 3, indiceOffset + 1 });
                normals.AddRange(new Vector3[] { -Vector3.UnitZ, -Vector3.UnitZ, -Vector3.UnitZ, -Vector3.UnitZ });

                addedVertices += 4;
                indiceOffset += 4;
            }
            //Right
            if (HasToRenderSide(x, y, z + 1))
            {
                vertices.Add(new Vector3(x + 0, y + 0, z + 1));
                vertices.Add(new Vector3(x + 1, y + 0, z + 1));
                vertices.Add(new Vector3(x + 1, y + 1, z + 1));
                vertices.Add(new Vector3(x + 0, y + 1, z + 1));

                indices.AddRange(new int[] { indiceOffset + 0, indiceOffset + 1, indiceOffset + 2, indiceOffset + 0, indiceOffset + 2, indiceOffset + 3 });
                normals.AddRange(new Vector3[] { Vector3.UnitZ, Vector3.UnitZ, Vector3.UnitZ, Vector3.UnitZ });

                addedVertices += 4;
                indiceOffset += 4;
            }
            //Front
            if (HasToRenderSide(x - 1, y, z))
            {
                vertices.Add(new Vector3(x + 0, y + 0, z + 0));
                vertices.Add(new Vector3(x + 0, y + 1, z + 1));
                vertices.Add(new Vector3(x + 0, y + 1, z + 0));
                vertices.Add(new Vector3(x + 0, y + 0, z + 1));

                indices.AddRange(new int[] { indiceOffset + 0, indiceOffset + 1, indiceOffset + 2, indiceOffset + 0, indiceOffset + 3, indiceOffset + 1 });
                normals.AddRange(new Vector3[] { -Vector3.UnitX, -Vector3.UnitX, -Vector3.UnitX, -Vector3.UnitX });

                addedVertices += 4;
                indiceOffset += 4;
            }
            //Back
            if (HasToRenderSide(x + 1, y, z))
            {
                vertices.Add(new Vector3(x + 1, y + 0, z + 0));
                vertices.Add(new Vector3(x + 1, y + 1, z + 0));
                vertices.Add(new Vector3(x + 1, y + 1, z + 1));
                vertices.Add(new Vector3(x + 1, y + 0, z + 1));

                indices.AddRange(new int[] { indiceOffset + 0, indiceOffset + 1, indiceOffset + 2, indiceOffset + 0, indiceOffset + 2, indiceOffset + 3 });
                normals.AddRange(new Vector3[] { Vector3.UnitX, Vector3.UnitX, Vector3.UnitX, Vector3.UnitX });

                addedVertices += 4;
                indiceOffset += 4;
            }
            //Bottom
            if (HasToRenderSide(x, y - 1, z))
            {
                vertices.Add(new Vector3(x + 0, y + 0, z + 0));
                vertices.Add(new Vector3(x + 1, y + 0, z + 0));
                vertices.Add(new Vector3(x + 1, y + 0, z + 1));
                vertices.Add(new Vector3(x + 0, y + 0, z + 1));

                indices.AddRange(new int[] { indiceOffset + 0, indiceOffset + 1, indiceOffset + 2, indiceOffset + 0, indiceOffset + 2, indiceOffset + 3 });
                normals.AddRange(new Vector3[] { -Vector3.UnitY, -Vector3.UnitY, -Vector3.UnitY, -Vector3.UnitY });

                addedVertices += 4;
                indiceOffset += 4;
            }
            //Top
            if (HasToRenderSide(x, y + 1, z))
            {
                vertices.Add(new Vector3(x + 1, y + 1, z + 0));
                vertices.Add(new Vector3(x + 0, y + 1, z + 0));
                vertices.Add(new Vector3(x + 1, y + 1, z + 1));
                vertices.Add(new Vector3(x + 0, y + 1, z + 1));

                indices.AddRange(new int[] { indiceOffset + 1, indiceOffset + 2, indiceOffset + 0, indiceOffset + 1, indiceOffset + 3, indiceOffset + 2 });
                normals.AddRange(new Vector3[] { Vector3.UnitY, Vector3.UnitY, Vector3.UnitY, Vector3.UnitY });

                addedVertices += 4;
                indiceOffset += 4;
            }

            if (addedVertices > 0)
            {
                Vector3 voxelColor = parentChunk.voxels[x, y, z].Color;
                for (int i = 0; i < addedVertices; i++)
                    colors.Add(voxelColor);
            }
        }
        bool HasToRenderSide(int x, int y, int z)
        {
            if (x < 0 || x >= parentChunk.voxels.GetLength(0) ||
                y < 0 || y >= parentChunk.voxels.GetLength(1) ||
                z < 0 || z >= parentChunk.voxels.GetLength(2))
                return true;

            return parentChunk.voxels[x, y, z] == null;
        }

        public override Vector3[] GetVertices()
        {
            return vertexData;
        }

        public override Vector3[] GetColors()
        {
            return colorData;
        }

        public override Vector3[] GetNormals()
        {
            return normalData;
        }

        public override int[] GetIndices(int offset = 0)
        {
            int[] indis = indices.ToArray(); //FIXME: Aus irgendeinem mir unerklärlichen Grund ist indiceData nicht das gleiche wie indices

            for (int i = 0; i < indis.Length; i++)
                indis[i] += offset;

            return indis;
        }
    }
}