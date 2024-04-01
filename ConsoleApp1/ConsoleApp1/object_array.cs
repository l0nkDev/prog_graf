using OpenTK.Mathematics;
using System;
using System.Runtime.InteropServices;

namespace ConsoleApp1 { 
	public class object_array: List<world_object>
	{
		public void draw(Shader shader, Matrix4 view, Matrix4 projection, double time)
		{
			foreach(world_object obj in this) {
				obj.draw(shader, view, projection, time);
			}		
		}

    }
}
