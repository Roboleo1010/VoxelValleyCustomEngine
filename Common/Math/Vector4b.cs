using System.Drawing;

namespace VoxelValley.Common.Math
{
    public class Vector4b
    {
        //Values
        byte x;
        byte y;
        byte z;
        byte w;

        //Component shorthands (Coordinates)
        public byte X { get { return x; } private set { x = value; } }
        public byte Y { get { return y; } private set { y = value; } }
        public byte Z { get { return z; } private set { z = value; } }
        public byte W { get { return w; } private set { w = value; } }

        //Component shorthands (Colors)
        public byte R { get { return x; } private set { x = value; } }
        public byte G { get { return y; } private set { y = value; } }
        public byte B { get { return z; } private set { z = value; } }
        public byte A { get { return w; } private set { w = value; } }

        public static int SizeInBytes { get { return 4; } }
        public static Vector4b Zero { get { return new Vector4b(0, 0, 0, 0); } }
        public static Vector4b One { get { return new Vector4b(1, 1, 1, 1); } }

        public Vector4b(byte x, byte y, byte z, byte w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        public Vector4b(Color color)
        {
            this.R = color.R;
            this.G = color.G;
            this.B = color.B;
            this.A = color.A;
        }
    }
}