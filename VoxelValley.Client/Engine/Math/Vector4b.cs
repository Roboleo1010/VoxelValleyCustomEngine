using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace VoxelValley.Engine.Mathematics
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector4b
    {
        //Values
        byte _x;
        byte _y;
        byte _z;
        byte _w;

        //Component shorthands (Coordinates)
        public byte X { get { return _x; } private set { _x = value; } }
        public byte Y { get { return _y; } private set { _y = value; } }
        public byte Z { get { return _z; } private set { _z = value; } }
        public byte W { get { return _w; } private set { _w = value; } }

        //Component shorthands (Colors)
        public byte R { get { return _x; } private set { _x = value; } }
        public byte G { get { return _y; } private set { _y = value; } }
        public byte B { get { return _z; } private set { _z = value; } }
        public byte A { get { return _w; } private set { _w = value; } }

        public static readonly int SizeInBytes = Marshal.SizeOf<Vector4b>();

        public static Vector4b Zero { get { return new Vector4b(0, 0, 0, 0); } }
        public static Vector4b One { get { return new Vector4b(1, 1, 1, 1); } }

        public Vector4b(byte x, byte y, byte z, byte w)
        {
            _x = x;
            _y = y;
            _z = z;
            _w = w;
        }

        public Vector4b(Color color)
        {
            _x = color.R;
            _y = color.G;
            _z = color.B;
            _w = color.A;
        }
    }
}