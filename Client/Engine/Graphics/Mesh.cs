using OpenToolkit.Mathematics;
using VoxelValley.Common.Diagnostics;
using VoxelValley.Client.Engine.SceneGraph;
using VoxelValley.Engine.Mathematics;

namespace VoxelValley.Client.Engine.Graphics
{
    public abstract class Mesh
    {
        public GameObject ParentGameObject;
        public Matrix4 ModelMatrix = Matrix4.Identity;

        Vector3[] normalData;

        public virtual int VertexCount { get; set; }
        public virtual int IndiceCount { get; set; }
        public virtual int ColorCount { get; set; }
        public virtual int NormalCount { get; set; }

        public abstract Vector3[] GetVertices();

        public abstract int[] GetIndices(int offset = 0);

        public abstract Vector4b[] GetColors();

        public virtual void CalculateModelMatrix()
        {
            if (ParentGameObject == null)
            {
                Log.Error(typeof(Mesh), "No ParentGameObject set in Mesh. Set ModelMatrix to Matrix4.Identity");
                ModelMatrix = Matrix4.Identity;
            }
            else
                ModelMatrix = Matrix4.CreateScale(ParentGameObject.Transform.Scale) *
                              Matrix4.CreateRotationX(ParentGameObject.Transform.Rotation.X) *
                              Matrix4.CreateRotationY(ParentGameObject.Transform.Rotation.Y) *
                              Matrix4.CreateRotationZ(ParentGameObject.Transform.Rotation.Z) *
                              Matrix4.CreateTranslation(ParentGameObject.Transform.Position);
        }

        public virtual Vector3[] GetNormals()
        {
            if (normalData == null)
                CalculateNormals();

            return normalData;
        }

        public void CalculateNormals()
        {
            Vector3[] normals = new Vector3[VertexCount];
            Vector3[] vertices = GetVertices();
            int[] indices = GetIndices();

            //Calculate normals for each Face
            for (int i = 0; i < IndiceCount; i += 3)
            {
                Vector3 v1 = vertices[indices[i]];
                Vector3 v2 = vertices[indices[i + 1]];
                Vector3 v3 = vertices[indices[i + 2]];

                normals[indices[i]] += Vector3.Cross(v2 - v1, v3 - v1);
                normals[indices[i + 1]] += Vector3.Cross(v2 - v1, v3 - v1);
                normals[indices[i + 2]] += Vector3.Cross(v2 - v1, v3 - v1);
            }

            for (int i = 0; i < normals.Length; i++)
                normals[i] = normals[i].Normalized();

            normalData = normals;
        }
    }
}