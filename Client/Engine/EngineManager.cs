using OpenToolkit.Mathematics;
using OpenToolkit.Windowing.Desktop;
using OpenToolkit.Windowing.Common;
using VoxelValley.Client.Game;
using VoxelValley.Common.SceneGraph;
using VoxelValley.Common.Threading;
using VoxelValley.Client.Engine.Graphics;
using VoxelValley.Common;

namespace VoxelValley.Client.Engine
{
    public static class EngineManager
    {
        public static Window Window;

        public static void OpenWindow()
        {
            NativeWindowSettings nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(800, 600),
                Title = CommonConstants.Version,
                APIVersion = new System.Version(3, 2),
                Profile = ContextProfile.Compatability
            };

            GameWindowSettings gameWindowSettings = new GameWindowSettings()
            {
                RenderFrequency = 0,
                UpdateFrequency = 20
            };

            using (Window window = new Window(gameWindowSettings, nativeWindowSettings))
            {
                Window = window;
                window.Run();
            }
        }

        internal static void EngineInitialized()
        {
            GameManager gm = new GameManager("Game Manager");
        }

        public static void OnTick(float deltaTime)
        {
            GameObjectManager.OnTick(deltaTime);
        }

        public static void OnUpdate(float deltaTime)
        {
            GameObjectManager.OnUpdate(deltaTime);
            ThreadManager.OnUpdate(deltaTime);
        }

        public static Mesh[] GetMeshes()
        {
            return GameObjectManager.GetMeshes();
        }
    }
}