using System;
using OpenToolkit.Mathematics;
using VoxelValley.Engine.Core.ComponentSystem;

namespace VoxelValley.Engine.Graphics
{
    public class Camera : GameObject
    {
        public float NearClippingPane { get; private set; }
        public float FarClippingPane { get; private set; }

        public Camera(string name, GameObject parent, float nearClippingPane = 0.2f, float farClippingPane = 1000) : base(name, parent)
        {
            NearClippingPane = nearClippingPane;
            FarClippingPane = farClippingPane;
        }

        public Matrix4 GetViewMatrix()
        {
            Vector3 lookAt = new Vector3();

            lookAt.X = (float)(Math.Sin((float)Transform.Rotation.X) * Math.Cos((float)Transform.Rotation.Y));
            lookAt.Y = (float)(Math.Sin((float)Transform.Rotation.Y));
            lookAt.Z = (float)(Math.Cos((float)Transform.Rotation.X) * Math.Cos((float)Transform.Rotation.Y));

            return Matrix4.LookAt(Transform.Position, Transform.Position + lookAt, Vector3.UnitY);
        }
    }
}