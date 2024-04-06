using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
namespace ConsoleApp1
{
    public class Game : GameWindow
    {
        public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title }) { }

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

        bool last_p = false;

        object_array pantallas = new object_array();

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

                if (KeyboardState.IsKeyDown(Keys.P) && !last_p)
                {
                    last_p = true;
                    pantallas.Add(new pantalla(Position.X, Position.Y, Position.Z));
                } else if (!KeyboardState.IsKeyDown(Keys.P))
                {
                    last_p = false;
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
            MousePosition = new Vector2(Size.X / 2f, Size.Y / 2f);

            Console.WriteLine("Current position: {0} \n", Position);
  

            pantallas.Add(new pantalla( 2f, 0f,  0f, -9f));
            pantallas.Add(new pantalla(-2f, 0f,  0f,  90f));
            pantallas.Add(new pantalla( 0f, 0f, -2f));
            pantallas.Add(new pantalla( 0f, 0f,  2f, 180f));

            GL.ClearColor(0.25882353f, 0.72549f, 0.96f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            CursorState = CursorState.Grabbed;

            Position = new Vector3(0.0f, 0.0f,  3.0f);
            front =    new Vector3(0.0f, 0.0f, -1.0f);
            up =           Vector3.UnitY;

            shader = new Shader("../../../shader.vert", "../../../shader.frag");

            

            view = Matrix4.LookAt(Position, Position + front, up);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90f), Size.X / (float)Size.Y, 0.1f, 100.0f);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            var cursorpos = Console.GetCursorPosition();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Current position: {0} \n", Position);
            Console.SetCursorPosition(cursorpos.Left, cursorpos.Top);


            view = Matrix4.LookAt(Position, Position + front, up);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            pantallas.draw(shader, view, projection, args.Time);


            SwapBuffers();
        }

            

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90f), Size.X / (float)Size.Y, 0.1f, 100.0f);
            GL.Viewport(0, 0, Size.X, Size.Y);
        }

        protected override void OnUnload()
        {
            shader.Dispose();
        }
    }
}