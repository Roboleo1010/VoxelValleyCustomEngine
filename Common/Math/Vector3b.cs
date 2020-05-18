using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace VoxelValley.Common.Mathematics
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3b
    {
        //Values
        byte _x;
        byte _y;
        byte _z;

        //Component shorthands (Coordinates)
        public byte X { get { return _x; } private set { _x = value; } }
        public byte Y { get { return _y; } private set { _y = value; } }
        public byte Z { get { return _z; } private set { _z = value; } }

        //Component shorthands (Colors)
        public byte R { get { return _x; } private set { _x = value; } }
        public byte G { get { return _y; } private set { _y = value; } }
        public byte B { get { return _z; } private set { _z = value; } }

        public static readonly int SizeInBytes = Marshal.SizeOf<Vector3b>();

        public static Vector3b Zero { get { return new Vector3b(0, 0, 0); } }
        public static Vector3b One { get { return new Vector3b(1, 1, 1); } }

        public Vector3b(byte x, byte y, byte z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public Vector3b(Color color)
        {
            _x = color.R;
            _y = color.G;
            _z = color.B;
        }
    }
}