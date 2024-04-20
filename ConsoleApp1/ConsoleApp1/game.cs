using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using Newtonsoft.Json;
using System;
using System.IO;

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

        double elapsed_second = 0;
        int current_second_frames = 0;
        int fps = 0;

        List<string> object_names;

        string selected_object = "monitor";
        string selected_piece = "main";

        Scene main_scene = new();



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

            main_scene.Objects.Add("monitor", LoadObject("monitor", 0.0f, 0.75f, 0.0f));
            main_scene.Objects.Add("pot", LoadObject("pot", -0.55f, 0.75f, 0.0f));
            main_scene.Objects.Add("desk", LoadObject("desk"));

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
                    main_scene.Objects[selected_object].RotateX(1f * (float)args.Time);
                }

                if (KeyboardState.IsKeyDown(Keys.I))
                {
                    main_scene.Objects[selected_object].RotateX(-1f * (float)args.Time);
                }

                if (KeyboardState.IsKeyDown(Keys.L))
                {
                    main_scene.Objects[selected_object].RotateY(1f * (float)args.Time);
                }

                if (KeyboardState.IsKeyDown(Keys.J))
                {
                    main_scene.Objects[selected_object].RotateY(-1f * (float)args.Time);
                }

                if (KeyboardState.IsKeyDown(Keys.U))
                {
                    main_scene.Objects[selected_object].RotateZ(1f * (float)args.Time);
                }

                if (KeyboardState.IsKeyDown(Keys.O))
                {
                    main_scene.Objects[selected_object].RotateZ(-1f * (float)args.Time);
                }

                if (KeyboardState.IsKeyPressed(Keys.Q))
                {
                    main_scene.Objects[selected_object].visible = !main_scene.Objects[selected_object].visible;
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

        private Object3D LoadObject(string name, float offset_x = 0.0f, float offset_y = 0.0f, float offset_z = 0.0f)
        {
            using (StreamReader sr = File.OpenText("../../../assets/objects/" + name + ".json"))
            {
                Object3D objectOut = JsonConvert.DeserializeObject<Object3D>(sr.ReadToEnd());
                objectOut.offset_x = offset_x;
                objectOut.offset_y = offset_y;
                objectOut.offset_z = offset_z;
                return objectOut;
            }
        }
    }
}