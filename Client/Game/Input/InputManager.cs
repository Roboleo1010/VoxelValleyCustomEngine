using OpenToolkit.Mathematics;
using VoxelValley.Engine.Core.ComponentSystem;
using VoxelValley.Engine.Core;
using VoxelValley.Game.Entities;

namespace VoxelValley.Game.Input
{
    public class InputManager
    {
        Player player;
        bool wasGPressedLastFrame = false;

        public Vector2 lastMousePos = new Vector2();

        public InputManager(Player player)
        {
            this.player = player;
        }

        public void ProcessInput()
        {
            // KeyboardState keyboard = Keyboard.GetState();

            // if (keyboard.IsKeyDown(Key.W))
            //     player.Move(0f, 0.1f, 0f);

            // if (keyboard.IsKeyDown(Key.S))
            //     player.Move(0f, -0.1f, 0f);

            // if (keyboard.IsKeyDown(Key.A))
            //     player.Move(-0.1f, 0f, 0f);

            // if (keyboard.IsKeyDown(Key.D))
            //     player.Move(0.1f, 0f, 0f);

            // if (keyboard.IsKeyDown(Key.Q))
            //     player.Move(0f, 0f, -0.1f);

            // if (keyboard.IsKeyDown(Key.E))
            //     player.Move(0f, 0f, 0.1f);

            // if (keyboard.IsKeyDown(Key.Escape))
            //     EngineManager.Window.Exit();

            // if (keyboard.IsKeyDown(Key.G) && !wasGPressedLastFrame)
            // {
            //     wasGPressedLastFrame = true;
            //     GameObjectManager.DrawSceneGraph();
            // }

            // if (wasGPressedLastFrame && keyboard.IsKeyUp(Key.G))
            //     wasGPressedLastFrame = false;

            // if (EngineManager.Window.Focused)
            // {
            //     Vector2 delta = lastMousePos - new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            //     player.AddRotation(delta.X, delta.Y);
            //     lastMousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            // }
        }
    }
}