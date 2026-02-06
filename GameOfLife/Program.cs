namespace GameOfLife
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var view = new ConsoleGameView();
            var game = new GameController(view);
            
            Console.WriteLine("Введите размер поля");
            int size = int.Parse(Console.ReadLine());
            Console.Clear();
            
            game.InitializeGame(size);
            game.StartGame();
        }
    }
}