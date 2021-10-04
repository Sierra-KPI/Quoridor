using Quoridor.View;
using Quoridor.Model;
using System;
using Quoridor.OutputConsole.Input;

namespace Quoridor.OutputConsole.App
{
    internal class Program
    {
        private static void Main()
        {
            Board board1 = new BoardFactory().CreateBoard();
            Player player1 = new(board1.GetStartCellForPlayer(1),
                board1.GetEndCellsForPlayer(board1.GetStartCellForPlayer(1)));
            Player player2 = new(board1.GetStartCellForPlayer(2),
                board1.GetEndCellsForPlayer(board1.GetStartCellForPlayer(2)));
            QuoridorGame game = new(player1, player2, board1);

            ConsoleInput input = new(game);

            input.ReadMove();

            //Player player1 = new();


            //IElement[,] _board = new IElement[3, 3]
            //{
            //    {
            //        new Cell(new Coordinates(0, 0), 1),
            //        new Wall(new Coordinates(0, 0),
            //            new Coordinates(0, 1), Orientation.Vertical),
            //        new Cell(new Coordinates(0, 2), 1)
            //    },
            //    {
            //        new Wall(new Coordinates(1, 0),
            //            new Coordinates(1, 1), Orientation.Horizontal)
            //            {HasWall = true},
            //        new Wall(new Coordinates(1, 1),
            //            new Coordinates(1, 2), Orientation.Horizontal)
            //            {HasWall = true},
            //        new Wall(new Coordinates(1, 2),
            //            new Coordinates(1, 3), Orientation.Horizontal)
            //            {HasWall = true}
            //    },
            //    {
            //        new Cell(new Coordinates(2, 0), 1),
            //        new Wall(new Coordinates(2, 0),
            //            new Coordinates(2, 1), Orientation.Vertical),
            //        new Cell(new Coordinates(2, 2), 1)
            //    }
            //};

            //QuoridorGame game = new(,, _board);
            //ViewOutput viewOutput = new();
            //viewOutput.DrawBoard();
        }
    }
}
