using OpenToolkit.Mathematics;

namespace VoxelValley.Client.Engine.SceneGraph.Components
{
    public class LightSpot : Component
    {
        public Vector3 Color;
        public float AmbientIntensity = 0.8f;
        public float DiffuseIntensity = 0.4f;
        public float SpecularIntensity = 0.5f;

        public float Constant = 0f;
        public float Linear = 0f;
        public float Quadratic = 0f;

        public float CutOff = 0f;
        public float OuterCutOff = 0f;
    }
}