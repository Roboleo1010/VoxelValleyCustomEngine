using System;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Engine;
using VoxelValley.Client.Engine.Input;
using VoxelValley.Client.Engine.SceneGraph;
using VoxelValley.Client.Engine.SceneGraph.Components;
using VoxelValley.Client.Game.Enviroment;
using VoxelValley.Common;

namespace VoxelValley.Client.Game.Entities
{
    public class Player : GameObject
    {
        float moveSpeed = 0.1f;

        float mouseSensitivity = 0.0025f;

        Vector3 movementDirection = Vector3.Zero;

        public Player(string name, GameObject parent, Vector3 spawnPosition) : base(name, parent)
        {
            ((World)Parent).Player = this;
            EngineManager.Window.player = this;

            Transform.Position = spawnPosition;
            GameManager.ActiveCamera = AddComponent<Camera>();

            InputManager.GetState("Movement", "Move_Forward").Callback += (bool newState) => { SetMovement(newState, new Vector3(0f, 0f, 0.1f)); };
            InputManager.GetState("Movement", "Move_Backwards").Callback += (bool newState) => { SetMovement(newState, new Vector3(0f, 0f, -0.1f)); };

            InputManager.GetState("Movement", "Move_Left").Callback += (bool newState) => { SetMovement(newState, new Vector3(-0.1f, 0f, 0f)); };
            InputManager.GetState("Movement", "Move_Right").Callback += (bool newState) => { SetMovement(newState, new Vector3(0.1f, 0f, 0f)); };

            InputManager.GetState("Movement", "Move_Up").Callback += (bool newState) => { SetMovement(newState, new Vector3(0f, 0.1f, 0f)); };
            InputManager.GetState("Movement", "Move_Down").Callback += (bool newState) => { SetMovement(newState, new Vector3(0f, -0.1f, 0f)); };
        }

        void SetMovement(bool newState, Vector3 movement)
        {
            if (newState)
                movementDirection += movement;
            else
                movementDirection -= movement;
        }

        protected override void OnTick(float deltaTime)
        {
            Move(movementDirection.X, movementDirection.Y, movementDirection.Z); //TODO CommonConstants.World.Gravity
        }

        public void Move(float x, float y, float z)
        {
            Vector3 offset = new Vector3();

            Vector3 forward = new Vector3((float)Math.Sin((float)Transform.Rotation.X), 0, (float)Math.Cos((float)Transform.Rotation.X));
            Vector3 right = new Vector3(-forward.Z, 0, forward.X);

            offset += x * right;
            offset += z * forward;
            offset.Y += y;

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
    }
}