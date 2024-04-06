using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using System.Diagnostics;
using System.IO;

namespace ConsoleApp1 { 
	public class pantalla : world_object
	{

        public new int VertexBufferObject;
        public new int ElementBufferObject;
        public new int VertexArrayObject;
        public new Matrix4 model;
        Vector4 pos;
        Vector3 rot;

        public new float[] vertices =
{
              // positions        // colors            //punto de referencia 0, 0 local
             
             50.0f, -20.00f, 0.0f,  0.24f,  1.0f, 0.24f,   // pantalla
             50.0f,  36.25f, 0.0f,  1.00f, 0.24f, 0.24f,
            -50.0f, -20.00f, 0.0f,  0.24f, 0.24f,  1.0f,
            -50.0f,  36.25f, 0.0f,  0.24f, 0.24f, 0.24f,

             50.0f, -20.0f, 0.0f,  0.24f, 0.24f, 0.24f,   // parlantes
             50.0f, -30.0f, 0.0f,  0.24f, 0.24f, 0.24f,
            -50.0f, -20.0f, 0.0f,  0.24f, 0.24f, 0.24f,
            -50.0f, -30.0f, 0.0f,  0.24f, 0.24f, 0.24f,

            -2.5f, -30.0f, -02.5f,  0.13f, 0.13f, 0.13f,  // pata
             2.5f, -30.0f, -02.5f,  0.13f, 0.13f, 0.13f,
            -2.5f, -60.0f, -02.5f,  0.13f, 0.13f, 0.13f,
             2.5f, -60.0f, -02.5f,  0.13f, 0.13f, 0.13f,

             50.0f, -30.00f, -10.0f,  0.10f, 0.10f, 0.10f,  // parte trasera
             50.0f,  36.25f, -10.0f,  0.10f, 0.10f, 0.10f,
            -50.0f, -30.00f, -10.0f,  0.10f, 0.10f, 0.10f,
            -50.0f,  36.25f, -10.0f,  0.10f, 0.10f, 0.10f,

             50.0f,  36.25f,  0.00f, 0.17f, 0.17f, 0.17f,  // lado izquierdo 
             50.0f, -30.00f,  0.00f, 0.17f, 0.17f, 0.17f,
             50.0f,  36.25f, -10.0f, 0.17f, 0.17f, 0.17f,
             50.0f, -30.00f, -10.0f, 0.17f, 0.17f, 0.17f,

            -50.0f,  36.25f,  0.0f, 0.17f, 0.17f, 0.17f,  // lado derecho 
            -50.0f, -30.00f,  0.0f, 0.17f, 0.17f, 0.17f,
            -50.0f,  36.25f, -10.0f, 0.17f, 0.17f, 0.17f,
            -50.0f, -30.00f, -10.0f, 0.17f, 0.17f, 0.17f,

            -50.0f, 36.25f,  0.00f, 0.20f, 0.20f, 0.20f,   // lado superior
             50.0f, 36.25f,  0.00f, 0.20f, 0.20f, 0.20f,
            -50.0f, 36.25f, -10.0f, 0.20f, 0.20f, 0.20f,
             50.0f, 36.25f, -10.0f, 0.20f, 0.20f, 0.20f,

            -50.0f, -30.0f,  0.00f, 0.13f, 0.13f, 0.13f,   // lado inferior
             50.0f, -30.0f,  0.00f, 0.13f, 0.13f, 0.13f,
            -50.0f, -30.0f, -10.0f, 0.13f, 0.13f, 0.13f,
             50.0f, -30.0f, -10.0f, 0.13f, 0.13f, 0.13f,

            -2.5f, -30.0f, -07.5f,  0.07f, 0.07f, 0.07f,  // pata lado trasero
             2.5f, -30.0f, -07.5f,  0.07f, 0.07f, 0.07f,
            -2.5f, -60.0f, -07.5f,  0.07f, 0.07f, 0.07f,
             2.5f, -60.0f, -07.5f,  0.07f, 0.07f, 0.07f,

            -2.5f, -30.0f, -07.5f,  0.10f, 0.10f, 0.10f,  // pata lado izquierdo
            -2.5f, -30.0f, -02.5f,  0.10f, 0.10f, 0.10f,
            -2.5f, -60.0f, -07.5f,  0.10f, 0.10f, 0.10f,
            -2.5f, -60.0f, -02.5f,  0.10f, 0.10f, 0.10f,

            2.5f, -30.0f, -07.5f,  0.10f, 0.10f, 0.10f,  // pata lado derecho
            2.5f, -30.0f, -02.5f,  0.10f, 0.10f, 0.10f,
            2.5f, -60.0f, -07.5f,  0.10f, 0.10f, 0.10f,
            2.5f, -60.0f, -02.5f,  0.10f, 0.10f, 0.10f,

            -15.0f, -60.0f,  10.0f, 0.23f, 0.23f, 0.23f,   // base lado superior
             15.0f, -60.0f,  10.0f, 0.23f, 0.23f, 0.23f,
            -25.0f, -60.0f, -20.0f, 0.23f, 0.23f, 0.23f,
             25.0f, -60.0f, -20.0f, 0.23f, 0.23f, 0.23f,

            -15.0f, -62.5f,  10.0f, 0.13f, 0.13f, 0.13f,   // base lado inferior
             15.0f, -62.5f,  10.0f, 0.13f, 0.13f, 0.13f,
            -25.0f, -62.5f, -20.0f, 0.13f, 0.13f, 0.13f,
             25.0f, -62.5f, -20.0f, 0.13f, 0.13f, 0.13f,

            -25.0f, -60.0f,  -20.0f, 0.10f, 0.10f, 0.10f,   // base lado trasero
             25.0f, -60.0f,  -20.0f, 0.10f, 0.10f, 0.10f,
            -25.0f, -62.5f,  -20.0f, 0.10f, 0.10f, 0.10f,
             25.0f, -62.5f,  -20.0f, 0.10f, 0.10f, 0.10f,

            -15.0f, -60.0f,  10.0f, 0.20f, 0.20f, 0.20f,   // base lado delantero
             15.0f, -60.0f,  10.0f, 0.20f, 0.20f, 0.20f,
            -15.0f, -62.5f,  10.0f, 0.20f, 0.20f, 0.20f,
             15.0f, -62.5f,  10.0f, 0.20f, 0.20f, 0.20f,

            -25.0f, -60.0f,  -20.0f, 0.17f, 0.17f, 0.17f,   // base lado izquierdo
            -15.0f, -60.0f,   10.0f, 0.17f, 0.17f, 0.17f,
            -25.0f, -62.5f,  -20.0f, 0.17f, 0.17f, 0.17f,
            -15.0f, -62.5f,   10.0f, 0.17f, 0.17f, 0.17f,

             25.0f, -60.0f,  -20.0f, 0.17f, 0.17f, 0.17f,   // base lado derecho
             15.0f, -60.0f,   10.0f, 0.17f, 0.17f, 0.17f,
             25.0f, -62.5f,  -20.0f, 0.17f, 0.17f, 0.17f,
             15.0f, -62.5f,   10.0f, 0.17f, 0.17f, 0.17f,

             01.2f, -23.8f, 0.001f,  1.0f, 1.0f, 1.0f,   // logo
             01.2f, -26.2f, 0.001f,  1.0f, 1.0f, 1.0f,
            -01.2f, -23.8f, 0.001f,  1.0f, 1.0f, 1.0f,
            -01.2f, -26.2f, 0.001f,  1.0f, 1.0f, 1.0f
        };

        public new uint[] indices;

        public pantalla(float x = 0.0f, float y = 0.0f, float z = 0.0f, float yaw = 0.0f)
		{
            Console.WriteLine("New Screen at: {0}, {1}, {2}", x, y, z);
            indices = new uint[vertices.Length / 4];
            uint tmp = 0;
            uint c = 0;
            while (c < indices.Length)
            {
                indices[c] = tmp;
                indices[c + 1] = tmp + 1;
                indices[c + 2] = tmp + 2;
                indices[c + 3] = tmp + 1;
                indices[c + 4] = tmp + 2;
                indices[c + 5] = tmp + 3;
                tmp += 4;
                c += 6;
            }

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

            pos = new Vector4(x, y, z, 1.0f);
            rot = new Vector3(0.0f, 0.0f, 0.0f);
            model = Matrix4.Identity * Matrix4.CreateTranslation(new Vector3(pos.X, pos.Y, pos.Z));
        }

        public override void draw(Shader shader, Matrix4 view, Matrix4 projection, double time)
        {

            model = Matrix4.CreateScale(0.01f, 0.01f, 0.01f) * Matrix4.CreateTranslation(new Vector3(pos.X, pos.Y, pos.Z));
            GL.BindVertexArray(VertexArrayObject);
            shader.Use();
            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", view);
            shader.SetMatrix4("projection", projection);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            }

        public void rotate(double time)
        {
            rot.Y += 45f * (float)time;
            pos = pos * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(45f * (float)time));
        }
    }
}
