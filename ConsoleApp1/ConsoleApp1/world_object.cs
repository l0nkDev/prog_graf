using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using System.Diagnostics;
using System.IO;
using System;
namespace ConsoleApp1;
	public class world_object
	{
        public int VertexBufferObject;
        public int ElementBufferObject;
        public int VertexArrayObject;
        public Matrix4 model;
        public float[] vertices;
        public uint[] indices;

        public world_object()
		{
		}

        public virtual void draw(Shader shader, Matrix4 view, Matrix4 projection, double time)
        {
        }
}
