using Newtonsoft.Json;
using OpenTK.Mathematics;
using System;
using System.Runtime.Serialization;

namespace JuegoProgramacionGrafica
{
    public class Piece
    {
        public Dictionary<string, Face> Faces = new();

        public float offset_x    = 0.0f, offset_y   = 0.0f, offset_z  = 0.0f;
        public float pitch_value = 0.0f, roll_value = 0.0f, yaw_value = 0.0f;
        public float scale_x     = 1.0f, scale_y    = 1.0f, scale_z   = 1.0f;
        private Matrix4 pitch, roll, yaw, position, scale;
        public bool visible = true;
        public Piece(float offset_x = 0.0f, float offset_y = 0.0f, float offset_z = 0.0f)
		{
			this.offset_x = offset_x;
			this.offset_y = offset_y;
			this.offset_z = offset_z;

            GenMatrixes(new StreamingContext());
        }

        public Piece()
        {
            position = pitch = yaw = roll = scale = Matrix4.Identity;
        }

		public void Draw(Shader shader, Matrix4 model, Matrix4 view, Matrix4 projection, double time)
		{
            if (visible) 
            { 
			    foreach (Face face in Faces.Values)
			    {
				    face.Draw(shader, scale * roll * pitch * yaw * position * model, view, projection, time);
			    }
            }
        }

        [OnDeserialized]
        private void GenMatrixes(StreamingContext context)
        {
            position = Matrix4.CreateTranslation(offset_x, offset_y, offset_z);
            pitch = Matrix4.CreateRotationX(pitch_value);
            yaw = Matrix4.CreateRotationX(yaw_value);
            roll = Matrix4.CreateRotationX(roll_value);
            scale = Matrix4.CreateScale(scale_x, scale_y, scale_z);
        }

        public void SetRotation(float pitch, float yaw, float roll)
        {
            this.yaw = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(yaw));
            this.pitch = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(pitch));
            this.roll = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(roll));
            yaw_value = yaw;
            pitch_value = pitch;
            roll_value = roll;
        }

        public void Rotate(float pitch, float yaw, float roll)
        {
            this.yaw *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(yaw));
            this.pitch *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(pitch));
            this.roll *= Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(roll));
            yaw_value += yaw;
            pitch_value += pitch;
            roll_value += roll;
        }

        public void SetPosition(float x, float y, float z)
        {
            position = Matrix4.CreateTranslation(x, y, z);
            offset_x = x;
            offset_y = y;
            offset_z = z;
        }

        public void Move(float x, float y, float z)
        {
            position *= Matrix4.CreateTranslation(x, y, z);
            offset_x += x;
            offset_y += y;
            offset_z += z;
        }

        public void SetScale(float x, float y, float z)
        {
            scale = Matrix4.CreateScale(x, y, z);
            scale_x = x;
            scale_y = y;
            scale_z = z;
        }

        public void Scale(float x, float y, float z)
        {
            scale *= Matrix4.CreateScale(x, y, z);
            scale_x += x;
            scale_y += y;
            scale_z += z;
        }
    }
}

