using System;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Engine.Graphics;
using VoxelValley.Common.ComponentSystem;

namespace VoxelValley.Client.Game.Entities
{
    public class Player : GameObject
    {
        float moveSpeed = 2f;
        float mouseSensitivity = 0.0025f;
        Camera camera;
        
        public Player(string name, GameObject parent, Vector3 spawnPosition) : base(name, parent)
        {
            Transform.Position = spawnPosition;

            camera = new Camera("Main Camera", this.gameObject);
            AddChild(camera);
        }

        public void Move(float x, float y, float z)
        {
            Vector3 offset = new Vector3();

            Vector3 forward = new Vector3((float)Math.Sin((float)Transform.Rotation.X), 0, (float)Math.Cos((float)Transform.Rotation.X));
            Vector3 right = new Vector3(-forward.Z, 0, forward.X);

            offset += x * right;
            offset += y * forward;
            offset.Y += z;

            offset.NormalizeFast();
            offset = Vector3.Multiply(offset, moveSpeed);

            Transform.Position += offset;
        }

        public void AddRotation(float x, float y)
        {
            x = x * mouseSensitivity;
            y = y * mouseSensitivity;

            Transform.Rotation = new Vector3(
                (Transform.Rotation.X + x) % ((float)Math.PI * 2.0f),
                Math.Max(Math.Min(Transform.Rotation.Y + y, (float)Math.PI / 2.0f - 0.1f), (float)-Math.PI / 2.0f + 0.1f),
                Transform.Rotation.Z);
        }

        public Camera GetCamera()
        {
            return camera;
        }
    }
}