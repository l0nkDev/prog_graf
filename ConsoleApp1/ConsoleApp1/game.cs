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

        scene main_scene = new();
    


        protected override void OnLoad()
        {
            base.OnLoad();
            MousePosition = new Vector2(Size.X / 2f, Size.Y / 2f);

            GL.ClearColor(0.25882353f, 0.72549f, 0.96f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            CursorState = CursorState.Grabbed;

            Position = new Vector3(0.0f, 0.0f, 3.0f);
            front = new Vector3(0.0f, 0.0f, -1.0f);
            up = Vector3.UnitY;

            main_scene.Add("monitor", LoadObject("monitor", 0.0f, 0.75f, 0.0f));
            main_scene.Add("pot", Pot(-0.55f, 0.75f, 0.0f));
            main_scene.Add("desk", Desk());
            object_names = new List<string>(main_scene.Keys);

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

                if(KeyboardState.IsKeyPressed(Keys.Tab))
                {
                    SerializeObjects();
                }


                if (KeyboardState.IsKeyDown(Keys.K))
                {
                    main_scene[selected_object].rotate_X(1f * (float)args.Time);
                }

                if (KeyboardState.IsKeyDown(Keys.I))
                {
                    main_scene[selected_object].rotate_X(-1f * (float)args.Time);
                }

                if (KeyboardState.IsKeyDown(Keys.L))
                {
                    main_scene[selected_object].rotate_Y(1f * (float)args.Time);
                }

                if (KeyboardState.IsKeyDown(Keys.J))
                {
                    main_scene[selected_object].rotate_Y(-1f * (float)args.Time);
                }

                if (KeyboardState.IsKeyDown(Keys.U))
                {
                    main_scene[selected_object].rotate_Z(1f * (float)args.Time);
                }

                if (KeyboardState.IsKeyDown(Keys.O))
                {
                    main_scene[selected_object].rotate_Z(-1f * (float)args.Time);
                }

                if (KeyboardState.IsKeyPressed(Keys.Q))
                {
                    main_scene[selected_object].visible = !main_scene[selected_object].visible;
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
            using (StreamWriter sw = File.CreateText("../../../assets/objects/tri.json"))
            {
                sw.Write(JsonConvert.SerializeObject(Test(), Formatting.Indented));
            }
            using (StreamWriter sw = File.CreateText("../../../assets/objects/monitor.json"))
            {
                sw.Write(JsonConvert.SerializeObject(Monitor(0.0f, 0.75f, 0.0f), Formatting.Indented));
            }
            using (StreamWriter sw = File.CreateText("../../../assets/objects/pot.json"))
            {
                sw.Write(JsonConvert.SerializeObject(Pot(), Formatting.Indented));
            }
            using (StreamWriter sw = File.CreateText("../../../assets/objects/desk.json"))
            {
                sw.Write(JsonConvert.SerializeObject(Desk(), Formatting.Indented));
            }
        }

        private tri Test()
        {
            return
                new tri(0.634f, 0.000f, 0.0f, 0.24f, 1.00f, 0.24f,
                        0.634f, 0.381f, 0.0f, 1.00f, 0.24f, 0.24f,
                        0.000f, 0.000f, 0.0f, 0.24f, 0.24f, 1.00f);
        }

        private scene_object LoadObject(string name, float offset_x = 0.0f, float offset_y = 0.0f, float offset_z = 0.0f)
        {
            using (StreamReader sr = File.OpenText("../../../assets/objects/" + name + ".json"))
            {
                return JsonConvert.DeserializeObject<scene_object>(sr.ReadToEnd());
            }
        }

        private tri JsonTri()
        {
            using (StreamReader sr = File.OpenText("../../../assets/objects/tri.json"))
            {
                return JsonConvert.DeserializeObject<tri>(sr.ReadToEnd());
            }
        }

        private scene_object Monitor(float offset_x = 0.0f, float offset_y = 0.0f, float offset_z = 0.0f)
        {
            return new scene_object(offset_x, offset_y, offset_z)
                    {
                        {
                            "main monitor",
                            new piece(0.0f, 0.3001f, 0.015f)
                            {
                                {
                                    "screen",
                                    new face(-0.317f, -0.1805f, 0.01f)
                                    {
                                        new tri(0.634f, 0.000f, 0.0f, 0.24f, 1.00f, 0.24f,
                                                0.634f, 0.381f, 0.0f, 1.00f, 0.24f, 0.24f,
                                                0.000f, 0.000f, 0.0f, 0.24f, 0.24f, 1.00f),

                                        new tri(0.634f,  0.381f, 0.0f, 1.00f, 0.24f, 0.24f,
                                                0.000f, -0.000f, 0.0f, 0.24f, 0.24f, 1.00f,
                                                0.000f,  0.381f, 0.0f, 0.24f, 0.24f, 0.24f)
                                    }
                                },

                                {
                                    "back pane",
                                    new face(-0.317f, -0.2005f, -0.01f)
                                    {
                                        new tri(0.634f, 0.000f, 0.0f, 0.10f, 0.10f,  0.10f,
                                                0.634f, 0.401f, 0.0f, 0.10f, 0.10f,  0.10f,
                                                0.000f, 0.000f, 0.0f, 0.10f, 0.10f,  0.10f),

                                        new tri(0.634f, 0.401f, 0.0f, 0.10f, 0.10f,  0.10f,
                                                0.000f, 0.000f, 0.0f, 0.10f, 0.10f,  0.10f,
                                                0.000f, 0.401f, 0.0f, 0.10f, 0.10f,  0.10f)
                                    }
                                },

                                {
                                    "speakers",
                                    new face(-0.317f, -0.2005f, 0.01f)
                                    {
                                        new tri(0.634f, 0.02f, 0.0f, 0.24f,  0.24f, 0.24f,
                                                0.634f, 0.00f, 0.0f, 0.24f,  0.24f, 0.24f,
                                                0.000f, 0.02f, 0.0f, 0.24f,  0.24f, 0.24f),

                                        new tri(0.634f, 0.00f, 0.0f, 0.24f,  0.24f, 0.24f,
                                                0.000f, 0.02f, 0.0f, 0.24f,  0.24f, 0.24f,
                                                0.000f, 0.00f, 0.0f, 0.24f,  0.24f, 0.24f)
                                    }
                                },

                                {
                                    "left pane",
                                    new face(-0.317f, -0.2005f, -0.01f)
                                    {
                                        new tri(0.0f, 0.401f, 0.02f, 0.17f, 0.17f, 0.17f,
                                                0.0f, 0.000f, 0.02f, 0.17f, 0.17f, 0.17f,
                                                0.0f, 0.401f, 0.00f, 0.17f, 0.17f, 0.17f),

                                        new tri(0.0f, 0.000f, 0.02f, 0.17f, 0.17f, 0.17f,
                                                0.0f, 0.401f, 0.00f, 0.17f, 0.17f, 0.17f,
                                                0.0f, 0.000f, 0.00f, 0.17f, 0.17f, 0.17f)
                                    }
                                },

                                {
                                    "right pane",
                                    new face(0.317f, -0.2005f, -0.01f)
                                    {
                                        new tri(0.0f, 0.401f, 0.02f, 0.17f, 0.17f, 0.17f,
                                                0.0f, 0.000f, 0.02f, 0.17f, 0.17f, 0.17f,
                                                0.0f, 0.401f, 0.00f, 0.17f, 0.17f, 0.17f),

                                        new tri(0.0f, 0.000f, 0.02f, 0.17f, 0.17f, 0.17f,
                                                0.0f, 0.401f, 0.00f, 0.17f, 0.17f, 0.17f,
                                                0.0f, 0.000f, 0.00f, 0.17f, 0.17f, 0.17f)
                                    }
                                },

                                {
                                    "top pane",
                                    new face(-0.317f, 0.2005f, -0.01f)
                                    {
                                        new tri(0.000f, 0.0f, 0.02f, 0.20f, 0.20f, 0.20f,
                                                0.634f, 0.0f, 0.02f, 0.20f, 0.20f, 0.20f,
                                                0.000f, 0.0f, 0.00f, 0.20f, 0.20f, 0.20f),

                                        new tri(0.634f, 0.0f, 0.02f, 0.20f, 0.20f, 0.20f,
                                                0.000f, 0.0f, 0.00f, 0.20f, 0.20f, 0.20f,
                                                0.634f, 0.0f, 0.00f, 0.20f, 0.20f, 0.20f)
                                    }
                                },

                                {
                                    "bottom pane",
                                    new face(-0.317f, -0.2005f, -0.01f)
                                    {
                                        new tri(0.000f, 0.0f, 0.02f, 0.20f, 0.20f, 0.20f,
                                                0.634f, 0.0f, 0.02f, 0.20f, 0.20f, 0.20f,
                                                0.000f, 0.0f, 0.00f, 0.20f, 0.20f, 0.20f),

                                        new tri(0.634f, 0.0f, 0.02f, 0.20f, 0.20f, 0.20f,
                                                0.000f, 0.0f, 0.00f, 0.20f, 0.20f, 0.20f,
                                                0.634f, 0.0f, 0.00f, 0.20f, 0.20f, 0.20f)
                                    }
                                }
                            }
                        },

                        {
                            "stand",
                            new piece(0.0f, 0.01f, 0.0f)
                            {
                                {
                                    "front",
                                    new face(-0.025f, 0.0f, 0.005f)
                                    {
                                        new tri(0.00f, 0.0f, 0.0f, 0.13f, 0.13f, 0.13f,
                                                0.05f, 0.0f, 0.0f, 0.13f, 0.13f, 0.13f,
                                                0.05f, 0.3f, 0.0f, 0.13f, 0.13f, 0.13f),

                                        new tri(0.00f, 0.0f, 0.0f, 0.13f, 0.13f, 0.13f,
                                                0.05f, 0.3f, 0.0f, 0.13f, 0.13f, 0.13f,
                                                0.00f, 0.3f, 0.0f, 0.13f, 0.13f, 0.13f)
                                    }
                                },

                                {
                                    "back",
                                    new face(-0.025f, 0.0f, -0.005f)
                                    {
                                        new tri(0.00f, 0.0f, 0.0f, 0.07f, 0.07f, 0.07f,
                                                0.05f, 0.0f, 0.0f, 0.07f, 0.07f, 0.07f,
                                                0.05f, 0.3f, 0.0f, 0.07f, 0.07f, 0.07f),

                                        new tri(0.00f, 0.0f, 0.0f, 0.07f, 0.07f, 0.07f,
                                                0.05f, 0.3f, 0.0f, 0.07f, 0.07f, 0.07f,
                                                0.00f, 0.3f, 0.0f, 0.07f, 0.07f, 0.07f)
                                    }
                                },

                                {
                                    "left",
                                    new face(-0.025f, 0.0f, -0.005f)
                                    {
                                        new tri(0.00f, 0.3f, 0.00f, 0.10f, 0.10f, 0.10f,
                                                0.00f, 0.3f, 0.01f, 0.10f, 0.10f, 0.10f,
                                                0.00f, 0.0f, 0.00f, 0.10f, 0.10f, 0.10f),

                                        new tri(0.00f, 0.3f, 0.01f, 0.10f, 0.10f, 0.10f,
                                                0.00f, 0.0f, 0.00f, 0.10f, 0.10f, 0.10f,
                                                0.00f, 0.0f, 0.01f, 0.10f, 0.10f, 0.10f)
                                    }
                                },

                                {
                                    "right",
                                    new face(0.025f, 0.0f, -0.005f)
                                    {
                                        new tri(0.00f, 0.3f, 0.00f, 0.10f, 0.10f, 0.10f,
                                                0.00f, 0.3f, 0.01f, 0.10f, 0.10f, 0.10f,
                                                0.00f, 0.0f, 0.00f, 0.10f, 0.10f, 0.10f),

                                        new tri(0.00f, 0.3f, 0.01f, 0.10f, 0.10f, 0.10f,
                                                0.00f, 0.0f, 0.00f, 0.10f, 0.10f, 0.10f,
                                                0.00f, 0.0f, 0.01f, 0.10f, 0.10f, 0.10f)
                                    }
                                }
                            }
                        },

                        {
                            "base",
                            new piece
                            {
                                {
                                    "top",
                                    new face(-0.135f, 0.01f, -0.075f)
                                    {
                                        new tri(0.00f, 0.0f, 0.00f, 0.23f, 0.23f, 0.23f,
                                                0.27f, 0.0f, 0.00f, 0.23f, 0.23f, 0.23f,
                                                0.05f, 0.0f, 0.15f, 0.23f, 0.23f, 0.23f),

                                        new tri(0.27f, 0.0f, 0.00f, 0.23f, 0.23f, 0.23f,
                                                0.05f, 0.0f, 0.15f, 0.23f, 0.23f, 0.23f,
                                                0.22f, 0.0f, 0.15f, 0.23f, 0.23f, 0.23f)
                                    }
                                },

                                {
                                    "bottom",
                                    new face(-0.135f, 0.0f, -0.075f)
                                    {
                                        new tri(0.00f, 0.0f, 0.00f, 0.13f, 0.13f, 0.13f,
                                                0.27f, 0.0f, 0.00f, 0.13f, 0.13f, 0.13f,
                                                0.05f, 0.0f, 0.15f, 0.13f, 0.13f, 0.13f),

                                        new tri(0.27f, 0.0f, 0.00f, 0.13f, 0.13f, 0.13f,
                                                0.05f, 0.0f, 0.15f, 0.13f, 0.13f, 0.13f,
                                                0.22f, 0.0f, 0.15f, 0.13f, 0.13f, 0.13f)
                                    }
                                },

                                {
                                    "left",
                                    new face(-0.135f, 0.0f, -0.075f)
                                    {
                                        new tri(0.00f, 0.01f, 0.00f, 0.17f, 0.17f, 0.17f,
                                                0.05f, 0.01f, 0.15f, 0.17f, 0.17f, 0.17f,
                                                0.00f, 0.00f, 0.00f, 0.17f, 0.17f, 0.17f),

                                        new tri(0.05f, 0.01f, 0.15f, 0.17f, 0.17f, 0.17f,
                                                0.00f, 0.00f, 0.00f, 0.17f, 0.17f, 0.17f,
                                                0.05f, 0.00f, 0.15f, 0.17f, 0.17f, 0.17f)
                                    }
                                },

                                {
                                    "right",
                                    new face(0.085f, 0.0f, -0.075f)
                                    {
                                        new tri(0.00f, 0.01f, 0.15f, 0.17f, 0.17f, 0.17f,
                                                0.05f, 0.01f, 0.00f, 0.17f, 0.17f, 0.17f,
                                                0.00f, 0.00f, 0.15f, 0.17f, 0.17f, 0.17f),

                                        new tri(0.05f, 0.01f, 0.00f, 0.17f, 0.17f, 0.17f,
                                                0.00f, 0.00f, 0.15f, 0.17f, 0.17f, 0.17f,
                                                0.05f, 0.00f, 0.00f, 0.17f, 0.17f, 0.17f)
                                    }
                                },

                                {
                                    "front",
                                    new face(-0.085f, 0.0f, 0.075f)
                                    {
                                        new tri(0.00f, 0.01f, 0.0f, 0.20f, 0.20f, 0.20f,
                                                0.17f, 0.01f, 0.0f, 0.20f, 0.20f, 0.20f,
                                                0.00f, 0.00f, 0.0f, 0.20f, 0.20f, 0.20f),

                                        new tri(0.17f, 0.01f, 0.0f, 0.20f, 0.20f, 0.20f,
                                                0.00f, 0.00f, 0.0f, 0.20f, 0.20f, 0.20f,
                                                0.17f, 0.00f, 0.0f, 0.20f, 0.20f, 0.20f)
                                    }
                                },

                                {
                                    "back",
                                    new face(-0.135f, 0.0f, -0.075f)
                                    {
                                        new tri(0.00f, 0.01f, 0.0f, 0.10f, 0.10f, 0.10f,
                                                0.27f, 0.01f, 0.0f, 0.10f, 0.10f, 0.10f,
                                                0.00f, 0.00f, 0.0f, 0.10f, 0.10f, 0.10f),

                                        new tri(0.27f, 0.01f, 0.0f, 0.10f, 0.10f, 0.10f,
                                                0.00f, 0.00f, 0.0f, 0.10f, 0.10f, 0.10f,
                                                0.27f, 0.00f, 0.0f, 0.10f, 0.10f, 0.10f)
                                    }
                                }
                            }
                        }
                    };
        }

        private scene_object Pot(float offset_x = 0.0f, float offset_y = 0.0f, float offset_z = 0.0f)
        {
            return new scene_object(offset_x, offset_y, offset_z)
                    {
                        {
                            "body",
                            new piece
                            {
                                {
                                    "base",
                                    new face
                                    {
                                        new tri( 0.000f,  0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f,
                                                -0.100f,  0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f,
                                                -0.050f,  0.000f, -0.087f, 0.1216f, 0.4118f, 0.50196f),

                                        new tri( 0.000f,  0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f,
                                                -0.050f,  0.000f, -0.087f, 0.1216f, 0.4118f, 0.50196f,
                                                 0.050f,  0.000f, -0.087f, 0.1216f, 0.4118f, 0.50196f),

                                        new tri( 0.000f,  0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f,
                                                 0.050f,  0.000f, -0.087f, 0.1216f, 0.4118f, 0.50196f,
                                                 0.100f,  0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f),

                                        new tri( 0.000f,  0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f,
                                                -0.100f,  0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f,
                                                -0.050f,  0.000f,  0.087f, 0.1216f, 0.4118f, 0.50196f),

                                        new tri( 0.000f,  0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f,
                                                -0.050f,  0.000f,  0.087f, 0.1216f, 0.4118f, 0.50196f,
                                                 0.050f,  0.000f,  0.087f, 0.1216f, 0.4118f, 0.50196f),

                                        new tri( 0.000f,  0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f,
                                                 0.050f,  0.000f,  0.087f, 0.1216f, 0.4118f, 0.50196f,
                                                 0.100f,  0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f)
                                    }
                                },

                                {
                                    "base fix",
                                    new face(0.0f, 0.001f, 0.0f)
                                    {
                                        new tri( 0.000f,  0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f,
                                                -0.100f,  0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f,
                                                -0.050f,  0.000f, -0.087f, 0.1216f, 0.4118f, 0.50196f),

                                        new tri( 0.000f,  0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f,
                                                -0.050f,  0.000f, -0.087f, 0.1216f, 0.4118f, 0.50196f,
                                                 0.050f,  0.000f, -0.087f, 0.1216f, 0.4118f, 0.50196f),

                                        new tri( 0.000f,  0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f,
                                                 0.050f,  0.000f, -0.087f, 0.1216f, 0.4118f, 0.50196f,
                                                 0.100f,  0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f),

                                        new tri( 0.000f,  0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f,
                                                -0.100f,  0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f,
                                                -0.050f,  0.000f,  0.087f, 0.1216f, 0.4118f, 0.50196f),

                                        new tri( 0.000f,  0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f,
                                                -0.050f,  0.000f,  0.087f, 0.1216f, 0.4118f, 0.50196f,
                                                 0.050f,  0.000f,  0.087f, 0.1216f, 0.4118f, 0.50196f),

                                        new tri( 0.000f,  0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f,
                                                 0.050f,  0.000f,  0.087f, 0.1216f, 0.4118f, 0.50196f,
                                                 0.100f,  0.000f,  0.000f, 0.1216f, 0.4118f, 0.50196f)
                                    }
                                },

                                {
                                    "panza inferior frente",
                                    new face
                                    {
                                        new tri(-0.05f, 0.000f, 0.087f,  0.2416f, 0.5318f, 0.63196f,
                                                 0.05f, 0.000f, 0.087f,  0.2416f, 0.5318f, 0.63196f,
                                                 0.10f, 0.173f, 0.173f,  0.2416f, 0.5318f, 0.63196f),

                                        new tri(-0.05f, 0.000f, 0.087f,  0.2416f, 0.5318f, 0.63196f,
                                                 0.10f, 0.173f, 0.173f,  0.2416f, 0.5318f, 0.63196f,
                                                -0.10f, 0.173f, 0.173f,  0.2456f, 0.5318f, 0.63196f)
                                    }
                                },

                                {
                                    "panza inferior frente izquierda",
                                    new face
                                    {
                                        new tri(-0.10f, 0.000f, 0.000f,  0.2116f, 0.5018f, 0.60196f,
                                                -0.05f, 0.000f, 0.087f,  0.2116f, 0.5018f, 0.60196f,
                                                -0.10f, 0.173f, 0.173f,  0.2116f, 0.5018f, 0.60196f),

                                        new tri(-0.10f, 0.000f, 0.000f,  0.2116f, 0.5018f, 0.60196f,
                                                -0.20f, 0.173f, 0.000f,  0.2116f, 0.5018f, 0.60196f,
                                                -0.10f, 0.173f, 0.173f,  0.2116f, 0.5018f, 0.60196f)
                                    }
                                },

                                {
                                    "panza inferior frente derecha",
                                    new face
                                    {
                                        new tri( 0.10f, 0.000f, 0.000f,  0.2116f, 0.5018f, 0.60196f,
                                                 0.05f, 0.000f, 0.087f,  0.2116f, 0.5018f, 0.60196f,
                                                 0.10f, 0.173f, 0.173f,  0.2116f, 0.5018f, 0.60196f),

                                        new tri( 0.10f, 0.000f, 0.000f,  0.2116f, 0.5018f, 0.60196f,
                                                 0.20f, 0.173f, 0.000f,  0.2116f, 0.5018f, 0.60196f,
                                                 0.10f, 0.173f, 0.173f,  0.2116f, 0.5018f, 0.60196f)
                                    }
                                },

                                {
                                    "panza inferior trasero",
                                    new face
                                    {
                                        new tri(-0.05f, 0.000f,-0.087f,  0.1516f, 0.4418f, 0.53196f,
                                                 0.05f, 0.000f,-0.087f,  0.1516f, 0.4418f, 0.53196f,
                                                 0.10f, 0.173f,-0.173f,  0.1516f, 0.4418f, 0.53196f),

                                        new tri(-0.05f, 0.000f,-0.087f,  0.1516f, 0.4418f, 0.53196f,
                                                 0.10f, 0.173f,-0.173f,  0.1516f, 0.4418f, 0.53196f,
                                                -0.10f, 0.173f,-0.173f,  0.1556f, 0.4418f, 0.53196f)
                                    }
                                },

                                {
                                    "panza inferior trasero izquierda",
                                    new face
                                    {
                                        new tri(-0.10f, 0.000f, 0.000f,  0.1816f, 0.4718f, 0.57196f,
                                                -0.05f, 0.000f,-0.087f,  0.1816f, 0.4718f, 0.57196f,
                                                -0.10f, 0.173f,-0.173f,  0.1816f, 0.4718f, 0.57196f),

                                        new tri(-0.10f, 0.000f, 0.000f,  0.1816f, 0.4718f, 0.57196f,
                                                -0.20f, 0.173f, 0.000f,  0.1816f, 0.4718f, 0.57196f,
                                                -0.10f, 0.173f,-0.173f,  0.1816f, 0.4718f, 0.57196f)
                                    }
                                },

                                {
                                    "panza inferior trasero derecha",
                                    new face
                                    {
                                        new tri( 0.10f, 0.000f, 0.000f,  0.1816f, 0.4718f, 0.57196f,
                                                 0.05f, 0.000f,-0.087f,  0.1816f, 0.4718f, 0.57196f,
                                                 0.10f, 0.173f,-0.173f,  0.1816f, 0.4718f, 0.57196f),

                                        new tri( 0.10f, 0.000f, 0.000f,  0.1816f, 0.4718f, 0.57196f,
                                                 0.20f, 0.173f, 0.000f,  0.1816f, 0.4718f, 0.57196f,
                                                 0.10f, 0.173f,-0.173f,  0.1816f, 0.4718f, 0.57196f)
                                    }
                                },

                                {
                                    "panza superior frente",
                                    new face
                                    {
                                        new tri(-0.05f, 0.346f, 0.087f,  0.2716f, 0.5618f, 0.66196f,
                                                 0.05f, 0.346f, 0.087f,  0.2716f, 0.5618f, 0.66196f,
                                                 0.10f, 0.173f, 0.173f,  0.2716f, 0.5618f, 0.66196f),

                                        new tri(-0.05f, 0.346f, 0.087f,  0.2716f, 0.5618f, 0.66196f,
                                                 0.10f, 0.173f, 0.173f,  0.2716f, 0.5618f, 0.66196f,
                                                -0.10f, 0.173f, 0.173f,  0.2756f, 0.5618f, 0.66196f)
                                    }
                                },

                                {
                                    "panza superior frente izquierda",
                                    new face
                                    {
                                        new tri(-0.10f, 0.346f, 0.000f,  0.2416f, 0.5318f, 0.63196f,
                                                -0.05f, 0.346f, 0.087f,  0.2416f, 0.5318f, 0.63196f,
                                                -0.10f, 0.173f, 0.173f,  0.2416f, 0.5318f, 0.63196f),

                                        new tri(-0.10f, 0.346f, 0.000f,  0.2416f, 0.5318f, 0.63196f,
                                                -0.20f, 0.173f, 0.000f,  0.2416f, 0.5318f, 0.63196f,
                                                -0.10f, 0.173f, 0.173f,  0.2416f, 0.5318f, 0.63196f)
                                    }
                                },

                                {
                                    "panza superior frente derecha",
                                    new face
                                    {
                                        new tri( 0.10f, 0.346f, 0.000f,  0.2416f, 0.5318f, 0.63196f,
                                                 0.05f, 0.346f, 0.087f,  0.2416f, 0.5318f, 0.63196f,
                                                 0.10f, 0.173f, 0.173f,  0.2416f, 0.5318f, 0.63196f),

                                        new tri( 0.10f, 0.346f, 0.000f,  0.2416f, 0.5318f, 0.63196f,
                                                 0.20f, 0.173f, 0.000f,  0.2416f, 0.5318f, 0.63196f,
                                                 0.10f, 0.173f, 0.173f,  0.2416f, 0.5318f, 0.63196f)
                                    }
                                },

                                {
                                    "panza superior trasero",
                                    new face
                                    {
                                        new tri(-0.05f, 0.346f,-0.087f,  0.1816f, 0.4718f, 0.57196f,
                                                 0.05f, 0.346f,-0.087f,  0.1816f, 0.4718f, 0.57196f,
                                                 0.10f, 0.173f,-0.173f,  0.1816f, 0.4718f, 0.57196f),

                                        new tri(-0.05f, 0.346f,-0.087f,  0.1816f, 0.4718f, 0.57196f,
                                                 0.10f, 0.173f,-0.173f,  0.1816f, 0.4718f, 0.57196f,
                                                -0.10f, 0.173f,-0.173f,  0.1816f, 0.4718f, 0.57196f)
                                    }
                                },

                                {
                                    "panza superior trasero izquierda",
                                    new face
                                    {
                                        new tri(-0.10f, 0.346f, 0.000f,  0.2116f, 0.5018f, 0.60196f,
                                                -0.05f, 0.346f,-0.087f,  0.2116f, 0.5018f, 0.60196f,
                                                -0.10f, 0.173f,-0.173f,  0.2116f, 0.5018f, 0.60196f),

                                        new tri(-0.10f, 0.346f, 0.000f,  0.2116f, 0.5018f, 0.60196f,
                                                -0.20f, 0.173f, 0.000f,  0.2116f, 0.5018f, 0.60196f,
                                                -0.10f, 0.173f,-0.173f,  0.2116f, 0.5018f, 0.60196f)
                                    }
                                },

                                {
                                    "panza superior trasero derecha",
                                    new face
                                    {
                                        new tri( 0.10f, 0.346f, 0.000f,  0.2116f, 0.5018f, 0.60196f,
                                                 0.05f, 0.346f,-0.087f,  0.2116f, 0.5018f, 0.60196f,
                                                 0.10f, 0.173f,-0.173f,  0.2116f, 0.5018f, 0.60196f),

                                        new tri( 0.10f, 0.346f, 0.000f,  0.2116f, 0.5018f, 0.60196f,
                                                 0.20f, 0.173f, 0.000f,  0.2116f, 0.5018f, 0.60196f,
                                                 0.10f, 0.173f,-0.173f,  0.2116f, 0.5018f, 0.60196f)
                                    }
                                }
                            }
                        }
                    };
        }

        private scene_object Desk(float offset_x = 0.0f, float offset_y = 0.0f, float offset_z = 0.0f)
        {
            return new scene_object()
                    {
                        {
                            "desktop",
                            new piece(0.0f, 0.72f, 0.0f)
                            {
                                {
                                    "top",
                                    new face(0.0f, 0.03f, 0.0f)
                                    {
                                        new tri(-0.75f, 0.00f, -0.375f, 0.4f, 0.2745f, 0.1804f,
                                                 0.75f, 0.00f, -0.375f, 0.4f, 0.2745f, 0.1804f,
                                                -0.75f, 0.00f,  0.375f, 0.4f, 0.2745f, 0.1804f),

                                        new tri( 0.75f, 0.00f, -0.375f, 0.4f, 0.2745f, 0.1804f,
                                                -0.75f, 0.00f,  0.375f, 0.4f, 0.2745f, 0.1804f,
                                                 0.75f, 0.00f,  0.375f, 0.4f, 0.2745f, 0.1804f)
                                    }
                                },

                                {
                                    "front",
                                    new face(0.0f, 0.0f, 0.375f)
                                    {
                                        new tri(-0.75f,  0.03f, 0.0f, 0.37f, 0.2445f, 0.1504f,
                                                 0.75f,  0.03f, 0.0f, 0.37f, 0.2445f, 0.1504f,
                                                -0.75f, -0.03f, 0.0f, 0.37f, 0.2445f, 0.1504f),

                                        new tri( 0.75f,  0.03f, 0.0f, 0.37f, 0.2445f, 0.1504f,
                                                -0.75f, -0.03f, 0.0f, 0.37f, 0.2445f, 0.1504f,
                                                 0.75f, -0.03f, 0.0f, 0.37f, 0.2445f, 0.1504f)
                                    }
                                },

                                {
                                    "left",
                                    new face(-0.75f, 0.0f, 0.0f)
                                    {
                                        new tri(0.0f,  0.03f, -0.375f, 0.34f, 0.2145f, 0.1204f,
                                                0.0f,  0.03f,  0.375f, 0.34f, 0.2145f, 0.1204f,
                                                0.0f, -0.03f, -0.375f, 0.34f, 0.2145f, 0.1204f),

                                        new tri(0.0f,  0.03f,  0.375f, 0.34f, 0.2145f, 0.1204f,
                                                0.0f, -0.03f, -0.375f, 0.34f, 0.2145f, 0.1204f,
                                                0.0f, -0.03f,  0.375f, 0.34f, 0.2145f, 0.1204f)
                                    }
                                },

                                {
                                    "right",
                                    new face(0.75f, 0.0f, 0.0f)
                                    {
                                        new tri(0.0f,  0.03f, -0.375f, 0.34f, 0.2145f, 0.1204f,
                                                0.0f,  0.03f,  0.375f, 0.34f, 0.2145f, 0.1204f,
                                                0.0f, -0.03f, -0.375f, 0.34f, 0.2145f, 0.1204f),

                                        new tri(0.0f,  0.03f,  0.375f, 0.34f, 0.2145f, 0.1204f,
                                                0.0f, -0.03f, -0.375f, 0.34f, 0.2145f, 0.1204f,
                                                0.0f, -0.03f,  0.375f, 0.34f, 0.2145f, 0.1204f)
                                    }
                                },

                                {
                                    "back",
                                    new face(0.0f, 0.0f, -0.375f)
                                    {
                                        new tri(-0.75f,  0.03f, 0.0f, 0.31f, 0.1845f, 0.0904f,
                                                 0.75f,  0.03f, 0.0f, 0.31f, 0.1845f, 0.0904f,
                                                -0.75f, -0.03f, 0.0f, 0.31f, 0.1845f, 0.0904f),

                                        new tri( 0.75f,  0.03f, 0.0f, 0.31f, 0.1845f, 0.0904f,
                                                -0.75f, -0.03f, 0.0f, 0.31f, 0.1845f, 0.0904f,
                                                 0.75f, -0.03f, 0.0f, 0.31f, 0.1845f, 0.0904f)
                                    }
                                },

                                {
                                    "bottom",
                                    new face
                                    {
                                        new tri(-0.75f, -0.03f, -0.375f, 0.28f, 0.1545f, 0.0604f,
                                                 0.75f, -0.03f, -0.375f, 0.28f, 0.1545f, 0.0604f,
                                                -0.75f, -0.03f,  0.375f, 0.28f, 0.1545f, 0.0604f),

                                        new tri( 0.75f, -0.03f, -0.375f, 0.28f, 0.1545f, 0.0604f,
                                                -0.75f, -0.03f,  0.375f, 0.28f, 0.1545f, 0.0604f,
                                                 0.75f, -0.03f,  0.375f, 0.28f, 0.1545f, 0.0604f)
                                    }
                                }
                            }
                        },

                        {
                            "left foot",
                            new piece(-0.67f)
                            {
                                {
                                    "right",
                                    new face(0.03f)
                                    {
                                        new tri(0.0f, 0.69f, -0.315f, 0.25f, 0.1245f, 0.0304f,
                                                0.0f, 0.69f,  0.315f, 0.25f, 0.1245f, 0.0304f,
                                                0.0f, 0.00f, -0.315f, 0.25f, 0.1245f, 0.0304f),

                                        new tri(0.0f, 0.69f,  0.315f, 0.25f, 0.1245f, 0.0304f,
                                                0.0f, 0.00f, -0.315f, 0.25f, 0.1245f, 0.0304f,
                                                0.0f, 0.00f,  0.315f, 0.25f, 0.1245f, 0.0304f)
                                    }
                                },

                                {
                                    "front",
                                    new face(0.0f, 0.0f, 0.315f)
                                    {
                                        new tri(-0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                                 0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                                -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f),

                                        new tri( 0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                                -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                                 0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f)
                                    }
                                },

                                {
                                    "left",
                                    new face(-0.03f)
                                    {
                                        new tri(0.0f, 0.69f, -0.315f, 0.31f, 0.1845f, 0.0904f,
                                                0.0f, 0.69f,  0.315f, 0.31f, 0.1845f, 0.0904f,
                                                0.0f, 0.00f, -0.315f, 0.31f, 0.1845f, 0.0904f),

                                        new tri(0.0f, 0.69f,  0.315f, 0.31f, 0.1845f, 0.0904f,
                                                0.0f, 0.00f, -0.315f, 0.31f, 0.1845f, 0.0904f,
                                                0.0f, 0.00f,  0.315f, 0.31f, 0.1845f, 0.0904f)
                                    }
                                },

                                {
                                    "back",
                                    new face(0.0f, 0.0f, -0.315f)
                                    {
                                        new tri(-0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                                 0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                                -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f),

                                        new tri( 0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                                -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                                 0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f)
                                    }
                                },

                                {
                                    "bottom",
                                    new face
                                    {
                                        new tri(-0.03f, 0.0f, -0.315f, 0.22f, 0.0945f, 0.0004f,
                                                 0.03f, 0.0f, -0.315f, 0.22f, 0.0945f, 0.0004f,
                                                -0.03f, 0.0f,  0.315f, 0.22f, 0.0945f, 0.0004f),

                                        new tri( 0.03f, 0.0f, -0.315f, 0.22f, 0.0945f, 0.0004f,
                                                -0.03f, 0.0f,  0.315f, 0.22f, 0.0945f, 0.0004f,
                                                 0.03f, 0.0f,  0.315f, 0.22f, 0.0945f, 0.0004f)
                                    }
                                }
                            }
                        },

                        {
                            "right foot",
                            new piece(0.67f)
                            {
                                {
                                    "left",
                                    new face(-0.03f)
                                    {
                                        new tri(0.0f, 0.69f, -0.315f, 0.25f, 0.1245f, 0.0304f,
                                                0.0f, 0.69f,  0.315f, 0.25f, 0.1245f, 0.0304f,
                                                0.0f, 0.00f, -0.315f, 0.25f, 0.1245f, 0.0304f),

                                        new tri(0.0f, 0.69f,  0.315f, 0.25f, 0.1245f, 0.0304f,
                                                0.0f, 0.00f, -0.315f, 0.25f, 0.1245f, 0.0304f,
                                                0.0f, 0.00f,  0.315f, 0.25f, 0.1245f, 0.0304f)
                                    }
                                },

                                {
                                    "front",
                                    new face(0.0f, 0.0f, 0.315f)
                                    {
                                        new tri(-0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                                 0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                                -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f),

                                        new tri( 0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                                -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                                 0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f)
                                    }
                                },

                                {
                                    "right",
                                    new face(0.03f)
                                    {
                                        new tri(0.0f, 0.69f, -0.315f, 0.31f, 0.1845f, 0.0904f,
                                                0.0f, 0.69f,  0.315f, 0.31f, 0.1845f, 0.0904f,
                                                0.0f, 0.00f, -0.315f, 0.31f, 0.1845f, 0.0904f),

                                        new tri(0.0f, 0.69f,  0.315f, 0.31f, 0.1845f, 0.0904f,
                                                0.0f, 0.00f, -0.315f, 0.31f, 0.1845f, 0.0904f,
                                                0.0f, 0.00f,  0.315f, 0.31f, 0.1845f, 0.0904f)
                                    }
                                },

                                {
                                    "back",
                                    new face(0.0f, 0.0f, -0.315f)
                                    {
                                        new tri(-0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                                 0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                                -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f),

                                        new tri( 0.03f, 0.69f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                                -0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f,
                                                 0.03f, 0.00f, 0.0f, 0.28f, 0.1545f, 0.0604f)
                                    }
                                },

                                {
                                    "bottom",
                                    new face
                                    {
                                        new tri(-0.03f, 0.0f, -0.315f, 0.22f, 0.0945f, 0.0004f,
                                                 0.03f, 0.0f, -0.315f, 0.22f, 0.0945f, 0.0004f,
                                                -0.03f, 0.0f,  0.315f, 0.22f, 0.0945f, 0.0004f),

                                        new tri( 0.03f, 0.0f, -0.315f, 0.22f, 0.0945f, 0.0004f,
                                                -0.03f, 0.0f,  0.315f, 0.22f, 0.0945f, 0.0004f,
                                                 0.03f, 0.0f,  0.315f, 0.22f, 0.0945f, 0.0004f)
                                    }
                                }
                            }
                        }
                    };
        }
    }
}