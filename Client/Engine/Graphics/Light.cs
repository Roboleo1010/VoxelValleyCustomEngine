using OpenToolkit.Mathematics;

namespace VoxelValley.Client.Engine.Graphics
{
    public class Light
    {
        public Vector3 Position;
        public Vector3 Color;
        public float DiffuseIntensity;
        public float AmbientIntensity;

        public Light(Vector3 position, Vector3 color, float diffuseIntensity = 1, float ambientIntensity = 0.1f)
        {
            Position = position;
            Color = color;
            DiffuseIntensity = diffuseIntensity;
            AmbientIntensity = ambientIntensity;
        }
    }
}