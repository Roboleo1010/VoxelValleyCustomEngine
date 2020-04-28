using System;
using System.ComponentModel;
using System.Drawing;
using OpenToolkit.Graphics.OpenGL4;
using OpenToolkit.Mathematics;
using OpenToolkit.Windowing.Common;
using OpenToolkit.Windowing.Desktop;
using OpenToolkit.Windowing.GraphicsLibraryFramework;
using VoxelValley.Client.Engine.Graphics.Rendering;
using VoxelValley.Client.Engine.Graphics.Shading;
using VoxelValley.Client.Engine.Input;
using VoxelValley.Client.Game;
using VoxelValley.Client.Game.Entities;
using VoxelValley.Client.Engine.SceneGraph;

namespace VoxelValley.Client.Engine
{
    public class Window : GameWindow
    {
        Type type = typeof(Window);

        public Vector2 lastMousePos = new Vector2();
        public Player player;

        //for fps
        public double time = 0;
        public int framesPerSecond = 0;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings) { }

        protected override void OnLoad()
        {
            GL.ClearColor(Color.LightSkyBlue);
            GL.Enable(EnableCap.DepthTest);
            GL.LoadBindings(new GLFWBindingsContext());

            CursorVisible = false;
            VSync = VSyncMode.Off;

            ShaderManager.LoadShaders();
            InputManager.LoadContexts();

            base.OnLoad();

            GameManager.Start();

            InputManager.GetAction("Debug", "DrawSceneGraph").Callback += () => { GameObjectManager.DrawSceneGraph(); };
            InputManager.GetAction("Menu", "Quit").Callback += () => { Close(); };
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            Vector2 delta = lastMousePos - new Vector2(MouseState.X, MouseState.Y); //TODO Move into game
            lastMousePos = new Vector2(MouseState.X, MouseState.Y);
            player.AddRotation(delta.X, delta.Y);

            InputManager.HandleInput();
            EngineManager.OnTick((float)e.Time);

            foreach (RenderBuffer renderBuffer in RenderBufferManager.GetBuffers())
                renderBuffer.UpdateMeshes();

            base.OnUpdateFrame(e);
        }

        //Executed vsync times per second.
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            time += e.Time;
            framesPerSecond++;

            if (time >= 1)
            {
                Title = $"{framesPerSecond}FPS - {1000 / (double)framesPerSecond}ms/f";

                framesPerSecond = 0;
                time = 0;
            }

            EngineManager.OnUpdate((float)e.Time);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            foreach (RenderBuffer buffer in RenderBufferManager.GetBuffers())
                buffer.Render(ClientSize.X / (float)ClientSize.Y);

            GL.Flush();
            SwapBuffers();

            base.OnRenderFrame(e);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);

            base.OnResize(e);
        }

        protected override void OnFocusedChanged(FocusedChangedEventArgs e)
        {
            lastMousePos = new Vector2(MouseState.X, MouseState.Y);

            base.OnFocusedChanged(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            RenderBufferManager.RemoveAllBuffers();
            ShaderManager.RemoveAllShaders();
        }

        #region Input Handling
        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            InputManager.OnKeyDown(e);

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            InputManager.OnKeyUp(e);

            base.OnKeyDown(e);
        }
        #endregion
    }
}