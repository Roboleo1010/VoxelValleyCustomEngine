using System.Drawing;

namespace VoxelValley.Common.Math
{
    public class Vector3b
    {
        //Values
        byte x;
        byte y;
        byte z;

        //Component shorthands (Coordinates)
        public byte X { get { return x; } private set { x = value; } }
        public byte Y { get { return y; } private set { y = value; } }
        public byte Z { get { return z; } private set { z = value; } }

        //Component shorthands (Colors)
        public byte R { get { return x; } private set { x = value; } }
        public byte G { get { return y; } private set { y = value; } }
        public byte B { get { return z; } private set { z = value; } }

        public static int SizeInBytes { get { return 3; } }
        public static Vector3b Zero { get { return new Vector3b(0, 0, 0); } }
        public static Vector3b One { get { return new Vector3b(1, 1, 1); } }

        public Vector3b(byte x, byte y, byte z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Vector3b(Color color)
        {
            this.R = color.R;
            this.G = color.G;
            this.B = color.B;
        }
    }
}