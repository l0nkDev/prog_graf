using Newtonsoft.Json;
using OpenTK.Mathematics;
using System;
using System.Runtime.CompilerServices;




namespace ConsoleApp1
{
    [JsonObjectAttribute]
    public class Piece : Dictionary<string, Face>
    {
        public bool visible = true;

        private Matrix4 pitch, roll, yaw;

        public float offset_x, offset_y, offset_z = 0.0f;
		public Piece(float offset_x = 0.0f, float offset_y = 0.0f, float offset_z = 0.0f)
		{
			this.offset_x = offset_x;
			this.offset_y = offset_y;
			this.offset_z = offset_z;

            pitch = roll = yaw = Matrix4.Identity;
        }

        public Piece()
        {
            pitch = roll = yaw = Matrix4.Identity;
        }

		public void Draw(Shader shader, Matrix4 model, Matrix4 view, Matrix4 projection, double time)
		{
            if (visible) 
            { 
			    foreach (Face face in this.Values)
			    {
				    face.Draw(shader, roll * pitch * yaw * Matrix4.CreateTranslation(offset_x, offset_y, offset_z) * model, view, projection, time);
			    }
            }
        }

		public void RotateY(float delta)
		{
			yaw *= Matrix4.CreateRotationY(delta);
        }

        public void RotateX(float delta)
        {
            pitch *= Matrix4.CreateRotationX(delta);
        }

        public void RotateZ(float delta)
        {
            roll *= Matrix4.CreateRotationZ(delta);
        }
    }
}
