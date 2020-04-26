using OpenToolkit.Mathematics;

namespace VoxelValley.Common.SceneGraph.Components
{
    public class Light : Component
    {
        public Vector3 Color;
        public float DiffuseIntensity = 1;
        public float AmbientIntensity = 0.1f;
    }
}