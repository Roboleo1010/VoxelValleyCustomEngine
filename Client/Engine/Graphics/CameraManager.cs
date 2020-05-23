using System;
using System.Collections.Generic;
using VoxelValley.Client.Engine.SceneGraph.Components;
using VoxelValley.Common.Diagnostics;

namespace VoxelValley.Client.Engine.Graphics
{
    public static class CameraManager
    {
        static Type type = typeof(CameraManager);
        static Dictionary<string, Camera> cameras = new Dictionary<string, Camera>();
        static string activeCamera;

        static float _aspectRatio;
        public static float AspectRatio { get { return _aspectRatio; } set { _aspectRatio = value; foreach (Camera camera in cameras.Values) camera.AspectRatio = _aspectRatio; } }

        public static Camera GetActiveCamera()
        {
            if (cameras.TryGetValue(activeCamera, out Camera camera))
                return camera;

            Log.Error(type, "No active Camera found.");
            return null;
        }

        public static void AddCamera(string cameraType, Camera camera)
        {
            if (!cameras.ContainsKey(cameraType))
                cameras.Add(cameraType, camera);
            else
                Log.Error(type, $"Camera with Type of {cameraType} was already added.");
        }

        public static void SetActiveCamera(string cameraType)
        {
            activeCamera = cameraType;
            Log.Info(type, $"Switched Camera {cameraType}.");
        }
    }
}