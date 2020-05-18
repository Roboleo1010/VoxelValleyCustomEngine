using System.Drawing;
using OpenToolkit.Mathematics;
using VoxelValley.Common.Mathematics;

namespace VoxelValley.Client.Engine.Graphics.Primitives
{
    public class Cube : Mesh
    {
        Vector4b color;
        Vector4b[] colors;

        public Cube(Color color)
        {
            this.color = new Vector4b(color);
            VertexCount = 24;
            IndiceCount = 36;
            ColorCount = 24;
        }

        public override Vector3[] GetVertices()
        {
            return new Vector3[] {
                //left
                new Vector3(0, 0, 0),
                new Vector3(1, 1, 0),
                new Vector3(1, 0, 0),
                new Vector3(0, 1, 0), 
                //back
                new Vector3(1, 0, 0),
                new Vector3(1, 1, 0),
                new Vector3(1, 1, 1),
                new Vector3(1, 0, 1), 
                //right
                new Vector3(0, 0, 1),
                new Vector3(1, 0, 1),
                new Vector3(1, 1, 1),
                new Vector3(0, 1, 1), 
                //top
                new Vector3(1, 1, 0),
                new Vector3(0, 1, 0),
                new Vector3(1, 1, 1),
                new Vector3(0, 1, 1), 
                //front
                new Vector3(0, 0, 0),
                new Vector3(0, 1, 1),
                new Vector3(0, 1, 0),
                new Vector3(0, 0, 1), 
                //bottom
                new Vector3(0, 0, 0),
                new Vector3(1, 0, 0),
                new Vector3(1, 0, 1),
                new Vector3(0, 0, 1)
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
            if (colors == null)
            {
                colors = new Vector4b[ColorCount];
                for (int i = 0; i < ColorCount; i++)
                    colors[i] = color;
            }

            return colors;
        }
    }
}