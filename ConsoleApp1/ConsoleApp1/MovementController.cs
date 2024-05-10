using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System.Drawing;

namespace JuegoProgramacionGrafica
{
    public class MovementController
    {
        Game game;
        private readonly float speed = 1.5f;

        public Matrix4 View { get { return Matrix4.LookAt(Position, Position + front, up); } }
        public Matrix4 Projection;

        public Vector3 Position;
        public Vector3 front;
        public Vector3 up;

        public float pitch = 0f;
        public float yaw = 0f;
        public readonly float sensitivity = 0.1f;

        private bool firstMove = true;
        public MovementController(Game game)
        {
            this.game = game;
            game.KeyboardInputEvent += OnKeyboardInput;
            game.MouseMove += OnMouseMove;
            game.MouseDown += OnMouseDown;

            Position = new Vector3(0.0f, 0.0f, 3.0f);
            front = new Vector3(0.0f, 0.0f, -1.0f);
            up = Vector3.UnitY;
            GenProjection();
        }

        private void OnKeyboardInput(FrameEventArgs e)
        {
            if (!game.IsFocused || game.CursorState == CursorState.Normal) return;

            if (game.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Escape)) game.Close();

            if (game.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.W)) Position += front * speed * (float)e.Time;

            if (game.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.A)) Position -= Vector3.Normalize(Vector3.Cross(front, up)) * speed * (float)e.Time;

            if (game.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.S)) Position -= front * speed * (float)e.Time;

            if (game.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.D)) Position += Vector3.Normalize(Vector3.Cross(front, up)) * speed * (float)e.Time;

            if (game.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Space)) Position += up * speed * (float)e.Time;

            if (game.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.LeftShift)) Position -= up * speed * (float)e.Time;
        }

        private void OnMouseMove(MouseMoveEventArgs e)
        {
            if (game.IsFocused && game.CursorState == CursorState.Grabbed)
            {
                if (firstMove)
                {
                    firstMove = false;
                    return;
                }

                Vector2 center = new(game.Size.X / 2f, game.Size.Y / 2f);

                float deltaX = game.MousePosition.X - center.X;
                float deltaY = game.MousePosition.Y - center.Y;
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

                game.MousePosition = center;
            }
        }
        private void OnMouseDown(MouseButtonEventArgs e)
        {
            if (game.IsFocused && game.CursorState == CursorState.Grabbed && e.Button == OpenTK.Windowing.GraphicsLibraryFramework.MouseButton.Left) 
                game.CursorState = CursorState.Normal;
            else if (game.IsFocused && game.CursorState == CursorState.Normal && e.Button == OpenTK.Windowing.GraphicsLibraryFramework.MouseButton.Right)
                game.CursorState = CursorState.Grabbed;
        }

        public void GenProjection() { Projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90f), game.Size.X / (float)game.Size.Y, 0.1f, 100.0f); }
    }
}

