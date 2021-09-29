using System;
using Quoridor.Model;

namespace Quoridor.View
{
    public class ViewOutput
    {
        private const string CellSymbol = " C ";
        private const string HorizontalWallSymbol = "───";
        private const string VerticalWallSymbol = " │ ";
        private const string HorizontalPlacedWallSymbol = "═══";
        private const string VerticalPlacedWallSymbol = " ║ ";

        private readonly IElement[,] _board = new IElement[3, 3]
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

        public void DrawBoard()
        {
            for (var i = 0; i < _board.GetLength(0); i++)
            {
                for (var j = 0; j < _board.GetLength(0); j++)
                {
                    if (_board[i,j] is Cell)
                    {
                        Console.Write(CellSymbol);
                    }
                    else
                    {
                        Wall wall = (Wall)_board[i, j];
                        if (wall.HasWall)
                        {
                            if (wall.Orientation == Orientation.Vertical)
                            {
                                Console.Write(VerticalPlacedWallSymbol);
                            }
                            else
                            {
                                Console.Write(HorizontalPlacedWallSymbol);
                            }
                        }
                        else
                        {
                            if (wall.Orientation == Orientation.Vertical)
                            {
                                Console.Write(VerticalWallSymbol);
                            }
                            else
                            {
                                Console.Write(HorizontalWallSymbol);
                            }
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
