using System;
using System.Text;
using Quoridor.Model;

namespace Quoridor.View
{
    public class ViewOutput
    {
        private const string CellSymbol = " C ";
        private const string PlayerSymbol = " P ";
        private const string HorizontalWallSymbol = "───";
        private const string VerticalWallSymbol = " │ ";
        private const string HorizontalPlacedWallSymbol = "═══";
        private const string VerticalPlacedWallSymbol = " ║ ";

        StringBuilder outputString = new("");


        private readonly IElement[,] _board = new IElement[3, 3]
        {
            {
                new Cell(new Coordinates(0, 0), 1) {HasPlayer = true},
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
                new Cell(new Coordinates(2, 2), 1) {HasPlayer = true}
            }
        };

        public void DrawBoard()
        {
            for (var i = 0; i < _board.GetLength(0); i++)
            {
                for (var j = 0; j < _board.GetLength(0); j++)
                {
                    if (_board[i, j] is Cell cell)
                    {
                        outputString.Append(DrawCell(cell));
                    }
                    else
                    {
                        Wall wall = (Wall)_board[i, j];
                        outputString.Append(DrawWall(wall));
                    }
                }
                outputString.Append('\n');
            }
            Console.WriteLine(outputString);
        }

        public string DrawCell(Cell cell)
        {
            if (cell.HasPlayer)
            {
                return CellSymbol;
            }
            return PlayerSymbol;
        }

        public string DrawWall(Wall wall)
        {
            if (wall.HasWall)
            {
                if (wall.Orientation == Orientation.Vertical)
                {
                    return VerticalPlacedWallSymbol;
                }
                return HorizontalPlacedWallSymbol;
            }
            else
            {
                if (wall.Orientation == Orientation.Vertical)
                {
                    return VerticalWallSymbol;
                }
                return HorizontalWallSymbol;
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
