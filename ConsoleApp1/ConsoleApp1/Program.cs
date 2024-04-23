using ConsoleApp1;

internal class Program
{
    private static void Main(string[] args)
    {
        using (Game game = new Game(800, 600, "LearnOpenTK"))
        {
            game.Run();
        }
    }
}