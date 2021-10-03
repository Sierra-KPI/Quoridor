using Quoridor.View;
using Quoridor.Model;

namespace Quoridor.Console.App
{
    internal class Program
    {
        private static void Main()
        {
            Player player1 = new();

            Board board1 = new();
            // Will be assigned in the future
            IElement[,] _board = new IElement[3, 3]
            {
                {
                    new Cell(new Coordinates(0, 0), 1),
                    new Wall(new Coordinates(0, 0),
                        new Coordinates(0, 1), Orientation.Vertical),
                    new Cell(new Coordinates(0, 2), 1)
                },
                {
                    new Wall(new Coordinates(1, 0),
                        new Coordinates(1, 1), Orientation.Horizontal)
                        {HasWall = true},
                    new Wall(new Coordinates(1, 1),
                        new Coordinates(1, 2), Orientation.Horizontal)
                        {HasWall = true},
                    new Wall(new Coordinates(1, 2),
                        new Coordinates(1, 3), Orientation.Horizontal)
                        {HasWall = true}
                },
                {
                    new Cell(new Coordinates(2, 0), 1),
                    new Wall(new Coordinates(2, 0),
                        new Coordinates(2, 1), Orientation.Vertical),
                    new Cell(new Coordinates(2, 2), 1)
                }
            };

            QuoridorGame game = new QuoridorGame(,, _board);
            ViewOutput viewOutput = new();
            viewOutput.DrawBoard();
        }
    }
}
