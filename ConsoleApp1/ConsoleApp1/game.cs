using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using System.Diagnostics;

namespace ConsoleApp1
{
    public class Game : GameWindow
    {
        public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title }) { }

        private readonly float[] vertices =
        {
              // positions        // colors
             0.5f,  0.6f, 0.0f,   1.0f, 0.24f, 0.24f,   // pantalla
             0.5f, -0.2f, 0.0f,  0.24f,  1.0f, 0.24f,   
            -0.5f, -0.2f, 0.0f,  0.24f, 0.24f,  1.0f,
            -0.5f,  0.6f, 0.0f,  0.24f, 0.24f, 0.24f,

             0.5f, -0.2f, 0.0f,  0.24f, 0.24f, 0.24f,   // parlantes
             0.5f, -0.3f, 0.0f,  0.24f, 0.24f, 0.24f,
            -0.5f, -0.2f, 0.0f,  0.24f, 0.24f, 0.24f,
            -0.5f, -0.3f, 0.0f,  0.24f, 0.24f, 0.24f,

            -0.05f, -0.3f, 0.0f,  0.10f, 0.10f, 0.10f,  // pata
             0.05f, -0.3f, 0.0f,  0.10f, 0.10f, 0.10f,
            -0.05f, -0.6f, 0.0f,  0.10f, 0.10f, 0.10f,
             0.05f, -0.6f, 0.0f,  0.10f, 0.10f, 0.10f,

             0.5f,  0.6f, -0.1f,  0.10f, 0.10f, 0.10f,  // parte trasera
             0.5f, -0.2f, -0.1f,  0.10f, 0.10f, 0.10f,
            -0.5f, -0.2f, -0.1f,  0.10f, 0.10f, 0.10f,
            -0.5f,  0.6f, -0.1f,  0.10f, 0.10f, 0.10f,

             0.05f,  0.6f,  0.0f, 0.20f, 0.20f, 0.20f,  // lado izquierdo 
             0.05f, -0.3f,  0.0f, 0.20f, 0.20f, 0.20f,
             0.05f,  0.6f, -0.1f, 0.20f, 0.20f, 0.20f,
             0.05f, -0.3f, -0.1f, 0.20f, 0.20f, 0.20f,

            -0.05f,  0.6f,  0.0f, 0.20f, 0.20f, 0.20f,  // lado derecho 
            -0.05f, -0.3f,  0.0f, 0.20f, 0.20f, 0.20f,
            -0.05f,  0.6f, -0.1f, 0.20f, 0.20f, 0.20f,
            -0.05f, -0.3f, -0.1f, 0.20f, 0.20f, 0.20f

            -0.5f, 0.6f,  0.0f, 0.20f, 0.20f, 0.20f,   // lado superior
             0.5f, 0.6f,  0.0f, 0.20f, 0.20f, 0.20f,
            -0.5f, 0.6f, -0.1f, 0.20f, 0.20f, 0.20f,
             0.5f, 0.6f, -0.1f, 0.20f, 0.20f, 0.20f

            -0.5f, -0.3f,  0.0f, 0.20f, 0.20f, 0.20f,   // lado inferior
             0.5f, -0.3f,  0.0f, 0.20f, 0.20f, 0.20f,
            -0.5f, -0.3f, -0.1f, 0.20f, 0.20f, 0.20f,
             0.5f, -0.3f, -0.1f, 0.20f, 0.20f, 0.20f


        };

        uint[] indices = {
            12, 13, 14,
            12, 14, 15,
            
            0, 1, 2,   
            0, 2, 3,    

            4, 5, 6,
            5, 6, 7,

            8, 9, 10,
            9, 10, 11,

            16, 17, 18,
            17, 18, 19,

            20, 21, 22,
            21, 22, 23,

            24, 25, 26,
            25, 26, 27,

            28, 29, 30,
            29, 30, 31

        };

        int VertexBufferObject;
        int ElementBufferObject;
        int VertexArrayObject;
        Stopwatch timer;

        Shader shader;

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);


            shader = new Shader("../../../shaders/shader.vert", "../../../shaders/shader.frag");

            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            shader.Use();
            GL.BindVertexArray(VertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
        }

            

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);
        }

        protected override void OnUnload()
        {
            shader.Dispose();
        }
    }
}