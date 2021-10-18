using System;
using Quoridor.Model;

namespace Quoridor.View
{
    public class ViewOutput
    {
        #region Fields

        private readonly QuoridorGame _currentGame;

        private const string FirstPlayerSymbol = " W ";
        private const string SecondPlayerSymbol = " B ";
        private const string EmptyCellSymbol = "   ";
        private const string HorizontalWallSymbol = "───";
        private const string VerticalWallSymbol = "│";
        private const string HorizontalPlacedWallSymbol = "■■■";
        private const string VerticalPlacedWallSymbol = "█";

        private readonly int _viewBoardSize;
        private string[,] _viewBoard;

        #endregion Fields

        #region Constructor

        public ViewOutput(QuoridorGame game)
        {
            _currentGame = game;
            _viewBoardSize = game.CurrentBoard.Size * 2 + 1;
            CreateBoard();
        }

        #endregion Constructor

        #region Methods

        private void CreateBoard()
        {
            _viewBoard = new string[_viewBoardSize, _viewBoardSize];
            CleanCells();

            for (var i = 0; i < _viewBoardSize; i++)
            {
                for (var j = 0; j < _viewBoardSize; j++)
                {
                    if (i % 2 == 0)
                    {
                        _viewBoard[i, j] = HorizontalWallSymbol;
                    }
                    if (j % 2 == 0)
                    {
                        _viewBoard[i, j] = VerticalWallSymbol;
                    }
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
            for (var i = 1; i < _viewBoardSize; i += 2)
            {
                for (var j = 1; j < _viewBoardSize; j += 2)
                {
                    _viewBoard[i, j] = EmptyCellSymbol;
                }
            }
        }

        private void UpdateCells()
        {
            CleanCells();
            int x = _currentGame.FirstPlayer.CurrentCell.Coordinates.X * 2 + 1;
            int y = _currentGame.FirstPlayer.CurrentCell.Coordinates.Y * 2 + 1;
            _viewBoard[x, y] = FirstPlayerSymbol;

            x = _currentGame.SecondPlayer.CurrentCell.Coordinates.X * 2 + 1;
            y = _currentGame.SecondPlayer.CurrentCell.Coordinates.Y * 2 + 1;
            _viewBoard[x, y] = SecondPlayerSymbol;
        }

        private void UpdateWalls()
        {
            var walls = _currentGame.CurrentBoard.GetPlacedWalls();
            for (var i = 0; i < walls.GetLength(0); i++)
            {
                int x1 = walls[i].Coordinates.X * 2 + 1;
                int y1 = walls[i].Coordinates.Y * 2 + 1;
                int x2 = walls[i].EndCoordinates.X * 2;
                int y2 = walls[i].EndCoordinates.Y * 2;

                if (walls[i].Orientation == Orientation.Vertical)
                {
                    _viewBoard[x1, y2] = VerticalPlacedWallSymbol;
                    _viewBoard[x1 + 1, y2] = VerticalPlacedWallSymbol;
                    _viewBoard[x1 + 2, y2] = VerticalPlacedWallSymbol;
                }
                if (walls[i].Orientation == Orientation.Horizontal)
                {
                    _viewBoard[x2, y1] = HorizontalPlacedWallSymbol;
                    _viewBoard[x2, y1 + 2] = HorizontalPlacedWallSymbol;
                }
            }
        }

        public void DrawBoard()
        {
            UpdateBoard();

            Console.Write("  ");
            for (var i = 0; i < _currentGame.CurrentBoard.Size; i++)
            {
                Console.Write("   " + (char)(i + 65));
            }
            Console.WriteLine();

            for (var i = 0; i < _viewBoardSize; i++)
            {
                if (i % 2 == 1)
                {
                    Console.Write(" " + ((i / 2) + 1) + " ");
                }
                else
                {
                    Console.Write("   ");
                }

                for (var j = 0; j < _viewBoardSize; j++)
                {
                    Console.Write(_viewBoard[i, j]);
                }
                Console.WriteLine();
            }
        }

        #endregion Methods
    }
}
