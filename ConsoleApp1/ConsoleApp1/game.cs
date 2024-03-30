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
             
             0.5f, -0.2f, 0.0f,  0.24f,  1.0f, 0.24f,   // pantalla
             0.5f,  0.6f, 0.0f,   1.0f, 0.24f, 0.24f,
            -0.5f, -0.2f, 0.0f,  0.24f, 0.24f,  1.0f,
            -0.5f,  0.6f, 0.0f,  0.24f, 0.24f, 0.24f,

             0.5f, -0.2f, 0.0f,  0.24f, 0.24f, 0.24f,   // parlantes
             0.5f, -0.3f, 0.0f,  0.24f, 0.24f, 0.24f,
            -0.5f, -0.2f, 0.0f,  0.24f, 0.24f, 0.24f,
            -0.5f, -0.3f, 0.0f,  0.24f, 0.24f, 0.24f,

            -0.025f, -0.3f, -0.025f,  0.13f, 0.13f, 0.13f,  // pata
             0.025f, -0.3f, -0.025f,  0.13f, 0.13f, 0.13f,
            -0.025f, -0.6f, -0.025f,  0.13f, 0.13f, 0.13f,
             0.025f, -0.6f, -0.025f,  0.13f, 0.13f, 0.13f,

             0.5f, -0.3f, -0.1f,  0.10f, 0.10f, 0.10f,  // parte trasera
             0.5f,  0.6f, -0.1f,  0.10f, 0.10f, 0.10f,
            -0.5f, -0.3f, -0.1f,  0.10f, 0.10f, 0.10f,
            -0.5f,  0.6f, -0.1f,  0.10f, 0.10f, 0.10f,

             0.5f,  0.6f,  0.0f, 0.17f, 0.17f, 0.17f,  // lado izquierdo 
             0.5f, -0.3f,  0.0f, 0.17f, 0.17f, 0.17f,
             0.5f,  0.6f, -0.1f, 0.17f, 0.17f, 0.17f,
             0.5f, -0.3f, -0.1f, 0.17f, 0.17f, 0.17f,

            -0.5f,  0.6f,  0.0f, 0.17f, 0.17f, 0.17f,  // lado derecho 
            -0.5f, -0.3f,  0.0f, 0.17f, 0.17f, 0.17f,
            -0.5f,  0.6f, -0.1f, 0.17f, 0.17f, 0.17f,
            -0.5f, -0.3f, -0.1f, 0.17f, 0.17f, 0.17f,

            -0.5f, 0.6f,  0.0f, 0.20f, 0.20f, 0.20f,   // lado superior
             0.5f, 0.6f,  0.0f, 0.20f, 0.20f, 0.20f,
            -0.5f, 0.6f, -0.1f, 0.20f, 0.20f, 0.20f,
             0.5f, 0.6f, -0.1f, 0.20f, 0.20f, 0.20f,

            -0.5f, -0.3f,  0.0f, 0.13f, 0.13f, 0.13f,   // lado inferior
             0.5f, -0.3f,  0.0f, 0.13f, 0.13f, 0.13f,
            -0.5f, -0.3f, -0.1f, 0.13f, 0.13f, 0.13f,
             0.5f, -0.3f, -0.1f, 0.13f, 0.13f, 0.13f,

            -0.025f, -0.3f, -0.075f,  0.07f, 0.07f, 0.07f,  // pata lado trasero
             0.025f, -0.3f, -0.075f,  0.07f, 0.07f, 0.07f,
            -0.025f, -0.6f, -0.075f,  0.07f, 0.07f, 0.07f,
             0.025f, -0.6f, -0.075f,  0.07f, 0.07f, 0.07f,

            -0.025f, -0.3f, -0.075f,  0.10f, 0.10f, 0.10f,  // pata lado izquierdo
            -0.025f, -0.3f, -0.025f,  0.10f, 0.10f, 0.10f,
            -0.025f, -0.6f, -0.075f,  0.10f, 0.10f, 0.10f,
            -0.025f, -0.6f, -0.025f,  0.10f, 0.10f, 0.10f,

            0.025f, -0.3f, -0.075f,  0.10f, 0.10f, 0.10f,  // pata lado derecho
            0.025f, -0.3f, -0.025f,  0.10f, 0.10f, 0.10f,
            0.025f, -0.6f, -0.075f,  0.10f, 0.10f, 0.10f,
            0.025f, -0.6f, -0.025f,  0.10f, 0.10f, 0.10f,

            -0.15f, -0.6f,  0.1f, 0.23f, 0.23f, 0.23f,   // base lado superior
             0.15f, -0.6f,  0.1f, 0.23f, 0.23f, 0.23f,
            -0.25f, -0.6f, -0.2f, 0.23f, 0.23f, 0.23f,
             0.25f, -0.6f, -0.2f, 0.23f, 0.23f, 0.23f,

            -0.15f, -0.625f,  0.1f, 0.13f, 0.13f, 0.13f,   // base lado inferior
             0.15f, -0.625f,  0.1f, 0.13f, 0.13f, 0.13f,
            -0.25f, -0.625f, -0.2f, 0.13f, 0.13f, 0.13f,
             0.25f, -0.625f, -0.2f, 0.13f, 0.13f, 0.13f,

            -0.25f, -0.600f,  -0.2f, 0.10f, 0.10f, 0.10f,   // base lado trasero
             0.25f, -0.600f,  -0.2f, 0.10f, 0.10f, 0.10f,
            -0.25f, -0.625f,  -0.2f, 0.10f, 0.10f, 0.10f,
             0.25f, -0.625f,  -0.2f, 0.10f, 0.10f, 0.10f,

            -0.15f, -0.600f,  0.1f, 0.20f, 0.20f, 0.20f,   // base lado delantero
             0.15f, -0.600f,  0.1f, 0.20f, 0.20f, 0.20f,
            -0.15f, -0.625f,  0.1f, 0.20f, 0.20f, 0.20f,
             0.15f, -0.625f,  0.1f, 0.20f, 0.20f, 0.20f,

            -0.25f, -0.600f,  -0.2f, 0.17f, 0.17f, 0.17f,   // base lado izquierdo
            -0.15f, -0.600f,   0.1f, 0.17f, 0.17f, 0.17f,
            -0.25f, -0.625f,  -0.2f, 0.17f, 0.17f, 0.17f,
            -0.15f, -0.625f,   0.1f, 0.17f, 0.17f, 0.17f,

             0.25f, -0.600f,  -0.2f, 0.17f, 0.17f, 0.17f,   // base lado derecho
             0.15f, -0.600f,   0.1f, 0.17f, 0.17f, 0.17f,
             0.25f, -0.625f,  -0.2f, 0.17f, 0.17f, 0.17f,
             0.15f, -0.625f,   0.1f, 0.17f, 0.17f, 0.17f,


        };

        uint[] indices;

        int VertexBufferObject;
        int ElementBufferObject;
        int VertexArrayObject;
        double time = 0;
        float speed = 1.5f;

        bool firstMove = true;

        Shader shader;

        Matrix4 view;
        Matrix4 projection;

        Vector3 Position;
        Vector3 front;
        Vector3 up;

        float pitch = 0f;
        float yaw = 0f;
        float sensitivity = 0.1f;

        Vector2 lastPos;

        void calculate_indices()
        {
            indices = new uint[vertices.Length / 4];
            uint tmp = 0;
            uint c = 0;
            while(c < indices.Length)
            {
                indices[c]     = tmp;
                indices[c + 1] = tmp + 1;
                indices[c + 2] = tmp + 2;
                indices[c + 3] = tmp + 1;
                indices[c + 4] = tmp + 2;
                indices[c + 5] = tmp + 3;
                tmp += 4;
                c += 6;
            }
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (IsFocused)
            {
                if (KeyboardState.IsKeyDown(Keys.Escape))
                {
                    Close();
                }

                if (KeyboardState.IsKeyDown(Keys.W))
                {
                    Position += front * speed * (float)args.Time;
                }

                if (KeyboardState.IsKeyDown(Keys.S))
                {
                    Position -= front * speed * (float)args.Time;
                }

                if (KeyboardState.IsKeyDown(Keys.D))
                {
                    Position += Vector3.Normalize(Vector3.Cross(front, up)) * speed * (float)args.Time;
                }

                if (KeyboardState.IsKeyDown(Keys.A))
                {
                    Position -= Vector3.Normalize(Vector3.Cross(front, up)) * speed * (float)args.Time;
                }

                if (KeyboardState.IsKeyDown(Keys.Space))
                {
                    Position += up * speed * (float)args.Time;
                }

                if (KeyboardState.IsKeyDown(Keys.LeftShift))
                {
                    Position -= up * speed * (float)args.Time;
                }
            }
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            if (IsFocused)
            {
                if (firstMove)
                {
                    firstMove = false;
                    return;
                }

                Vector2 center = new Vector2(Size.X / 2f, Size.Y / 2f);

                float deltaX = MousePosition.X - center.X;
                float deltaY = MousePosition.Y - center.Y;
                yaw += deltaX * sensitivity;
                pitch -= deltaY * sensitivity;

                if (pitch > 89.0f)
                {
                    pitch = 89.0f;
                }
                else if (pitch < -89.0f)
                {
                    pitch = -89.0f;
                }

                // Console.Write("(");
                // Console.Write(yaw);
                // Console.Write(", ");
                // Console.Write(pitch);
                // Console.WriteLine(")");

                front.X = (float)Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)Math.Cos(MathHelper.DegreesToRadians(yaw));
                front.Y = (float)Math.Sin(MathHelper.DegreesToRadians(pitch));
                front.Z = (float)Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)Math.Sin(MathHelper.DegreesToRadians(yaw));
                front = Vector3.Normalize(front);

                MousePosition = new Vector2(Size.X / 2f, Size.Y / 2f);
            }
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            calculate_indices();
            MousePosition = new Vector2(Size.X / 2f, Size.Y / 2f);

            GL.ClearColor(0.25882353f, 0.72549f, 0.96f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            CursorState = CursorState.Grabbed;

            Position = new Vector3(0.0f, 0.0f,  3.0f);
            front =    new Vector3(0.0f, 0.0f, -1.0f);
            up =           Vector3.UnitY;

            shader = new Shader("../../../shader.vert", "../../../shader.frag");

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

            view = Matrix4.LookAt(Position, Position + front, up);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90f), Size.X / (float)Size.Y, 0.1f, 100.0f);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            time += 10.0 * args.Time;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.BindVertexArray(VertexArrayObject);
            shader.Use();

            view = Matrix4.LookAt(Position, Position + front, up);
            var model = Matrix4.Identity * Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(time));

            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", view);
            shader.SetMatrix4("projection", projection);

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