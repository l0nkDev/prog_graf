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

        private readonly float speed = 1.5f;

        private bool firstMove = true;

        private Shader shader;

        private Matrix4 view;
        private Matrix4 projection;

        private Vector3 Position;
        private Vector3 front;
        private Vector3 up;

        private float pitch = 0f;
        private float yaw = 0f;
        private readonly float sensitivity = 0.1f;

        private double elapsed_second = 0;
        private int current_second_frames = 0;
        private int fps = 0;

        private List<string> object_names;
        private List<string> pieces_names;

        private string selected_object;
        private string selected_piece;

        private readonly Scene main_scene = new();

        protected override void OnLoad()
        {
            base.OnLoad();

            //ObjectCreation.Serialize();

            MousePosition = new Vector2(Size.X / 2f, Size.Y / 2f);

            GL.ClearColor(0.25882353f, 0.72549f, 0.96f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            CursorState = CursorState.Grabbed;

            Position = new Vector3(0.0f, 0.0f, 3.0f);
            front = new Vector3(0.0f, 0.0f, -1.0f);
            up = Vector3.UnitY;

            main_scene.Objects.Add("monitor", ObjectCreation.LoadObject("monitor", 0.55f, 0.75f, 0.0f));
            main_scene.Objects.Add("pot", ObjectCreation.LoadObject("pot", -0.55f, 0.75f, 0.0f));
            main_scene.Objects.Add("desk", ObjectCreation.LoadObject("desk"));

            object_names = new List<string>(main_scene.Objects.Keys);
            selected_object = object_names.ElementAtOrDefault(0);
            pieces_names = new List<string>(main_scene.Objects[selected_object].Pieces.Keys);
            pieces_names.Insert(0, "self");
            selected_piece = pieces_names.ElementAtOrDefault(0);

            shader = new Shader("../../../shaders/shader.vert", "../../../shaders/shader.frag");

            view = Matrix4.LookAt(Position, Position + front, up);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90f), Size.X / (float)Size.Y, 0.1f, 100.0f);
        }



        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            current_second_frames += 1;
            elapsed_second += args.Time;
            if (elapsed_second > 1)
            {
                elapsed_second = 0;
                fps = current_second_frames;
                current_second_frames = 0;
                UpdateUI();
            }

            view = Matrix4.LookAt(Position, Position + front, up);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            main_scene.draw(shader, Matrix4.Identity, view, projection, args.Time);

            SwapBuffers();

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

                if (KeyboardState.IsKeyDown(Keys.K))
                {
                    if (selected_piece == "self") main_scene.Objects[selected_object].RotateX(1f * (float)args.Time);
                    else main_scene.Objects[selected_object].Pieces[selected_piece].RotateX(1f * (float)args.Time);
                }

                if (KeyboardState.IsKeyDown(Keys.I))
                {
                    if (selected_piece == "self") main_scene.Objects[selected_object].RotateX(-1f * (float)args.Time);
                    else main_scene.Objects[selected_object].Pieces[selected_piece].RotateX(-1f * (float)args.Time);
                }

                if (KeyboardState.IsKeyDown(Keys.L))
                {
                    if (selected_piece == "self") main_scene.Objects[selected_object].RotateY(1f * (float)args.Time);
                    else main_scene.Objects[selected_object].Pieces[selected_piece].RotateY(1f * (float)args.Time);
                }

                if (KeyboardState.IsKeyDown(Keys.J))
                {
                    if (selected_piece == "self") main_scene.Objects[selected_object].RotateY(-1f * (float)args.Time);
                    else main_scene.Objects[selected_object].Pieces[selected_piece].RotateY(-1f * (float)args.Time);
                }

                if (KeyboardState.IsKeyDown(Keys.U))
                {
                    if (selected_piece == "self") main_scene.Objects[selected_object].RotateZ(1f * (float)args.Time);
                    else main_scene.Objects[selected_object].Pieces[selected_piece].RotateZ(1f * (float)args.Time);
                }

                if (KeyboardState.IsKeyDown(Keys.O))
                {
                    if (selected_piece == "self") main_scene.Objects[selected_object].RotateZ(-1f * (float)args.Time);
                    else main_scene.Objects[selected_object].Pieces[selected_piece].RotateZ(-1f * (float)args.Time);
                }

                if (KeyboardState.IsKeyPressed(Keys.Q))
                {
                    if (selected_piece == "self") main_scene.Objects[selected_object].visible = !main_scene.Objects[selected_object].visible;
                    else main_scene.Objects[selected_object].Pieces[selected_piece].visible = !main_scene.Objects[selected_object].Pieces[selected_piece].visible;
                }

                if (KeyboardState.IsKeyPressed(Keys.Tab))
                {
                    try
                    {
                        selected_object = object_names.ElementAt(object_names.IndexOf(selected_object) + 1);
                    }
                    catch
                    {
                        selected_object = object_names.ElementAt(0);
                    }
                    pieces_names = new List<string>(main_scene.Objects[selected_object].Pieces.Keys);
                    pieces_names.Insert(0, "self");
                    selected_piece = pieces_names.ElementAt(0);
                    UpdateUI();
                }

                if (KeyboardState.IsKeyPressed(Keys.CapsLock))
                {
                    try
                    {
                        selected_piece = pieces_names.ElementAt(pieces_names.IndexOf(selected_piece) + 1);
                    }
                    catch
                    {
                        selected_piece = pieces_names.ElementAt(0);
                    }
                    UpdateUI();
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

                front.X = (float)Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)Math.Cos(MathHelper.DegreesToRadians(yaw));
                front.Y = (float)Math.Sin(MathHelper.DegreesToRadians(pitch));
                front.Z = (float)Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)Math.Sin(MathHelper.DegreesToRadians(yaw));
                front = Vector3.Normalize(front);

                MousePosition = new Vector2(Size.X / 2f, Size.Y / 2f);
            }
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

        

        private void UpdateUI()
        {
            Console.Clear();
            Console.WriteLine("FPS: " + fps);
            Console.WriteLine("Selected Object: " + selected_object);
            Console.WriteLine("Selected Piece: " + selected_piece);
        }
    }
}