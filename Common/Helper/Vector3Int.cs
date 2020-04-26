using OpenToolkit.Mathematics;

namespace VoxelValley.Engine.Core.Helper
{
    public struct Vector3Int
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Vector3Int(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3Int(Vector3 vector)
        {
            X = (int)vector.X;
            Y = (int)vector.Y;
            Z = (int)vector.Z;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(X, Y, Z);
        }

        public Vector3Int(string s)
        {
            s.Split('_');

            X = s[0];
            Y = s[1];
            Z = s[2];
        }

        public override string ToString()
        {
            return string.Format("X:{0} Y:{1} Z:{2}", X, Y, Z);
        }

        // public static bool operator !=(Vector3Int vec1, Vector3Int vec2)
        // {
        //     return vec1.X != vec2.X || vec1.Y != vec2.Y || vec1.Z != vec2.Z;
        // }

        // public static bool operator ==(Vector3Int vec1, Vector3Int vec2)
        // {
        //     return vec1.X == vec2.X && vec1.Y == vec2.Y && vec1.Z == vec2.Z;
        // }
    }
}