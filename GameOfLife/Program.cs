namespace GameOfLife
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            var map = new Field();
            map.CreateMap();
            var game = new GameController();
            game.StartGame();
        
        }
    }
}