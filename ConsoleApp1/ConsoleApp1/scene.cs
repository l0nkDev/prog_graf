using OpenTK.Mathematics;
using System;

namespace ConsoleApp1
{
	public class scene: Dictionary<string, scene_object>
	{
		public float offset_x, offset_y, offset_z;
		public scene(float offset_x = 0.0f, float offset_y = 0.0f, float offset_z = 0.0f)
		{
			this.offset_x = offset_x;
			this.offset_y = offset_y;
			this.offset_z = offset_z;
		}

		public void draw(Shader shader, Matrix4 model, Matrix4 view, Matrix4 projection, double time)
		{
			foreach (scene_object scene_object in this.Values)
			{
				scene_object.draw(shader, model * Matrix4.CreateTranslation(offset_x, offset_y, offset_z), view, projection, time);
			}

		}
	}
}
