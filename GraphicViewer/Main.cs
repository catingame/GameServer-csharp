using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES30;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GraphicViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var game = new Window(800, 600, "LearnOpenTK"))
            {
                game.Run();
            }
        }
    }

    public class Window : GameWindow
    {
        public Window(Int32 width, Int32 height, String title) : base(
            GameWindowSettings.Default, 
            new NativeWindowSettings { Size = new Vector2i(width, height), Title = title }
        ) { }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            KeyboardState input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            base.OnUpdateFrame(e);
        }

        protected override void OnLoad()
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            //Code goes here

            base.OnLoad();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            //Code goes here.

            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }
    }
}
