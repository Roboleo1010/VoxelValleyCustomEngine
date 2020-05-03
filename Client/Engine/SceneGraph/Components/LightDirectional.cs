using OpenToolkit.Mathematics;

namespace VoxelValley.Client.Engine.SceneGraph.Components
{
    public class LightDirectional : Component
    {
        public Vector3 Color;
        public float AmbientIntensity = 0.8f;
        public float DiffuseIntensity = 0.4f;
        public float SpecularIntensity = 0.5f;
    }
}