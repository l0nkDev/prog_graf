using OpenTK.Mathematics;

namespace JuegoProgramacionGrafica
{
    public class GUI
    {

        public Face button = new Face();

        public GUI()
        {
        }

        public void Draw(Shader shader, double time)
        {
            button.Draw(shader, Matrix4.Identity, Matrix4.Identity, Matrix4.Identity, time);
        }
    }
}
