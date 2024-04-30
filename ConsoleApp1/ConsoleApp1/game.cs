﻿using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using System.ComponentModel;

namespace JuegoProgramacionGrafica
{
    public class Game : GameWindow
    {

        public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title }) { }

        private readonly float speed = 1.5f;

        private bool firstMove = true;
        private bool mouseEnabled = false;

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

        public string queue_path;
        public string queue_name;
        public string queue_scene;
        public bool queue_is_scene;
        public bool is_queued = false;

        public Dictionary<string, Scene> scenes;

        Form1 form;

        protected override void OnLoad()
        {
            base.OnLoad();

            //ObjectCreation.Serialize();

            MousePosition = new Vector2(Size.X / 2f, Size.Y / 2f);

            GL.ClearColor(0.25882353f, 0.72549f, 0.96f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            Position = new Vector3(0.0f, 0.0f, 3.0f);
            front = new Vector3(0.0f, 0.0f, -1.0f);
            up = Vector3.UnitY;

            scenes = new();
            //scenes.Add("main_scene", new Scene());
            //scenes["main_scene"].Objects.Add("monitor", ObjectCreation.LoadObject("../../../assets/objects/monitor.json", 0.55f, 0.75f, 0.0f));
            //scenes["main_scene"].Objects.Add("pot", ObjectCreation.LoadObject("../../../assets/objects/pot.json", -0.55f, 0.75f, 0.0f));
            //scenes["main_scene"].Objects.Add("desk", ObjectCreation.LoadObject("../../../assets/objects/desk.json"));

            shader = new Shader("../../../shaders/shader.vert", "../../../shaders/shader.frag");

            view = Matrix4.LookAt(Position, Position + front, up);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90f), Size.X / (float)Size.Y, 0.1f, 100.0f);

            //ObjectCreation.Serialize(scenes["main_scene"]);

            form = new(this);
            Thread thread = new Thread(() =>
            {
                UIThread();
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        [STAThread]
        private void UIThread()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.Run(form);
            Close();
        }


        protected override void OnRenderFrame(FrameEventArgs args)
        {
            if (is_queued)
            {
                if (queue_is_scene) scenes.Add(queue_name, ObjectCreation.Deserialize<Scene>(queue_path)); 
                else scenes[queue_scene].Objects.Add(queue_name, ObjectCreation.Deserialize<Object3D>(queue_path));
                form.Invoke(form.myDelegate);
                is_queued = false;
            }

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

            foreach (Scene scene in scenes.Values)
            {
                scene.draw(shader, Matrix4.Identity, view, projection, args.Time);
            }

            SwapBuffers();

        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (IsFocused)
            {
                if (MouseState[MouseButton.Right])
                {
                    MousePosition = new Vector2(Size.X / 2f, Size.Y / 2f);
                    CursorState = CursorState.Grabbed;
                }
                else
                {
                    CursorState = CursorState.Normal;
                    return;
                }

                if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Escape))
                {
                    //Close();
                }

                if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.W)) Position += front * speed * (float)args.Time;

                if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.A)) Position -= Vector3.Normalize(Vector3.Cross(front, up)) * speed * (float)args.Time;

                if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.S)) Position -= front * speed * (float)args.Time;

                if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.D)) Position += Vector3.Normalize(Vector3.Cross(front, up)) * speed * (float)args.Time;

                if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Space)) Position += up * speed * (float)args.Time;
           
                if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.LeftShift)) Position -= up * speed * (float)args.Time;

                if (KeyboardState.IsKeyPressed(OpenTK.Windowing.GraphicsLibraryFramework.Keys.P)) scenes["main_scene"].Objects.Add("new_object", ObjectCreation.Deserialize<Object3D>("../../../assets/objects/monitor.json"));
            }
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            if (IsFocused && CursorState == CursorState.Grabbed)
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

                MousePosition = center;
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
        }
    }
}