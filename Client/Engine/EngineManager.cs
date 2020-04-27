using OpenToolkit.Windowing.Common;
using OpenToolkit.Windowing.Desktop;
using VoxelValley.Common;
using VoxelValley.Common.SceneGraph;
using VoxelValley.Common.Threading;

namespace VoxelValley.Client.Engine
{
    public static class EngineManager
    {
        public static Window Window { get; private set; }

        public static void OpenWindow()
        {
            NativeWindowSettings nativeWindowSettings = new NativeWindowSettings()
            {
                Size = ClientConstants.Graphics.Size,
                Title = CommonConstants.Version             
            };

            GameWindowSettings gameWindowSettings = new GameWindowSettings()
            {
                RenderFrequency = 0,
                UpdateFrequency = ClientConstants.Graphics.RenderFrequency
            };

            using (Window window = new Window(gameWindowSettings, nativeWindowSettings))
            {
                Window = window;
                window.Run();
            }
        }

        public static void OnTick(float deltaTime)
        {
            GameObjectManager.OnUpdateOrTick(deltaTime, false);
        }

        public static void OnUpdate(float deltaTime)
        {
            GameObjectManager.OnUpdateOrTick(deltaTime, true);
            ThreadManager.OnUpdate(deltaTime);
        }
    }
}