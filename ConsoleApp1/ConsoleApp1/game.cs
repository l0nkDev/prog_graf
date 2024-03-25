using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using System.Diagnostics;
using StbImageSharp;
using System.IO;

namespace ConsoleApp1
{
    public class Game : GameWindow
    {
        public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title }) { }

        private readonly float[] vertices =
        {
              // positions        // texture coords
             0.5f,  0.5f,  0.5f,  1.0f, 1.0f,   // inferior
            -0.5f, -0.5f,  0.5f,  1.0f, 0.0f,   // izquierdo
            -0.5f,  0.5f, -0.5f,  0.0f, 0.0f,   // derecho
             0.5f, -0.5f, -0.5f,  0.0f, 1.0f    // trasero
        };

        float rotation_x = 0.0f;
        float rotation_y = 0.0f;

        uint[] indices = {
            
            0, 1, 2,   
            0, 2, 3,    
            0, 1, 3,
            1, 2, 3

        };

        int VertexBufferObject;
        int ElementBufferObject;
        int VertexArrayObject;
        Stopwatch timer;
        Texture texture;
        Shader shader;

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            if (KeyboardState.IsKeyDown(Keys.A))
            {
                rotation_x += 0.1f;
            }
            if (KeyboardState.IsKeyDown(Keys.D))
            {
                rotation_x += -0.1f;
            }
            if (KeyboardState.IsKeyDown(Keys.W))
            {
                rotation_y += 0.1f;
            }
            if (KeyboardState.IsKeyDown(Keys.S))
            {
                rotation_y += -0.1f;
            }
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            shader = new Shader("../../../shader.vert", "../../../shader.frag");

            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            int vertexLocation = shader.GetAttribLocation("aPos");
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            int texCoordLocation = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture = Texture.LoadFromFile("../../../img/container.png");
            texture.Use(TextureUnit.Texture0);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.BindVertexArray(VertexArrayObject);

            var transform = Matrix4.Identity;
            transform = transform * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotation_x));
            transform = transform * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotation_y));


            texture.Use(TextureUnit.Texture0);
            shader.Use();

            shader.SetMatrix4("transform", transform);

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