using System.Drawing;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Engine.Graphics.Rendering;
using VoxelValley.Client.Engine.Graphics.Shading;
using VoxelValley.Client.Engine.SceneGraph;
using VoxelValley.Engine.Mathematics;

namespace VoxelValley.Client.Engine.Graphics.Primitives
{
    public class Cube : Mesh
    {
        Vector4b[] colors;
        Vector3 scale;

        public Cube(Color color, Vector3 scale, GameObject parent)
        {
            VertexCount = 24;
            IndiceCount = 36;
            ColorCount = 24;

            colors = new Vector4b[ColorCount];
            for (int i = 0; i < ColorCount; i++)
                colors[i] = new Vector4b(color);

            this.scale = scale;
            ParentGameObject = parent;

            ((VoxelRenderBuffer)RenderBufferManager.GetBuffer(ShaderManager.ShaderType.VOXEL)).Add(this);
        }

        public override Vector3[] GetVertices()
        {
            return new Vector3[] {
                //left
                new Vector3(0 * scale.X, 0 * scale.Y, 0 * scale.Z),
                new Vector3(1 * scale.X, 1 * scale.Y, 0 * scale.Z),
                new Vector3(1 * scale.X, 0 * scale.Y, 0 * scale.Z),
                new Vector3(0 * scale.X, 1 * scale.Y, 0 * scale.Z), 
                //back
                new Vector3(1 * scale.X, 0 * scale.Y, 0 * scale.Z),
                new Vector3(1 * scale.X, 1 * scale.Y, 0 * scale.Z),
                new Vector3(1 * scale.X, 1 * scale.Y, 1 * scale.Z),
                new Vector3(1 * scale.X, 0 * scale.Y, 1 * scale.Z), 
                //right
                new Vector3(0 * scale.X, 0 * scale.Y, 1 * scale.Z),
                new Vector3(1 * scale.X, 0 * scale.Y, 1 * scale.Z),
                new Vector3(1 * scale.X, 1 * scale.Y, 1 * scale.Z),
                new Vector3(0 * scale.X, 1 * scale.Y, 1 * scale.Z), 
                //top
                new Vector3(1 * scale.X, 1 * scale.Y, 0 * scale.Z),
                new Vector3(0 * scale.X, 1 * scale.Y, 0 * scale.Z),
                new Vector3(1 * scale.X, 1 * scale.Y, 1 * scale.Z),
                new Vector3(0 * scale.X, 1 * scale.Y, 1 * scale.Z), 
                //front
                new Vector3(0 * scale.X, 0 * scale.Y, 0 * scale.Z),
                new Vector3(0 * scale.X, 1 * scale.Y, 1 * scale.Z),
                new Vector3(0 * scale.X, 1 * scale.Y, 0 * scale.Z),
                new Vector3(0 * scale.X, 0 * scale.Y, 1 * scale.Z), 
                //bottom
                new Vector3(0 * scale.X, 0 * scale.Y, 0 * scale.Z),
                new Vector3(1 * scale.X, 0 * scale.Y, 0 * scale.Z),
                new Vector3(1 * scale.X, 0 * scale.Y, 1 * scale.Z),
                new Vector3(0 * scale.X, 0 * scale.Y, 1 * scale.Z)
                };
        }

        public override int[] GetIndices(int offset = 0)
        {
            int[] indices = new int[]{
                //left
                0,1,2,0,3,1, 
                //back
                4,5,6,4,6,7,
                //right
                8,9,10,8,10,11, 
                //top
                13,14,12,13,15,14, 
                //front
                16,17,18,16,19,17, 
                //bottom
                20,21,22,20,22,23
            };

            if (offset != 0)
                for (int i = 0; i < indices.Length; i++)
                    indices[i] += offset;

            return indices;
        }

        public override Vector4b[] GetColors()
        {
            return colors;
        }
    }
}