using JuegoProgramacionGrafica;
using System.Windows.Forms;

internal class Program
{
    private static void Main(string[] args)
    {
        using (Game game = new Game(800, 600, "Juego Programación Gráfica"))
        {
            game.Run();
        }
    }
}