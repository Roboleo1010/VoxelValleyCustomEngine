using OpenToolkit.Mathematics;
using VoxelValley.Engine.Core.ComponentSystem;
using VoxelValley.Engine.Core.Threading;
using VoxelValley.Engine.Graphics;
using VoxelValley.Game;
using Window = VoxelValley.Engine.Graphics.Window;
using OpenToolkit.Windowing.Desktop;
using OpenToolkit.Windowing.Common;

namespace VoxelValley.Engine.Core
{
    public static class EngineManager
    {
        public static Window Window;

        public static void OpenWindow(string title)
        {
            NativeWindowSettings nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(800, 600),
                Title = "LearnOpenTK - Creating a Window",
                APIVersion = new System.Version(3, 2),
                Profile = ContextProfile.Compatability
            };

            GameWindowSettings gameWindowSettings = new GameWindowSettings()
            {
                RenderFrequency = 0,
                UpdateFrequency = 20
            };

            using (var window = new Window(gameWindowSettings, nativeWindowSettings))
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