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

        string selected_object;
        string selected_piece;

        Scene main_scene = new();
    


        protected override void OnLoad()
        {
            base.OnLoad();

            SerializeObjects();

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

            Object3D desk = main_scene.Objects["desk"];
            Object3D monitor = main_scene.Objects["monitor"];

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

        private void SerializeObjects()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize
            };

            using (StreamWriter sw = File.CreateText("../../../assets/objects/monitor.json"))
            {
                sw.Write(JsonConvert.SerializeObject(Monitor(), Formatting.Indented, settings));
            }

            using (StreamWriter sw = File.CreateText("../../../assets/objects/pot.json"))
            {
                sw.Write(JsonConvert.SerializeObject(Pot(), Formatting.Indented, settings));
            }

            using (StreamWriter sw = File.CreateText("../../../assets/objects/desk.json"))
            {
                sw.Write(JsonConvert.SerializeObject(Desk(), Formatting.Indented, settings));
            }
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

        private Object3D Monitor(float offset_x = 0.0f, float offset_y = 0.0f, float offset_z = 0.0f)
        {
            Object3D monitor = new Object3D(offset_x, offset_y, offset_z);
            Piece main = new Piece(0.0f, 0.3001f, 0.015f);

            Face screen = new Face(-0.317f, -0.1805f, 0.01f);
            screen.Tris.Add(0, new Tri(0.634f, 0.000f, 0.0f, 0.24f, 1.00f, 0.24f,
                                       0.634f, 0.381f, 0.0f, 1.00f, 0.24f, 0.24f,
                                       0.000f, 0.000f, 0.0f, 0.24f, 0.24f, 1.00f));
            screen.Tris.Add(1, new Tri(0.634f, 0.381f, 0.0f, 1.00f, 0.24f, 0.24f,
                                       0.000f, -0.000f, 0.0f, 0.24f, 0.24f, 1.00f,
                                       0.000f, 0.381f, 0.0f, 0.24f, 0.24f, 0.24f));
            main.Faces.Add("screen", screen);

            Face back = new Face(-0.317f, -0.2005f, -0.01f);
            back.Tris.Add(0, new Tri(0.634f, 0.000f, 0.0f, 0.10f, 0.10f, 0.10f,
                                     0.634f, 0.401f, 0.0f, 0.10f, 0.10f, 0.10f,
                                     0.000f, 0.000f, 0.0f, 0.10f, 0.10f, 0.10f));
            back.Tris.Add(1, new Tri(0.634f, 0.401f, 0.0f, 0.10f, 0.10f, 0.10f,
                                     0.000f, 0.000f, 0.0f, 0.10f, 0.10f, 0.10f,
                                     0.000f, 0.401f, 0.0f, 0.10f, 0.10f, 0.10f));
            main.Faces.Add("back", back);

            Face speakers = new Face(-0.317f, -0.2005f, 0.01f);
            speakers.Tris.Add(0, new Tri(0.634f, 0.02f, 0.0f, 0.24f, 0.24f, 0.24f,
                                         0.634f, 0.00f, 0.0f, 0.24f, 0.24f, 0.24f,
                                         0.000f, 0.02f, 0.0f, 0.24f, 0.24f, 0.24f));
            speakers.Tris.Add(1, new Tri(0.634f, 0.00f, 0.0f, 0.24f, 0.24f, 0.24f,
                                         0.000f, 0.02f, 0.0f, 0.24f, 0.24f, 0.24f,
                                         0.000f, 0.00f, 0.0f, 0.24f, 0.24f, 0.24f));
            main.Faces.Add("speakers", speakers);

            Face left = new Face(-0.317f, -0.2005f, -0.01f);
            left.Tris.Add(0, new Tri(0.0f, 0.401f, 0.02f, 0.17f, 0.17f, 0.17f,
                                     0.0f, 0.000f, 0.02f, 0.17f, 0.17f, 0.17f,
                                     0.0f, 0.401f, 0.00f, 0.17f, 0.17f, 0.17f));
            left.Tris.Add(1, new Tri(0.0f, 0.000f, 0.02f, 0.17f, 0.17f, 0.17f,
                                     0.0f, 0.401f, 0.00f, 0.17f, 0.17f, 0.17f,
                                     0.0f, 0.000f, 0.00f, 0.17f, 0.17f, 0.17f));
            main.Faces.Add("left", left);

            Face right = new Face(0.317f, -0.2005f, -0.01f);
            right.Tris.Add(0, new Tri(0.0f, 0.401f, 0.02f, 0.17f, 0.17f, 0.17f,
                                      0.0f, 0.000f, 0.02f, 0.17f, 0.17f, 0.17f,
                                      0.0f, 0.401f, 0.00f, 0.17f, 0.17f, 0.17f));
            right.Tris.Add(1, new Tri(0.0f, 0.000f, 0.02f, 0.17f, 0.17f, 0.17f,
                                      0.0f, 0.401f, 0.00f, 0.17f, 0.17f, 0.17f,
                                      0.0f, 0.000f, 0.00f, 0.17f, 0.17f, 0.17f));
            main.Faces.Add("right", right);

            Face top = new Face(-0.317f, 0.2005f, -0.01f);
            top.Tris.Add(0, new Tri(0.000f, 0.0f, 0.02f, 0.20f, 0.20f, 0.20f,
                                    0.634f, 0.0f, 0.02f, 0.20f, 0.20f, 0.20f,
                                    0.000f, 0.0f, 0.00f, 0.20f, 0.20f, 0.20f));
            top.Tris.Add(1, new Tri(0.634f, 0.0f, 0.02f, 0.20f, 0.20f, 0.20f,
                                    0.000f, 0.0f, 0.00f, 0.20f, 0.20f, 0.20f,
                                    0.634f, 0.0f, 0.00f, 0.20f, 0.20f, 0.20f));
            main.Faces.Add("top", top);

            Face bottom = new Face(-0.317f, -0.2005f, -0.01f);
            bottom.Tris.Add(0, new Tri(0.000f, 0.0f, 0.02f, 0.20f, 0.20f, 0.20f,
                                       0.634f, 0.0f, 0.02f, 0.20f, 0.20f, 0.20f,
                                       0.000f, 0.0f, 0.00f, 0.20f, 0.20f, 0.20f));
            bottom.Tris.Add(1, new Tri(0.634f, 0.0f, 0.02f, 0.20f, 0.20f, 0.20f,
                                       0.000f, 0.0f, 0.00f, 0.20f, 0.20f, 0.20f,
                                       0.634f, 0.0f, 0.00f, 0.20f, 0.20f, 0.20f));
            main.Faces.Add("bottom", bottom);

            monitor.Pieces.Add("main", main);

            Piece stand = new Piece(0.0f, 0.01f, 0.0f);

            Face front = new Face(-0.025f, 0.0f, 0.005f);
            front.Tris.Add(0, new Tri(0.00f, 0.0f, 0.0f, 0.13f, 0.13f, 0.13f,
                                      0.05f, 0.0f, 0.0f, 0.13f, 0.13f, 0.13f,
                                      0.05f, 0.3f, 0.0f, 0.13f, 0.13f, 0.13f));
            front.Tris.Add(1, new Tri(0.00f, 0.0f, 0.0f, 0.13f, 0.13f, 0.13f,
                                      0.05f, 0.3f, 0.0f, 0.13f, 0.13f, 0.13f,
                                      0.00f, 0.3f, 0.0f, 0.13f, 0.13f, 0.13f));
            stand.Faces.Add("front", front);

            Face sback = new Face(-0.025f, 0.0f, -0.005f);
            sback.Tris.Add(0, new Tri(0.00f, 0.0f, 0.0f, 0.07f, 0.07f, 0.07f,
                                      0.05f, 0.0f, 0.0f, 0.07f, 0.07f, 0.07f,
                                      0.05f, 0.3f, 0.0f, 0.07f, 0.07f, 0.07f));
            sback.Tris.Add(1, new Tri(0.00f, 0.0f, 0.0f, 0.07f, 0.07f, 0.07f,
                                      0.05f, 0.3f, 0.0f, 0.07f, 0.07f, 0.07f,
                                      0.00f, 0.3f, 0.0f, 0.07f, 0.07f, 0.07f));
            stand.Faces.Add("back", sback);

            Face sleft = new Face(-0.025f, 0.0f, -0.005f);
            sleft.Tris.Add(0, new Tri(0.00f, 0.3f, 0.00f, 0.10f, 0.10f, 0.10f,
                                      0.00f, 0.3f, 0.01f, 0.10f, 0.10f, 0.10f,
                                      0.00f, 0.0f, 0.00f, 0.10f, 0.10f, 0.10f));
            sleft.Tris.Add(1, new Tri(0.00f, 0.3f, 0.01f, 0.10f, 0.10f, 0.10f,
                                      0.00f, 0.0f, 0.00f, 0.10f, 0.10f, 0.10f,
                                      0.00f, 0.0f, 0.01f, 0.10f, 0.10f, 0.10f));
            stand.Faces.Add("left", sleft);

            Face sright = new Face(-0.025f, 0.0f, 0.005f);
            sright.Tris.Add(0, new Tri(0.00f, 0.3f, 0.00f, 0.10f, 0.10f, 0.10f,
                                       0.00f, 0.3f, 0.01f, 0.10f, 0.10f, 0.10f,
                                       0.00f, 0.0f, 0.00f, 0.10f, 0.10f, 0.10f));
            sright.Tris.Add(1, new Tri(0.00f, 0.3f, 0.01f, 0.10f, 0.10f, 0.10f,
                                       0.00f, 0.0f, 0.00f, 0.10f, 0.10f, 0.10f,
                                       0.00f, 0.0f, 0.01f, 0.10f, 0.10f, 0.10f));
            stand.Faces.Add("right", sright);

            monitor.Pieces.Add("stand", stand);

            Piece _base = new Piece();

            Face btop = new Face(-0.135f, 0.01f, -0.075f);
            btop.Tris.Add(0, new Tri(0.00f, 0.0f, 0.00f, 0.23f, 0.23f, 0.23f,
                                     0.27f, 0.0f, 0.00f, 0.23f, 0.23f, 0.23f,
                                     0.05f, 0.0f, 0.15f, 0.23f, 0.23f, 0.23f));
            btop.Tris.Add(1, new Tri(0.27f, 0.0f, 0.00f, 0.23f, 0.23f, 0.23f,
                                     0.05f, 0.0f, 0.15f, 0.23f, 0.23f, 0.23f,
                                     0.22f, 0.0f, 0.15f, 0.23f, 0.23f, 0.23f));
            _base.Faces.Add("top", btop);

            Face bbottom = new Face(-0.135f, 0.0f, -0.075f);
            bbottom.Tris.Add(0, new Tri(0.00f, 0.0f, 0.00f, 0.13f, 0.13f, 0.13f,
                                        0.27f, 0.0f, 0.00f, 0.13f, 0.13f, 0.13f,
                                        0.05f, 0.0f, 0.15f, 0.13f, 0.13f, 0.13f));
            bbottom.Tris.Add(1, new Tri(0.27f, 0.0f, 0.00f, 0.13f, 0.13f, 0.13f,
                                        0.05f, 0.0f, 0.15f, 0.13f, 0.13f, 0.13f,
                                        0.22f, 0.0f, 0.15f, 0.13f, 0.13f, 0.13f));
            _base.Faces.Add("bottom", bbottom);

            Face bleft = new Face(-0.135f, 0.0f, -0.075f);
            bleft.Tris.Add(0, new Tri(0.00f, 0.01f, 0.00f, 0.17f, 0.17f, 0.17f,
                                      0.05f, 0.01f, 0.15f, 0.17f, 0.17f, 0.17f,
                                      0.00f, 0.00f, 0.00f, 0.17f, 0.17f, 0.17f));
            bleft.Tris.Add(1, new Tri(0.05f, 0.01f, 0.15f, 0.17f, 0.17f, 0.17f,
                                      0.00f, 0.00f, 0.00f, 0.17f, 0.17f, 0.17f,
                                      0.05f, 0.00f, 0.15f, 0.17f, 0.17f, 0.17f));
            _base.Faces.Add("left", bleft);

            Face bright = new Face(0.085f, 0.0f, -0.075f);
            bright.Tris.Add(0, new Tri(0.00f, 0.01f, 0.15f, 0.17f, 0.17f, 0.17f,
                                       0.05f, 0.01f, 0.00f, 0.17f, 0.17f, 0.17f,
                                       0.00f, 0.00f, 0.15f, 0.17f, 0.17f, 0.17f));
            bright.Tris.Add(1, new Tri(0.05f, 0.01f, 0.00f, 0.17f, 0.17f, 0.17f,
                                       0.00f, 0.00f, 0.15f, 0.17f, 0.17f, 0.17f,
                                       0.05f, 0.00f, 0.00f, 0.17f, 0.17f, 0.17f));
            _base.Faces.Add("right", bright);

            Face bfront = new Face(-0.085f, 0.0f, 0.075f);
            bfront.Tris.Add(0, new Tri(0.00f, 0.01f, 0.0f, 0.20f, 0.20f, 0.20f,
                                       0.17f, 0.01f, 0.0f, 0.20f, 0.20f, 0.20f,
                                       0.00f, 0.00f, 0.0f, 0.20f, 0.20f, 0.20f));
            bfront.Tris.Add(1, new Tri(0.17f, 0.01f, 0.0f, 0.20f, 0.20f, 0.20f,
                                       0.00f, 0.00f, 0.0f, 0.20f, 0.20f, 0.20f,
                                       0.17f, 0.00f, 0.0f, 0.20f, 0.20f, 0.20f));
            _base.Faces.Add("front", bfront);

            Face bback = new Face(-0.135f, 0.0f, -0.075f);
            bback.Tris.Add(0, new Tri(0.00f, 0.01f, 0.0f, 0.10f, 0.10f, 0.10f,
                                      0.27f, 0.01f, 0.0f, 0.10f, 0.10f, 0.10f,
                                      0.00f, 0.00f, 0.0f, 0.10f, 0.10f, 0.10f));
            bback.Tris.Add(1, new Tri(0.27f, 0.01f, 0.0f, 0.10f, 0.10f, 0.10f,
                                      0.00f, 0.00f, 0.0f, 0.10f, 0.10f, 0.10f,
                                      0.27f, 0.00f, 0.0f, 0.10f, 0.10f, 0.10f));
            _base.Faces.Add("back", bback);

            monitor.Pieces.Add("base", _base);

            return monitor;
        }

        private Object3D Pot(float offset_x = 0.0f, float offset_y = 0.0f, float offset_z = 0.0f)
        {
            Object3D pot = new Object3D(offset_x, offset_y, offset_z);
            return pot;
        }

        private Object3D Desk(float offset_x = 0.0f, float offset_y = 0.0f, float offset_z = 0.0f)
        {
            Object3D desk = new Object3D(offset_x, offset_y, offset_z);

            Piece desktop = new Piece(0.0f, 0.72f, 0.0f);

            Face top = new Face(0.0f, 0.03f, 0.0f);
            top.Tris.Add(0, new Tri(-0.75f, 0.00f, -0.375f, 0.4f, 0.2745f, 0.1804f,
                                     0.75f, 0.00f, -0.375f, 0.4f, 0.2745f, 0.1804f,
                                    -0.75f, 0.00f, 0.375f, 0.4f, 0.2745f, 0.1804f));
            top.Tris.Add(1, new Tri( 0.75f, 0.00f, -0.375f, 0.4f, 0.2745f, 0.1804f,
                                    -0.75f, 0.00f, 0.375f, 0.4f, 0.2745f, 0.1804f,
                                     0.75f, 0.00f, 0.375f, 0.4f, 0.2745f, 0.1804f));
            desktop.Faces.Add("top", top);

            Face bottom = new Face();
            bottom.Tris.Add(0, new Tri(-0.75f, -0.03f, -0.375f, 0.28f, 0.1545f, 0.0604f,
                                        0.75f, -0.03f, -0.375f, 0.28f, 0.1545f, 0.0604f,
                                       -0.75f, -0.03f, 0.375f, 0.28f, 0.1545f, 0.0604f));
            bottom.Tris.Add(1, new Tri( 0.75f, -0.03f, -0.375f, 0.28f, 0.1545f, 0.0604f,
                                       -0.75f, -0.03f, 0.375f, 0.28f, 0.1545f, 0.0604f,
                                        0.75f, -0.03f, 0.375f, 0.28f, 0.1545f, 0.0604f));
            desktop.Faces.Add("bottom", bottom);

            Face left = new Face(-0.75f, 0.0f, 0.0f);
            left.Tris.Add(0, new Tri(0.0f, 0.03f, -0.375f, 0.34f, 0.2145f, 0.1204f,
                                     0.0f, 0.03f, 0.375f, 0.34f, 0.2145f, 0.1204f,
                                     0.0f, -0.03f, -0.375f, 0.34f, 0.2145f, 0.1204f));
            left.Tris.Add(1, new Tri(0.0f, 0.03f, 0.375f, 0.34f, 0.2145f, 0.1204f,
                                     0.0f, -0.03f, -0.375f, 0.34f, 0.2145f, 0.1204f,
                                     0.0f, -0.03f, 0.375f, 0.34f, 0.2145f, 0.1204f));
            desktop.Faces.Add("left", left);

            Face right = new Face(0.75f, 0.0f, 0.0f);
            right.Tris.Add(0, new Tri(0.0f, 0.03f, -0.375f, 0.34f, 0.2145f, 0.1204f,
                                      0.0f, 0.03f, 0.375f, 0.34f, 0.2145f, 0.1204f,
                                      0.0f, -0.03f, -0.375f, 0.34f, 0.2145f, 0.1204f));
            right.Tris.Add(1, new Tri(0.0f, 0.03f, 0.375f, 0.34f, 0.2145f, 0.1204f,
                                      0.0f, -0.03f, -0.375f, 0.34f, 0.2145f, 0.1204f,
                                      0.0f, -0.03f, 0.375f, 0.34f, 0.2145f, 0.1204f));
            desktop.Faces.Add("right", right);

            Face front = new Face(0.0f, 0.0f, 0.375f);
            front.Tris.Add(0, new Tri(-0.75f, 0.03f, 0.0f, 0.37f, 0.2445f, 0.1504f,
                                       0.75f, 0.03f, 0.0f, 0.37f, 0.2445f, 0.1504f,
                                      -0.75f, -0.03f, 0.0f, 0.37f, 0.2445f, 0.1504f));
            front.Tris.Add(1, new Tri( 0.75f, 0.03f, 0.0f, 0.37f, 0.2445f, 0.1504f,
                                      -0.75f, -0.03f, 0.0f, 0.37f, 0.2445f, 0.1504f,
                                       0.75f, -0.03f, 0.0f, 0.37f, 0.2445f, 0.1504f));
            desktop.Faces.Add("front", front);

            Face back = new Face(0.0f, 0.0f, -0.375f);
            back.Tris.Add(0, new Tri(-0.75f, 0.03f, 0.0f, 0.31f, 0.1845f, 0.0904f,
                                      0.75f, 0.03f, 0.0f, 0.31f, 0.1845f, 0.0904f,
                                     -0.75f, -0.03f, 0.0f, 0.31f, 0.1845f, 0.0904f));
            back.Tris.Add(1, new Tri( 0.75f, 0.03f, 0.0f, 0.31f, 0.1845f, 0.0904f,
                                     -0.75f, -0.03f, 0.0f, 0.31f, 0.1845f, 0.0904f,
                                      0.75f, -0.03f, 0.0f, 0.31f, 0.1845f, 0.0904f));
            desktop.Faces.Add("back", back);

            desk.Pieces.Add("desktop", desktop);

            Piece leftFoot = new Piece(-0.67f);

            Face lin = new Face(0.03f);
            lin.Tris.Add(0, new Tri(0.0f, 0.69f, -0.315f, 0.25f, 0.1245f, 0.0304f,
                                    0.0f, 0.69f, 0.315f, 0.25f, 0.1245f, 0.0304f,
                                    0.0f, 0.00f, -0.315f, 0.25f, 0.1245f, 0.0304f));
            lin.Tris.Add(1, new Tri(0.0f, 0.69f, 0.315f, 0.25f, 0.1245f, 0.0304f,
                                    0.0f, 0.00f, -0.315f, 0.25f, 0.1245f, 0.0304f,
                                    0.0f, 0.00f, 0.315f, 0.25f, 0.1245f, 0.0304f));
            leftFoot.Faces.Add("in", lin);

            Face lout = new Face(-0.03f);
            lout.Tris.Add(0, new Tri(0.0f, 0.69f, -0.315f, 0.31f, 0.1845f, 0.0904f,
                                     0.0f, 0.69f, 0.315f, 0.31f, 0.1845f, 0.0904f,
                                     0.0f, 0.00f, -0.315f, 0.31f, 0.1845f, 0.0904f));
            lout.Tris.Add(1, new Tri(0.0f, 0.69f, 0.315f, 0.31f, 0.1845f, 0.0904f,
                                     0.0f, 0.00f, -0.315f, 0.31f, 0.1845f, 0.0904f,
                                     0.0f, 0.00f, 0.315f, 0.31f, 0.1845f, 0.0904f));
            leftFoot.Faces.Add("out", lout);

            Face lfront = new Face(0.0f, 0.0f, 0.315f);
            lfront.Tris.Add(0, new Tri(-0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                        0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                       -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f));
            lfront.Tris.Add(1, new Tri(0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                      -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                       0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f));
            leftFoot.Faces.Add("front", lfront);

            Face lback = new Face(0.0f, 0.0f, -0.315f);
            lback.Tris.Add(0, new Tri(-0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                       0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                      -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f));
            lback.Tris.Add(1, new Tri(0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                      -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                       0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f));
            leftFoot.Faces.Add("back", lback);

            Face lbottom = new Face();
            lbottom.Tris.Add(0, new Tri(-0.03f, 0.0f, -0.315f, 0.22f, 0.0945f, 0.0004f,
                                         0.03f, 0.0f, -0.315f, 0.22f, 0.0945f, 0.0004f,
                                        -0.03f, 0.0f, 0.315f, 0.22f, 0.0945f, 0.0004f));
            lbottom.Tris.Add(1, new Tri(0.03f, 0.0f, -0.315f, 0.22f, 0.0945f, 0.0004f,
                                        -0.03f, 0.0f, 0.315f, 0.22f, 0.0945f, 0.0004f,
                                         0.03f, 0.0f, 0.315f, 0.22f, 0.0945f, 0.0004f));
            leftFoot.Faces.Add("bottom", lbottom);

            desk.Pieces.Add("leftFoot", leftFoot);

            Piece rightFoot = new Piece(0.67f);

            Face rin = new Face(-0.03f);
            rin.Tris.Add(0, new Tri(0.0f, 0.69f, -0.315f, 0.25f, 0.1245f, 0.0304f,
                                    0.0f, 0.69f, 0.315f, 0.25f, 0.1245f, 0.0304f,
                                    0.0f, 0.00f, -0.315f, 0.25f, 0.1245f, 0.0304f));
            rin.Tris.Add(1, new Tri(0.0f, 0.69f, 0.315f, 0.25f, 0.1245f, 0.0304f,
                                    0.0f, 0.00f, -0.315f, 0.25f, 0.1245f, 0.0304f,
                                    0.0f, 0.00f, 0.315f, 0.25f, 0.1245f, 0.0304f));
            rightFoot.Faces.Add("in", rin);

            Face rout = new Face(0.03f);
            rout.Tris.Add(0, new Tri(0.0f, 0.69f, -0.315f, 0.31f, 0.1845f, 0.0904f,
                                     0.0f, 0.69f, 0.315f, 0.31f, 0.1845f, 0.0904f,
                                     0.0f, 0.00f, -0.315f, 0.31f, 0.1845f, 0.0904f));
            rout.Tris.Add(1, new Tri(0.0f, 0.69f, 0.315f, 0.31f, 0.1845f, 0.0904f,
                                     0.0f, 0.00f, -0.315f, 0.31f, 0.1845f, 0.0904f,
                                     0.0f, 0.00f, 0.315f, 0.31f, 0.1845f, 0.0904f));
            rightFoot.Faces.Add("out", rout);

            Face rfront = new Face(0.0f, 0.0f, 0.315f);
            rfront.Tris.Add(0, new Tri(-0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                        0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                       -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f));
            rfront.Tris.Add(1, new Tri(0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                      -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                       0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f));
            rightFoot.Faces.Add("front", rfront);

            Face rback = new Face(0.0f, 0.0f, -0.315f);
            rback.Tris.Add(0, new Tri(-0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                       0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                      -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f));
            rback.Tris.Add(1, new Tri(0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                      -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                       0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f));
            rightFoot.Faces.Add("back", rback);

            Face rbottom = new Face();
            rbottom.Tris.Add(0, new Tri(-0.03f, 0.0f, -0.315f, 0.22f, 0.0945f, 0.0004f,
                                         0.03f, 0.0f, -0.315f, 0.22f, 0.0945f, 0.0004f,
                                        -0.03f, 0.0f, 0.315f, 0.22f, 0.0945f, 0.0004f));
            rbottom.Tris.Add(1, new Tri(0.03f, 0.0f, -0.315f, 0.22f, 0.0945f, 0.0004f,
                                        -0.03f, 0.0f, 0.315f, 0.22f, 0.0945f, 0.0004f,
                                         0.03f, 0.0f, 0.315f, 0.22f, 0.0945f, 0.0004f));
            rightFoot.Faces.Add("bottom", rbottom);

            desk.Pieces.Add("rightFoot", rightFoot);

            return desk;
        }
    }
}