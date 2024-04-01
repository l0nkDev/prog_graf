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
              // positions        // colors
             
             0.5f, -0.2000f, 0.0f,  0.24f,  1.0f, 0.24f,   // pantalla
             0.5f,  0.3625f, 0.0f,  1.00f, 0.24f, 0.24f,
            -0.5f, -0.2000f, 0.0f,  0.24f, 0.24f,  1.0f,
            -0.5f,  0.3625f, 0.0f,  0.24f, 0.24f, 0.24f,

             0.5f, -0.2f, 0.0f,  0.24f, 0.24f, 0.24f,   // parlantes
             0.5f, -0.3f, 0.0f,  0.24f, 0.24f, 0.24f,
            -0.5f, -0.2f, 0.0f,  0.24f, 0.24f, 0.24f,
            -0.5f, -0.3f, 0.0f,  0.24f, 0.24f, 0.24f,

            -0.025f, -0.3f, -0.025f,  0.13f, 0.13f, 0.13f,  // pata
             0.025f, -0.3f, -0.025f,  0.13f, 0.13f, 0.13f,
            -0.025f, -0.6f, -0.025f,  0.13f, 0.13f, 0.13f,
             0.025f, -0.6f, -0.025f,  0.13f, 0.13f, 0.13f,

             0.5f, -0.3000f, -0.1f,  0.10f, 0.10f, 0.10f,  // parte trasera
             0.5f,  0.3625f, -0.1f,  0.10f, 0.10f, 0.10f,
            -0.5f, -0.3000f, -0.1f,  0.10f, 0.10f, 0.10f,
            -0.5f,  0.3625f, -0.1f,  0.10f, 0.10f, 0.10f,

             0.5f,  0.3625f,  0.0f, 0.17f, 0.17f, 0.17f,  // lado izquierdo 
             0.5f, -0.3000f,  0.0f, 0.17f, 0.17f, 0.17f,
             0.5f,  0.3625f, -0.1f, 0.17f, 0.17f, 0.17f,
             0.5f, -0.3000f, -0.1f, 0.17f, 0.17f, 0.17f,

            -0.5f,  0.3625f,  0.0f, 0.17f, 0.17f, 0.17f,  // lado derecho 
            -0.5f, -0.3000f,  0.0f, 0.17f, 0.17f, 0.17f,
            -0.5f,  0.3625f, -0.1f, 0.17f, 0.17f, 0.17f,
            -0.5f, -0.3000f, -0.1f, 0.17f, 0.17f, 0.17f,

            -0.5f, 0.3625f,  0.0f, 0.20f, 0.20f, 0.20f,   // lado superior
             0.5f, 0.3625f,  0.0f, 0.20f, 0.20f, 0.20f,
            -0.5f, 0.3625f, -0.1f, 0.20f, 0.20f, 0.20f,
             0.5f, 0.3625f, -0.1f, 0.20f, 0.20f, 0.20f,

            -0.5f, -0.3f,  0.0f, 0.13f, 0.13f, 0.13f,   // lado inferior
             0.5f, -0.3f,  0.0f, 0.13f, 0.13f, 0.13f,
            -0.5f, -0.3f, -0.1f, 0.13f, 0.13f, 0.13f,
             0.5f, -0.3f, -0.1f, 0.13f, 0.13f, 0.13f,

            -0.025f, -0.3f, -0.075f,  0.07f, 0.07f, 0.07f,  // pata lado trasero
             0.025f, -0.3f, -0.075f,  0.07f, 0.07f, 0.07f,
            -0.025f, -0.6f, -0.075f,  0.07f, 0.07f, 0.07f,
             0.025f, -0.6f, -0.075f,  0.07f, 0.07f, 0.07f,

            -0.025f, -0.3f, -0.075f,  0.10f, 0.10f, 0.10f,  // pata lado izquierdo
            -0.025f, -0.3f, -0.025f,  0.10f, 0.10f, 0.10f,
            -0.025f, -0.6f, -0.075f,  0.10f, 0.10f, 0.10f,
            -0.025f, -0.6f, -0.025f,  0.10f, 0.10f, 0.10f,

            0.025f, -0.3f, -0.075f,  0.10f, 0.10f, 0.10f,  // pata lado derecho
            0.025f, -0.3f, -0.025f,  0.10f, 0.10f, 0.10f,
            0.025f, -0.6f, -0.075f,  0.10f, 0.10f, 0.10f,
            0.025f, -0.6f, -0.025f,  0.10f, 0.10f, 0.10f,

            -0.15f, -0.6f,  0.1f, 0.23f, 0.23f, 0.23f,   // base lado superior
             0.15f, -0.6f,  0.1f, 0.23f, 0.23f, 0.23f,
            -0.25f, -0.6f, -0.2f, 0.23f, 0.23f, 0.23f,
             0.25f, -0.6f, -0.2f, 0.23f, 0.23f, 0.23f,

            -0.15f, -0.625f,  0.1f, 0.13f, 0.13f, 0.13f,   // base lado inferior
             0.15f, -0.625f,  0.1f, 0.13f, 0.13f, 0.13f,
            -0.25f, -0.625f, -0.2f, 0.13f, 0.13f, 0.13f,
             0.25f, -0.625f, -0.2f, 0.13f, 0.13f, 0.13f,

            -0.25f, -0.600f,  -0.2f, 0.10f, 0.10f, 0.10f,   // base lado trasero
             0.25f, -0.600f,  -0.2f, 0.10f, 0.10f, 0.10f,
            -0.25f, -0.625f,  -0.2f, 0.10f, 0.10f, 0.10f,
             0.25f, -0.625f,  -0.2f, 0.10f, 0.10f, 0.10f,

            -0.15f, -0.600f,  0.1f, 0.20f, 0.20f, 0.20f,   // base lado delantero
             0.15f, -0.600f,  0.1f, 0.20f, 0.20f, 0.20f,
            -0.15f, -0.625f,  0.1f, 0.20f, 0.20f, 0.20f,
             0.15f, -0.625f,  0.1f, 0.20f, 0.20f, 0.20f,

            -0.25f, -0.600f,  -0.2f, 0.17f, 0.17f, 0.17f,   // base lado izquierdo
            -0.15f, -0.600f,   0.1f, 0.17f, 0.17f, 0.17f,
            -0.25f, -0.625f,  -0.2f, 0.17f, 0.17f, 0.17f,
            -0.15f, -0.625f,   0.1f, 0.17f, 0.17f, 0.17f,

             0.25f, -0.600f,  -0.2f, 0.17f, 0.17f, 0.17f,   // base lado derecho
             0.15f, -0.600f,   0.1f, 0.17f, 0.17f, 0.17f,
             0.25f, -0.625f,  -0.2f, 0.17f, 0.17f, 0.17f,
             0.15f, -0.625f,   0.1f, 0.17f, 0.17f, 0.17f,

             0.012f, -0.238f, 0.00001f,  1.0f, 1.0f, 1.0f,   // logo
             0.012f, -0.262f, 0.00001f,  1.0f, 1.0f, 1.0f,
            -0.012f, -0.238f, 0.00001f,  1.0f, 1.0f, 1.0f,
            -0.012f, -0.262f, 0.00001f,  1.0f, 1.0f, 1.0f
        };

        public new uint[] indices;

        public pantalla(float x = 0.0f, float y = 0.0f, float z = 0.0f, float yaw = 0.0f)
		{
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
            rotate(time);

            model = Matrix4.CreateTranslation(new Vector3(pos.X, pos.Y, pos.Z));
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
