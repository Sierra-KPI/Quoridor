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
            CleanWalls();
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

        private void CleanWalls()
        {
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
            CleanWalls();
            var walls = _currentGame.CurrentBoard.GetPlacedWalls();
            for (var i = 0; i < walls.GetLength(0); i++)
            {
                int x1 = walls[i].Coordinates.X * 2 + 1;
                int y1 = walls[i].Coordinates.Y * 2 + 1;
                int x2 = walls[i].EndCoordinates.X * 2;
                int y2 = walls[i].EndCoordinates.Y * 2;

                if (walls[i].Coordinates.X == walls[i].EndCoordinates.X)
                {
                    _viewBoard[x1, y2] = VerticalPlacedWallSymbol;
                    _viewBoard[x1 + 1, y2] = VerticalPlacedWallSymbol;
                    _viewBoard[x1 + 2, y2] = VerticalPlacedWallSymbol;
                }
                else
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

                if (i % 2 == 0 && i != 0 && i != _viewBoardSize - 1)
                {
                    Console.Write(" " + (i / 2) + " ");
                }
                else
                {
                    Console.Write("   ");
                }
                Console.WriteLine();
            }

            Console.Write("    ");
            for (var i = 0; i < _currentGame.CurrentBoard.Size - 1; i++)
            {
                Console.Write("   " + (char)(i + 83));
            }
            Console.WriteLine();
        }

        #endregion Methods


        private const string GreetingMessage = "Hi! Now You are " +
            "playing Quoridor.\nThe object of the game is to advance " +
            "your pawn to the opposite edge of the board.\nOn your " +
            "turn you may either move your pawn or place a wall. " +
            "\nYou may hinder your opponent with wall placement, " +
            "but not completely block them off.\nMeanwhile, " +
            "they are trying to do the same to you.\nThe first " +
            "pawn to reach the opposite side wins.";
        private const string NullOrEmptyMessage = "Your input " +
            "is empty! Try again";
        private const string HelpMessage = "Here's some tips " +
            "tips on how to play the game:\n1. start x - start new " +
            "game, where is the number of real players " +
            "(1 for one real and one bot. 2 for two real players)" +
            "\n2. move xy - move Your player to cell xy, for " +
            "example: move E8\n3. wall xyh - place wall in xy, h - horisontal," +
            "v - vertical, for example: wall V7h\n4. help - print this " +
            "helpbox\n5. quit - quit the game";
        private const string IncorrectMessage = "Incorrect command! " +
            "Try something else";
        private const string ChooseColorMessage =
            "Please, chose which player you want to play (black or white)";
        private const string CongratulationsMessage = " Has won!";
        private const string CurrentPlayerMessage = "Current player is ";
        private const string DelimiterMessage = "-----------------" +
            "----------------------------";
        private const string SingleplayerMessage = "New Singleplayer" +
            " Game has started!";
        private const string MultiplayerMessage = "New Multiplayer" +
            " Game has started!";
        private const string FirstPlayerName = "White Player";
        private const string SecondPlayerName = "Black Player";
        private string _currentPlayerName = FirstPlayerName;


        public static void WriteStartingMessage()
        {
            Console.WriteLine(GreetingMessage);
            Console.WriteLine(HelpMessage);
        }

        public static void WriteIncorrectMessage() =>
            Console.WriteLine(IncorrectMessage);

        public void WriteNullOrEmptyMessage() =>
            Console.WriteLine(NullOrEmptyMessage);

        public static void WriteChooseColorMessage() =>
            Console.WriteLine(ChooseColorMessage);

        public static void WriteSingleplayerMessage() =>
            Console.WriteLine(SingleplayerMessage);

        public static void WriteMultiplayerMessage() =>
            Console.WriteLine(MultiplayerMessage);

        public void WriteHelpMessage() =>
            Console.WriteLine(HelpMessage);

        public void WriteDelimiter() =>
            Console.WriteLine(DelimiterMessage);

        public void WriteNewGame() =>
            Console.WriteLine("To start new game, use command start with parameter");

        public void WritePlayerMessage()
        {
            if (_currentGame.CurrentPlayer == _currentGame.FirstPlayer)
            {
                _currentPlayerName = FirstPlayerName;
            }
            else
            {
                _currentPlayerName = SecondPlayerName;
            }

            Console.WriteLine(CurrentPlayerMessage +
                _currentPlayerName);
        }

        public void WriteCongratulations(IPlayer player)
        {
            var winner = "";
            if (player == _currentGame.FirstPlayer)
            {
                winner = FirstPlayerName;
            }
            else
            {
                winner = SecondPlayerName;
            }

            Console.WriteLine(winner + CongratulationsMessage);
        }

        public void WriteBotMove(string command, Cell element)
        {
            string formattedCoordinates =
                       $"{(char)(element.Coordinates.Y + 65)}" +
                       $"{element.Coordinates.X + 1}";
            Console.WriteLine(command + " " + formattedCoordinates);
        }

        public void WriteBotPlaceWall(string command, Wall element)
        {
            string formattedCoordinates =
                        $"{(char)(element.Coordinates.Y + 83)}" +
                        $"{element.Coordinates.X + 1}" +
                        $"{(element.Coordinates.X == element.EndCoordinates.X ? 'v' : 'h')}";
            Console.WriteLine(command + " " + formattedCoordinates);
        }

    }
}
