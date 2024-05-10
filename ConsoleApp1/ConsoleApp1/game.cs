using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using Zenseless.OpenTK.GUI;
using Microsoft.VisualBasic;

namespace JuegoProgramacionGrafica
{
    public class Game : GameWindow
    {
        public ImGuiFacade gui;
        private GameUI ui_handler;
        private MovementController movement;
        public event InputEventHandler KeyboardInputEvent;

        public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title }) { }

        private Shader shader;

        private double elapsed_second = 0;
        private int current_second_frames = 0;
        public int fps = 0;

        public Dictionary<string, GraphicsElement> elem = new();

        protected override void OnLoad()
        {
            base.OnLoad();
            MousePosition = new Vector2(Size.X / 2f, Size.Y / 2f);

            GL.ClearColor(0.25882353f, 0.72549f, 0.96f, 1.0f);

            shader = new Shader("../../../shaders/shader.vert", "../../../shaders/shader.frag");

            gui = new(this);
            ui_handler = new(this);
            movement = new(this);

            FileUtils.SerializeExamples("../../../assets/objects/");
            elem.Add("main scene", FileUtils.Deserialize("../../../assets/objects/main scene.json"));
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

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            foreach (GraphicsElement elm in elem.Values)
            {
                elm.Draw(shader, Matrix4.Identity, movement.View, movement.Projection, args.Time);
            }

            ui_handler.Render();
            SwapBuffers();
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);
            movement.GenProjection();
            GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
        }

        protected override void OnUpdateFrame(FrameEventArgs args) { base.OnUpdateFrame(args); if (KeyboardState.IsAnyKeyDown) KeyboardInputEvent?.Invoke(args); }

        protected override void OnUnload() { shader.Dispose(); }

        public delegate void InputEventHandler(FrameEventArgs e);
    }
}