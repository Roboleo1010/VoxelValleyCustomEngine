using System;
using System.Drawing;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Engine;
using VoxelValley.Client.Engine.Graphics.Primitives;
using VoxelValley.Client.Engine.Graphics.Rendering;
using VoxelValley.Client.Engine.Graphics.Shading;
using VoxelValley.Client.Engine.Input;
using VoxelValley.Client.Engine.SceneGraph;
using VoxelValley.Client.Engine.SceneGraph.Components;
using VoxelValley.Client.Game.Enviroment;
using VoxelValley.Common.Diagnostics;

namespace VoxelValley.Client.Game.Entities
{
    public class Player : GameObject
    {
        Type type = typeof(Player);

        float moveSpeed = 0.3f;

        float mouseSensitivity = 0.0025f;

        // Vector3 movementDirection = new Vector3(0, -CommonConstants.World.Gravity, 0);//TODO: Use gravity
        Vector3 movementDirection = new Vector3(0, 0, 0);

        public Player(string name, GameObject parent, Vector3 spawnPosition) : base(name, parent)
        {
            World.Instance.Player = this;
            EngineManager.Window.player = this;

            GameObject cameraContainer = new GameObject("Camera", this, new Vector3(0, 4, 0));
            Camera camera = cameraContainer.AddComponent<Camera>();
            GameManager.ActiveCamera = camera;

            Transform.Position = spawnPosition;

            InputManager.GetState("Movement", "Move_Forward").Callback += (bool newState) => { SetMovement(newState, new Vector3(0f, 0f, 0.1f)); };
            InputManager.GetState("Movement", "Move_Backwards").Callback += (bool newState) => { SetMovement(newState, new Vector3(0f, 0f, -0.1f)); };

            InputManager.GetState("Movement", "Move_Left").Callback += (bool newState) => { SetMovement(newState, new Vector3(-0.1f, 0f, 0f)); };
            InputManager.GetState("Movement", "Move_Right").Callback += (bool newState) => { SetMovement(newState, new Vector3(0.1f, 0f, 0f)); };

            InputManager.GetState("Movement", "Move_Up").Callback += (bool newState) => { SetMovement(newState, new Vector3(0f, 0.2f, 0f)); };
            InputManager.GetState("Movement", "Move_Down").Callback += (bool newState) => { SetMovement(newState, new Vector3(0f, -0.2f, 0f)); };

            InputManager.GetAction("Debug", "LogCoordinates").Callback += () => { Log.Debug(type, $"Coordinates: {Transform.Position.ToString()}, Rotation: {Transform.Rotation.ToString()}"); };
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
            Vector3 modifiedMoveDirection = movementDirection;

            // //Check Y
            // if (World.Instance.GetVoxelFromWoldSpace(new Vector3(Transform.Position.X, Transform.Position.Y, Transform.Position.Z)) != 0)
            //     if (modifiedMoveDirection.Y < 0)
            //         modifiedMoveDirection.Y = 0;

            // //Check X
            // if (modifiedMoveDirection.X > 0)
            // {
            //     if (World.Instance.GetVoxelFromWoldSpace(new Vector3(Transform.Position.X + 1, Transform.Position.Y + 1, Transform.Position.Z)) != 0)
            //         modifiedMoveDirection.X = 0;
            // }
            // else
            // {
            //     if (World.Instance.GetVoxelFromWoldSpace(new Vector3(Transform.Position.X - 1, Transform.Position.Y + 1, Transform.Position.Z)) != 0)
            //         modifiedMoveDirection.X = 0;
            // }

            // //Check Z
            // if (modifiedMoveDirection.Z > 0)
            // {
            //     if (World.Instance.GetVoxelFromWoldSpace(new Vector3(Transform.Position.X, Transform.Position.Y + 1, Transform.Position.Z + 1)) != 0)
            //         modifiedMoveDirection.Z = 0;
            // }
            // else
            // {
            //     if (World.Instance.GetVoxelFromWoldSpace(new Vector3(Transform.Position.X, Transform.Position.Y + 1, Transform.Position.Z - 1)) != 0)
            //         modifiedMoveDirection.Z = 0;
            // }


            Move(modifiedMoveDirection.X, modifiedMoveDirection.Y, modifiedMoveDirection.Z);
        }

        void Move(float x, float y, float z)
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