using OpenToolkit.Windowing.Common;
using OpenToolkit.Windowing.Desktop;
using VoxelValley.Client.Game;
using VoxelValley.Common;
using VoxelValley.Common.SceneGraph;
using VoxelValley.Common.Threading;


namespace VoxelValley.Client.Engine
{
    public static class EngineManager
    {
        public static void OpenWindow()
        {
            NativeWindowSettings nativeWindowSettings = new NativeWindowSettings()
            {
                Size = ClientConstants.Graphics.Size,
                Title = CommonConstants.Version,
                APIVersion = new System.Version(3, 2),
                Profile = ContextProfile.Compatability
            };

            GameWindowSettings gameWindowSettings = new GameWindowSettings()
            {
                RenderFrequency = 0,
                UpdateFrequency = ClientConstants.Graphics.RenderFrequency
            };

            using (Window window = new Window(gameWindowSettings, nativeWindowSettings))
            {
                window.Run();
            }
        }

        internal static void EngineInitialized()
        {
            new GameManager("Game Manager");
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