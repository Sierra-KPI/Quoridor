﻿using System;
using Quoridor.Model;

namespace Quoridor.View
{
    public class ViewOutput
    {
        #region Fields

        private readonly QuoridorGame _currentGame;

        private const string FirstPlayerSymbol = " 1 ";
        private const string SecondPlayerSymbol = " 2 ";
        private const string EmptyCellSymbol = "   ";
        private const string HorizontalWallSymbol = "───";
        private const string VerticalWallSymbol = "│";
        private const string HorizontalPlacedWallSymbol = "■■■";
        private const string VerticalPlacedWallSymbol = "█";

        private readonly int _size;
        private string[,] _board;

        #endregion Fields

        #region Constructor

        public ViewOutput(QuoridorGame game)
        {
            _currentGame = game;
            _size = game.CurrentBoard.Size;
            CreateBoard();
        }

        #endregion Constructor

        #region Methods

        private void CreateBoard()
        {
            _board = new string[_size * 2 + 1, _size * 2 + 1];
            CleanCells();

            for (var i = 0; i < _size * 2 + 1; i += 2)
            {
                for (var j = 0; j < _size * 2 + 1; j++)
                {
                    _board[i, j] = HorizontalWallSymbol;
                }
            }

            for (var i = 0; i < _size * 2 + 1; i++)
            {
                for (var j = 0; j < _size * 2 + 1; j += 2)
                {
                    _board[i, j] = VerticalWallSymbol;
                }
            }
        }

        private void UpdateBoard()
        {
            UpdateCells();
            UpdateWalls();
        }

        private void CleanCells()
        {
            for (var i = 1; i < _size * 2 + 1; i += 2)
            {
                for (var j = 1; j < _size * 2 + 1; j += 2)
                {
                    _board[i, j] = EmptyCellSymbol;
                }
            }
        }

        private void UpdateCells()
        {
            CleanCells();
            int x = _currentGame.FirstPlayer.CurrentCell.Coordinates.X * 2 + 1;
            int y = _currentGame.FirstPlayer.CurrentCell.Coordinates.Y * 2 + 1;
            _board[x, y] = FirstPlayerSymbol;

            x = _currentGame.SecondPlayer.CurrentCell.Coordinates.X * 2 + 1;
            y = _currentGame.SecondPlayer.CurrentCell.Coordinates.Y * 2 + 1;
            _board[x, y] = SecondPlayerSymbol;
        }

        private void UpdateWalls()
        {
            var walls = _currentGame.CurrentBoard.GetPlacedWalls();
            for (var i = 0; i < walls.GetLength(0); i++)
            {
                var x1 = walls[i].Coordinates.X * 2 + 1;
                var y1 = walls[i].Coordinates.Y * 2 + 1;
                var x2 = walls[i].EndCoordinates.X * 2;
                var y2 = walls[i].EndCoordinates.Y * 2;

                if (walls[i].Orientation == Orientation.Vertical)
                {
                    _board[x1, y2] = VerticalPlacedWallSymbol;
                    _board[x1 + 1, y2] = VerticalPlacedWallSymbol;
                    _board[x1 + 2, y2] = VerticalPlacedWallSymbol;
                }
                if (walls[i].Orientation == Orientation.Horizontal)
                {
                    _board[x2, y1] = HorizontalPlacedWallSymbol;
                    _board[x2, y1 + 2] = HorizontalPlacedWallSymbol;
                }
            }
        }

        public void DrawBoard()
        {
            UpdateBoard();

            Console.Write("  ");
            for (var i = 0; i < _size; i++)
            {
                Console.Write("   " + (char)(i + 65));
            }

            Console.WriteLine();

            for (var i = 0; i < _board.GetLength(0); i++)
            {
                if (i % 2 == 1)
                {
                    Console.Write(" " + ((i / 2) + 1) + " ");
                }
                else
                {
                    Console.Write("   ");
                }

                for (var j = 0; j < _board.GetLength(0); j++)
                {
                    Console.Write(_board[i, j]);
                }

                Console.WriteLine();
            }
        }

        #endregion Methods
    }
}
