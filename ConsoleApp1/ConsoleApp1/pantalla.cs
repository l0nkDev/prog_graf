using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using System.Diagnostics;
using System.IO;
using System.Drawing;

namespace ConsoleApp1 { 
	public class pantalla
    {
        private Vector3 vert1, vert2, vert3, color1, color2, color3;

        public int VertexBufferObject;
        public int ElementBufferObject;
        public int VertexArrayObject;
        public Matrix4 model;
        Vector4 pos;
        Vector3 rot;

        private float[] vertices =
{
              // positions        // colors            //punto de referencia 0, 0 local
             
             50.0f, -20.00f, 0.0f,  0.24f,  1.0f, 0.24f,   // pantalla
             50.0f,  36.25f, 0.0f,  1.00f, 0.24f, 0.24f,
            -50.0f, -20.00f, 0.0f,  0.24f, 0.24f,  1.0f};

        private uint[] indices = {0, 1, 2};

        public pantalla(Vector3 v1, Vector3 c1, Vector3 v2, Vector3 c2, Vector3 v3, Vector3 c3, Vector3 position)
		{
            vert1 = v1;
            vert2 = v2;
            vert3 = v3;
            color1 = c1;
            color2 = c2;
            color3 = c3;

            int i = 0;
            while (i < vertices.Length) { Console.WriteLine(vertices[i]); i++; }

            vertices[0] = vert1.X; vertices[1] = vert1.Y; vertices[2] = vert1.Z; vertices[3] = color1.X; vertices[4] = color1.Y; vertices[5] = color1.Z;
            vertices[6] = vert2.X; vertices[7] = vert2.Y; vertices[8] = vert2.Z; vertices[9] = color2.X; vertices[10] = color2.Y; vertices[11] = color2.Z;
            vertices[12] = vert3.X; vertices[13] = vert3.Y; vertices[14] = vert3.Z; vertices[15] = color3.X; vertices[16] = color3.Y; vertices[17] = color3.Z;

            Console.WriteLine("");

            i = 0;
            while (i < vertices.Length) { Console.WriteLine(vertices[i]); i++; }

            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            pos = new Vector4(position.X, position.Y, position.Z, 1.0f);
            rot = new Vector3(0.0f, 0.0f, 0.0f);
            model = Matrix4.Identity * Matrix4.CreateTranslation(new Vector3(pos.X, pos.Y, pos.Z));
        }

        public void draw(Shader shader, Matrix4 view, Matrix4 projection, double time)
        {

            model =Matrix4.CreateTranslation(new Vector3(pos.X, pos.Y, pos.Z));
            GL.BindVertexArray(VertexArrayObject);
            shader.Use();
            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", view);
            shader.SetMatrix4("projection", projection);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            }
    }
}
