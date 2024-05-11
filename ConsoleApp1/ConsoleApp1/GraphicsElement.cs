using OpenTK.Mathematics;
using System.Runtime.Serialization;

namespace JuegoProgramacionGrafica
{
    public class GraphicsElement
    {
        public byte level = 255;
        public float[] _position = { 0.0f, 0.0f, 0.0f };
        public float[] _rotation = { 0.0f, 0.0f, 0.0f };
        public float[] _scale    = { 1.0f, 1.0f, 1.0f };
        private Matrix4 pitch, roll, yaw, position, scale;
        public bool visible = true;
        
        public Dictionary<string, GraphicsElement> children = new();
        public Dictionary<int, Tri> tris = new();

        public GraphicsElement(byte level, bool visible = true,
                               float posX = 0.0f, float posY = 0.0f, float posZ = 0.0f,
                               float rotX = 0.0f, float rotY = 0.0f, float rotZ = 0.0f,
                               float sclX = 1.0f, float sclY = 1.0f, float sclZ = 1.0f)
        {
            this.level = level; this.visible = visible;
            _position = new float[] { posX, posY, posZ };
            _rotation = new float[] { rotX, rotY, rotZ };
            _scale    = new float[] { sclX, sclY, sclZ };

            GenMatrixes(new StreamingContext());
        }

        public GraphicsElement() { position = pitch = yaw = roll = scale = Matrix4.Identity; }

        public void Draw(Shader shader, Matrix4 model, Matrix4 view, Matrix4 projection, double time)
        {
            if (visible)
            {
                foreach (GraphicsElement elem in children.Values)
                {
                    elem.Draw(shader, scale * roll * pitch * yaw * position * model, view, projection, time);
                }
                foreach (Tri tri in tris.Values)
                {
                    tri.Draw(shader, scale * roll * pitch * yaw * position * model, view, projection, time);
                }
            }
        }

        [OnDeserialized]
        private void GenMatrixes(StreamingContext context)
        {
            position = Matrix4.CreateTranslation(_position[0], _position[1], _position[2]);
            pitch = Matrix4.CreateRotationX(_rotation[0]);
            yaw = Matrix4.CreateRotationY(_rotation[1]);
            roll = Matrix4.CreateRotationZ(_rotation[2]);
            scale = Matrix4.CreateScale(_scale[0], _scale[1], _scale[2]);
        }

        public void SetRotation(float pitch, float yaw, float roll)
        {
            this.yaw = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(yaw));
            this.pitch = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(pitch));
            this.roll = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(roll));
            _rotation = new float[] { pitch, yaw, roll };
        }

        public void Rotate(float pitch, float yaw, float roll)
        {
            this.yaw *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(yaw));
            this.pitch *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(pitch));
            this.roll *= Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(roll));
            _rotation = new float[] { _rotation[0] + pitch, _rotation[1] + yaw, _rotation[2] + roll };
        }

        public void SetPosition(float x, float y, float z)
        {
            position = Matrix4.CreateTranslation(x, y, z);
            _position = new float[] { x, y, z };
        }

        public void Move(float x, float y, float z)
        {
            position *= Matrix4.CreateTranslation(x, y, z);
            _position = new float[] { _position[0] + x, _position[1] + y, _position[2] + z };
        }

        public void SetScale(float x, float y, float z)
        {
            scale = Matrix4.CreateScale(x, y, z);
            _scale = new float[] { x, y, z };
        }

        public void Scale(float x, float y, float z)
        {
            scale *= Matrix4.CreateScale(x, y, z);
            _scale = new float[] { _scale[0] + x, _scale[1] + y, _scale[2] + z };
        }

        
    }
}
