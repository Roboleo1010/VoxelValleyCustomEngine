using System;
using OpenToolkit.Mathematics;

namespace VoxelValley.Client.Engine.SceneGraph.Components
{
    public class Camera : Component
    {
        public float NearClippingPane = 0.2f;
        public float FarClippingPane = 10000f;
        public float FOV = MathHelper.PiOver2;
        public float AspectRatio;


        public Matrix4 GetViewMatrix()
        {
            Vector3 lookAt = new Vector3();

            lookAt.X = (float)(Math.Sin((float)ParentGameObject.Transform.Rotation.X) * Math.Cos((float)ParentGameObject.Transform.Rotation.Y));
            lookAt.Y = (float)(Math.Sin((float)ParentGameObject.Transform.Rotation.Y));
            lookAt.Z = (float)(Math.Cos((float)ParentGameObject.Transform.Rotation.X) * Math.Cos((float)ParentGameObject.Transform.Rotation.Y));

            return Matrix4.LookAt(ParentGameObject.Transform.Position, ParentGameObject.Transform.Position + lookAt, Vector3.UnitY);
        }

        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(FOV, AspectRatio, NearClippingPane, FarClippingPane);
        }
    }
}