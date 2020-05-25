using System;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Engine.Graphics;
using VoxelValley.Client.Engine.Input;
using VoxelValley.Client.Engine.SceneGraph;
using VoxelValley.Client.Engine.SceneGraph.Components;
using VoxelValley.Client.Game.Enviroment;
using VoxelValley.Common.Diagnostics;

namespace VoxelValley.Client.Game.Entities
{
    public class Player : GameObject
    {
        public static Player Instance;
        Type type = typeof(Player);
        float moveSpeed = 0.2f;
        float mouseSensitivity = 0.0025f;

        GameObject cameraGameObject;
        Camera camera;

        Vector3 movementDirection = new Vector3(0, -ClientConstants.World.Gravity, 0);

        public Player(string name, GameObject parent, Vector3 spawnPosition) : base(name, parent)
        {
            Instance = this;

            cameraGameObject = new GameObject(ClientConstants.Graphics.Cameras.PlayerFirstPerson, this, new Vector3(0, 4, 0));
            camera = cameraGameObject.AddComponent<Camera>();
            CameraManager.SetActiveCamera(ClientConstants.Graphics.Cameras.PlayerFirstPerson);

            //new Cube(Color.Red, new Vector3(1, 2, 1), gameObject);

            Transform.Position = spawnPosition;

            InputManager.GetState("Movement", "Move_Forward").Callback += (bool newState) => { SetMovement(newState, new Vector3(0f, 0f, 0.1f)); };
            InputManager.GetState("Movement", "Move_Backwards").Callback += (bool newState) => { SetMovement(newState, new Vector3(0f, 0f, -0.1f)); };

            InputManager.GetState("Movement", "Move_Left").Callback += (bool newState) => { SetMovement(newState, new Vector3(-0.1f, 0f, 0f)); };
            InputManager.GetState("Movement", "Move_Right").Callback += (bool newState) => { SetMovement(newState, new Vector3(0.1f, 0f, 0f)); };

            InputManager.GetState("Movement", "Move_Up").Callback += (bool newState) => { SetMovement(newState, new Vector3(0f, 0.2f, 0f)); };
            InputManager.GetState("Movement", "Move_Down").Callback += (bool newState) => { SetMovement(newState, new Vector3(0f, -0.2f, 0f)); };

            InputManager.GetAction("Debug", "LogCoordinates").Callback += () => { Log.Debug(type, $"Player Coordinates: {Transform.Position.ToString()}, Rotation: {cameraGameObject.Transform.Rotation.ToString()}"); };
            InputManager.GetAction("Debug", "LogCoordinates").Callback += () => { Log.Debug(type, $"Voxel Coordinate: {World.ConvertFromWorldSpaceToVoxelSpace(Transform.Position)}"); };
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

            //Check Y
            if (World.Instance.GetVoxelFromWoldSpace(new Vector3(Transform.Position.X, Transform.Position.Y, Transform.Position.Z)) != 0)
                if (modifiedMoveDirection.Y < 0)
                    modifiedMoveDirection.Y = 0;

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

            Vector3 forward = new Vector3((float)Math.Sin((float)cameraGameObject.Transform.Rotation.X), 0, (float)Math.Cos((float)cameraGameObject.Transform.Rotation.X));
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

            cameraGameObject.Transform.Rotation = new Vector3(
                     (cameraGameObject.Transform.Rotation.X + x) % ((float)Math.PI * 2.0f),
                     Math.Max(Math.Min(cameraGameObject.Transform.Rotation.Y + y, (float)Math.PI / 2.0f - 0.1f), (float)-Math.PI / 2.0f + 0.1f),
                     cameraGameObject.Transform.Rotation.Z);
        }
    }
}