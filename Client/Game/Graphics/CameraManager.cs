using System;
using System.Collections.Generic;
using VoxelValley.Client.Engine.SceneGraph.Components;
using VoxelValley.Common.Diagnostics;

namespace VoxelValley.Client.Game.Graphics
{
    public static class CameraManager
    {
        static Type type = typeof(CameraManager);
        static Dictionary<CamreaType, Camera> cameras = new Dictionary<CamreaType, Camera>();
        static CamreaType activeCamera;

        static float _aspectRatio;
        public static float AspectRatio { get { return _aspectRatio; } set { _aspectRatio = value; foreach (Camera camera in cameras.Values) camera.AspectRatio = _aspectRatio; } }

        public enum CamreaType
        {
            PLAYER_FIRST,
            PLAYER_THIRD,
            DEBUG
        }

        public static Camera GetActiveCamera()
        {
            if (cameras.TryGetValue(activeCamera, out Camera camera))
                return camera;

            Log.Error(type, "No active Camera found.");
            return null;
        }

        public static void AddCamera(CamreaType cameraType, Camera camera)
        {
            if (!cameras.ContainsKey(cameraType))
                cameras.Add(cameraType, camera);
            else
                Log.Error(type, $"Camera with Type of {cameraType.ToString()} was already added.");
        }

        public static void SetActiveCamera(CamreaType cameraType)
        {
            activeCamera = cameraType;
            Log.Info(type, $"Switched Camera {cameraType.ToString()}.");
        }
    }
}