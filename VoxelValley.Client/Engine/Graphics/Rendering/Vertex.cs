using System.Runtime.InteropServices;
using OpenToolkit.Mathematics;
using VoxelValley.Common.Mathematics;

namespace VoxelValley.Client.Engine.Graphics.Rendering
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public Vector3 Position;
        public Vector4b Color;
        public Vector3 Normal;

        public static readonly int SizeInBytes = Marshal.SizeOf<Vertex>();

        public Vertex(Vector3 position, Vector4b color, Vector3 normal)
        {
            Position = position;
            Color = color;
            Normal = normal;
        }
    }
}