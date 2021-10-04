using System;
using System.Collections.Generic;
using System.Text;
using Quoridor.Model;

namespace Quoridor.View
{
    public class ViewOutput
    {
        private readonly QuoridorGame _currentGame;

        private const string FirstPlayerSymbol = " 1 ";
        private const string SecondPlayerSymbol = " 2 ";
        private const string EmptyCellSymbol = "   ";
        private const string HorizontalWallSymbol = "───";
        private const string VerticalWallSymbol = " │ ";
        private const string HorizontalPlacedWallSymbol = "═══";
        private const string VerticalPlacedWallSymbol = " ║ ";

        private readonly StringBuilder _outputString = new("");
        private readonly Dictionary<Tuple<Orientation, bool>,
            string> _stringValues = new();



        public ViewOutput(QuoridorGame game)
        {
            _currentGame = game;
            InitializeStringDictionary();
        }

        private void InitializeStringDictionary()
        {
            _stringValues.Add((Orientation.Horizontal,
                false).ToTuple(), HorizontalWallSymbol);
            _stringValues.Add((Orientation.Horizontal,
                true).ToTuple(), HorizontalPlacedWallSymbol);
            _stringValues.Add((Orientation.Vertical,
                false).ToTuple(), VerticalWallSymbol);
            _stringValues.Add((Orientation.Vertical,
                true).ToTuple(), VerticalPlacedWallSymbol);
        }

        public void DrawBoard()
        {
            //for (var i = 0; i < _board.GetLength(0); i++)
            //{
            //    for (var j = 0; j < _board.GetLength(0); j++)
            //    {
            //        if (_board[i, j] is Cell cell)
            //        {
            //            _outputString.Append(DrawCell(cell));
            //        }
            //        else
            //        {
            //            Wall wall = (Wall)_board[i, j];
            //            _outputString.Append(DrawWall(wall));
            //        }
            //    }
            //    _outputString.Append('\n');
            //}
            //Console.WriteLine(_outputString);
        }

        public string DrawCell(Cell cell)
        {
            if (cell == _currentGame.FirstPlayer.CurrentCell)
            {
                return FirstPlayerSymbol;
            }
            else if (cell == _currentGame.SecondPlayer.CurrentCell)
            {
                return SecondPlayerSymbol;
            }
            return EmptyCellSymbol;
        }

        public string DrawWall(Wall wall)
        {
            (Orientation Orientation, bool HasWall) wallTuple
                = (wall.Orientation, wall.HasWall);
            return _stringValues[wallTuple.ToTuple()];
        }

        // Probably should be in an input
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
