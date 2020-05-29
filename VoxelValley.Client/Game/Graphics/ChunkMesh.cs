using System.Collections.Generic;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Engine.Graphics;
using VoxelValley.Common.Enviroment;
using VoxelValley.Common.Mathematics;

namespace VoxelValley.Client.Game.Enviroment
{
    public class ChunkMesh : Mesh
    {
        Vector3[] vertexData;
        Vector4b[] colorData;
        Vector3[] normalData;

        //Just Temporary
        List<Vector3> vertices;
        List<Vector4b> colors;
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
            colors = new List<Vector4b>();
            normals = new List<Vector3>();
            indices = new List<int>();

            for (int x = 0; x < parentChunk.voxels.GetLength(0); x++)
                for (int z = 0; z < parentChunk.voxels.GetLength(2); z++)
                    for (int y = 0; y < parentChunk.voxels.GetLength(1); y++)
                        if (parentChunk.voxels[x, y, z] != VoxelManager.AirVoxel)
                            GetMeshData(x, y, z);

            vertexData = vertices.ToArray();
            PositionCount = vertices.Count;
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
            if (IsSolid(x, y, z - 1))
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
            if (IsSolid(x, y, z + 1))
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
            if (IsSolid(x - 1, y, z))
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
            if (IsSolid(x + 1, y, z))
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
            if (IsSolid(x, y - 1, z))
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
            if (IsSolid(x, y + 1, z))
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
                Vector4b voxelColor = VoxelManager.GetVoxel(parentChunk.voxels[x, y, z]).Color;
                for (int i = 0; i < addedVertices; i++)
                    colors.Add(voxelColor);
            }
        }
        bool IsSolid(int x, int y, int z)
        {
            if (x < 0 || x >= parentChunk.voxels.GetLength(0) ||
                y < 0 || y >= parentChunk.voxels.GetLength(1) ||
                z < 0 || z >= parentChunk.voxels.GetLength(2))
                return true;

            return parentChunk.voxels[x, y, z] == VoxelManager.AirVoxel;
        }

        public override Vector3[] GetPositions()
        {
            return vertexData;
        }

        public override Vector4b[] GetColors()
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