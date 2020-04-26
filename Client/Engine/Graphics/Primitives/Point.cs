using OpenToolkit.Mathematics;

namespace VoxelValley.Engine.Graphics.Primitives
{
    public class Point : Mesh
    {
        Vector3 color;

        Vector3[] colors;

        public Point(Vector3 color)
        {
            this.color = color;
            VertexCount = 24;
            IndiceCount = 36;
            ColorCount = 24;
        }

        public override Vector3[] GetVertices()
        {
            float scaleFactor = 1 / 10;

            return new Vector3[] {
               //left
                new Vector3(-1 * scaleFactor, -1 * scaleFactor, -1 * scaleFactor),
                new Vector3(1 * scaleFactor, 1 * scaleFactor, -1 * scaleFactor),
                new Vector3(1 * scaleFactor, -1 * scaleFactor, -1 * scaleFactor),
                new Vector3(-1 * scaleFactor, 1 * scaleFactor, -1 * scaleFactor), 
                //back
                new Vector3(1 * scaleFactor, -1 * scaleFactor, -1 * scaleFactor),
                new Vector3(1 * scaleFactor, 1 * scaleFactor, -1 * scaleFactor),
                new Vector3(1 * scaleFactor, 1 * scaleFactor, 1 * scaleFactor),
                new Vector3(1 * scaleFactor, -1 * scaleFactor, 1 * scaleFactor), 
                //right
                new Vector3(-1 * scaleFactor, -1 * scaleFactor, 1 * scaleFactor),
                new Vector3(1 * scaleFactor, -1 * scaleFactor, 1 * scaleFactor),
                new Vector3(1 * scaleFactor, 1 * scaleFactor, 1 * scaleFactor),
                new Vector3(-1 * scaleFactor, 1 * scaleFactor, 1 * scaleFactor), 
                //top
                new Vector3(1 * scaleFactor, 1 * scaleFactor, -1 * scaleFactor),
                new Vector3(-1 * scaleFactor, 1 * scaleFactor, -1 * scaleFactor),
                new Vector3(1 * scaleFactor, 1 * scaleFactor, 1 * scaleFactor),
                new Vector3(-1 * scaleFactor, 1 * scaleFactor, 1 * scaleFactor), 
                //front
                new Vector3(-1 * scaleFactor, -1 * scaleFactor, -1 * scaleFactor),
                new Vector3(-1 * scaleFactor, 1 * scaleFactor, 1 * scaleFactor),
                new Vector3(-1 * scaleFactor, 1 * scaleFactor, -1 * scaleFactor),
                new Vector3(-1 * scaleFactor, -1 * scaleFactor, 1 * scaleFactor), 
                //bottom
                new Vector3(-1 * scaleFactor, -1 * scaleFactor, -1 * scaleFactor),
                new Vector3(1 * scaleFactor, -1 * scaleFactor, -1 * scaleFactor),
                new Vector3(1 * scaleFactor, -1 * scaleFactor, 1 * scaleFactor),
                new Vector3(-1 * scaleFactor, -1 * scaleFactor, 1 * scaleFactor)
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

        public override Vector3[] GetColors()
        {
            if (colors == null)
            {
                colors = new Vector3[ColorCount];
                for (int i = 0; i < ColorCount; i++)
                    colors[i] = color;
            }

            return colors;
        }
    }
}