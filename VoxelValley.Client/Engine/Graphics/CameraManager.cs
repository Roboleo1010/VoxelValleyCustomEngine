using System;
using System.Collections.Generic;
using VoxelValley.Client.Engine.Input;
using VoxelValley.Client.Engine.SceneGraph.Components;
using VoxelValley.Common.Diagnostics;

namespace VoxelValley.Client.Engine.Graphics
{
    public static class CameraManager
    {
        static Type type = typeof(CameraManager);
        static Dictionary<string, Camera> cameras = new Dictionary<string, Camera>();
        static string activeCameraName;

        static float _aspectRatio;
        public static float AspectRatio { get { return _aspectRatio; } set { _aspectRatio = value; foreach (Camera camera in cameras.Values) camera.AspectRatio = _aspectRatio; } }

        static CameraManager()
        {
            InputManager.GetAction("Debug", "CycleCamera").Callback += () =>
            {
                if (activeCameraName == ClientConstants.Graphics.Cameras.PlayerFirstPerson)
                    SetActiveCamera(ClientConstants.Graphics.Cameras.Debug);
                else
                    SetActiveCamera(ClientConstants.Graphics.Cameras.PlayerFirstPerson);
            };
        }

        public static void Add(string name, Camera camera)
        {
            if (!cameras.ContainsKey(name))
                cameras.Add(name, camera);
            else
                Log.Error(type, $"Camera with Type of {name} was already added");
        }
        public static void Remove(string name)
        {
            cameras.Remove(name);
        }

        public static void SetActiveCamera(string name)
        {
            if (cameras.ContainsKey(name))
            {
                activeCameraName = name;
                Log.Info(type, $"Active Camera: {name}.");
            }
            else
                Log.Error(type, $"Trying to set active Camera to {name} but it doesn't exsist");
        }

        public static Camera GetActiveCamera()
        {
            if (cameras.TryGetValue(activeCameraName, out Camera camera))
                return camera;

            Log.Error(type, "No active Camera found");
            return null;
        }
    }
}