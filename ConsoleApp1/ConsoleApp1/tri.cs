using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using System.Diagnostics;
using System.IO;
using System.Drawing;

namespace ConsoleApp1 { 
	public class tri
    {
        private float v1_x, v1_y, v1_z, c1_r, c1_g, c1_b, v2_x, v2_y, v2_z, c2_r, c2_g, c2_b, v3_x, v3_y, v3_z, c3_r, c3_g, c3_b;

        public int VertexBufferObject;
        public int ElementBufferObject;
        public int VertexArrayObject;

        private float[] vertices;

        private uint[] indices = {0, 1, 2};

        public tri(float v1_x, float v1_y, float v1_z, float c1_r, float c1_g, float c1_b,
                   float v2_x, float v2_y, float v2_z, float c2_r, float c2_g, float c2_b,
                   float v3_x, float v3_y, float v3_z, float c3_r, float c3_g, float c3_b)
        {
            this.v1_x = v1_x; this.v1_y = v1_y; this.v1_z = v1_z; this.c1_r = c1_r; this.c1_g = c1_g; this.c1_b = c1_b;
            this.v2_x = v2_x; this.v2_y = v2_y; this.v2_z = v2_z; this.c2_r = c2_r; this.c2_g = c2_g; this.c2_b = c2_b;
            this.v3_x = v3_x; this.v3_y = v3_y; this.v3_z = v3_z; this.c3_r = c3_r; this.c3_g = c3_g; this.c3_b = c3_b;

                
            vertices = new float[18];

            vertices[0]  = v1_x; vertices[1]  = v1_y; vertices[2]  = v1_z; vertices[3]  = c1_r; vertices[4]  = c1_g; vertices[5]  = c1_b;
            vertices[6]  = v2_x; vertices[7]  = v2_y; vertices[8]  = v2_z; vertices[9]  = c2_r; vertices[10] = c2_g; vertices[11] = c2_b;
            vertices[12] = v3_x; vertices[13] = v3_y; vertices[14] = v3_z; vertices[15] = c3_r; vertices[16] = c3_g; vertices[17] = c3_b;

            Console.WriteLine("");

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
        }

        public void draw(Shader shader, Matrix4 model, Matrix4 view, Matrix4 projection, double time)
        {
            GL.BindVertexArray(VertexArrayObject);
            shader.Use();
            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", view);
            shader.SetMatrix4("projection", projection);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            }
    }
}
