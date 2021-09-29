using System;
using Quoridor.Model;

namespace Quoridor.View
{
    public class ViewOutput
    {
        IElement[,] board = new IElement[3, 3]
        {
            { new Cell(new Coordinates(0, 0), 1), new Wall(new Coordinates(0, 0), new Coordinates(0, 1), Orientation.Vertical), new Cell(new Coordinates(0, 2), 1) },
            { new Wall(new Coordinates(1, 0), new Coordinates(1, 1), Orientation.Horizontal), new Wall(new Coordinates(1, 1), new Coordinates(1, 2), Orientation.Horizontal), new Wall(new Coordinates(1, 2), new Coordinates(1, 3), Orientation.Horizontal) },
            { new Cell(new Coordinates(2, 0), 1), new Wall(new Coordinates(2, 0), new Coordinates(2, 1), Orientation.Vertical), new Cell(new Coordinates(2, 2), 1)  }
        };

        public void DrawBoard()
        {
            for (var i = 0; i < board.GetLength(0); i++)
            {
                for (var j = 0; j < board.GetLength(0); j++)
                {
                    if (board[i,j] is Cell)
                    {
                        Console.Write(" C ");
                    }
                    else
                    {
                        Wall wall = board[i, j] as Wall;
                        if (wall.Orientation == Orientation.Vertical)
                        {
                            Console.Write(" │ ");
                        }
                        else
                        {
                            Console.Write("───");
                        }
                    }
                }
                    Console.WriteLine();
            }
        }

        public static string ReadMove()
        {
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                return "No input. Try again";
            }
            return input;
        }
    }
}
