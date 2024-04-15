using OpenTK.Mathematics;
using System;

namespace ConsoleApp1
{
	public class face: HashSet<tri>
    {
        private Matrix4 pitch, roll, yaw;
        public float offset_x, offset_y, offset_z;
        public face(float offset_x = 0.0f, float offset_y = 0.0f, float offset_z = 0.0f)
		{
            this.offset_x = offset_x;
            this.offset_y = offset_y;
            this.offset_z = offset_z;

            pitch = roll = yaw = Matrix4.Identity;
        }

		public void draw(Shader shader, Matrix4 model, Matrix4 view, Matrix4 projection, double time)
		{
			foreach (tri tri in this)
			{
				tri.draw(shader, yaw * Matrix4.CreateTranslation(offset_x, offset_y, offset_z) * model, view, projection, time);
			}

        }

        public void rotate_Y(float delta)
        {
            yaw = yaw * Matrix4.CreateRotationY(delta);
        }
    }
}
