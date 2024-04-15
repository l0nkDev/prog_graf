using OpenTK.Mathematics;
using System;

namespace ConsoleApp1
{
	public class scene_object: Dictionary<string, piece>
	{
        private Matrix4 pitch, roll, yaw;
        public float offset_x, offset_y, offset_z;
		public scene_object(float offset_x = 0.0f, float offset_y = 0.0f, float offset_z = 0.0f)
		{
			this.offset_x = offset_x;
			this.offset_y = offset_y;
			this.offset_z = offset_z;

            pitch = roll = yaw = Matrix4.Identity;
        }

		public void draw(Shader shader, Matrix4 model, Matrix4 view, Matrix4 projection, double time)
		{
			foreach (piece piece in this.Values)
			{
				piece.draw(shader, roll * pitch * yaw * Matrix4.CreateTranslation(offset_x, offset_y, offset_z) * model, view, projection, time);
			}

		}

        public void rotate_Y(float delta)
        {
            yaw = yaw * Matrix4.CreateRotationY(delta);
        }

        public void rotate_X(float delta)
        {
            pitch = pitch * Matrix4.CreateRotationX(delta);
        }

        public void rotate_Z(float delta)
        {
            roll = roll * Matrix4.CreateRotationZ(delta);
        }
    }
}
