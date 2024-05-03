using System.Runtime.Serialization;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace JuegoProgramacionGrafica
{
    public class Tri
    {
        private int VertexBufferObject;
        private int ElementBufferObject;
        private int VertexArrayObject;
        public float[] Vertices;

        private uint[] Indices = { 0, 1, 2 };

        public Tri(float v1_x, float v1_y, float v1_z, float c1_r, float c1_g, float c1_b,
                   float v2_x, float v2_y, float v2_z, float c2_r, float c2_g, float c2_b,
                   float v3_x, float v3_y, float v3_z, float c3_r, float c3_g, float c3_b)
        {
            Vertices = new float[18];
            Vertices[0]  = v1_x; Vertices[1]  = v1_y; Vertices[2]  = v1_z; Vertices[3]  = c1_r; Vertices[4]  = c1_g; Vertices[5]  = c1_b;
            Vertices[6]  = v2_x; Vertices[7]  = v2_y; Vertices[8]  = v2_z; Vertices[9]  = c2_r; Vertices[10] = c2_g; Vertices[11] = c2_b;
            Vertices[12] = v3_x; Vertices[13] = v3_y; Vertices[14] = v3_z; Vertices[15] = c3_r; Vertices[16] = c3_g; Vertices[17] = c3_b;

            StreamingContext tmp = new();
            InitGL(tmp);
        }

        public Tri()
        {
        }

        [OnDeserialized]
        public void InitGL(StreamingContext context)
        {
            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * sizeof(float), Vertices, BufferUsageHint.StaticDraw);

            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Indices.Length * sizeof(uint), Indices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);
        }

        public void Draw(Shader shader, Matrix4 model, Matrix4 view, Matrix4 projection, double time)
        {
            GL.BindVertexArray(VertexArrayObject);
            shader.Use();
            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", view);
            shader.SetMatrix4("projection", projection);
            GL.DrawElements(PrimitiveType.Triangles, Indices.Length, DrawElementsType.UnsignedInt, 0);
            }
    }
}
