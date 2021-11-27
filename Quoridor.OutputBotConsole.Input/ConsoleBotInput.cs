using System;
using System.Collections.Generic;
using Quoridor.Model;
using Quoridor.View;

namespace Quoridor.OutputBotConsole.Input
{
    public class ConsoleBotInput
    {
        #region Fields

        public QuoridorGame CurrentGame;
        public BotView View;

        private Dictionary<char, int> _chars = new();

        #endregion Fields

        #region Methods

        public void OnStart()
        {
            InitializeDictionary();
            StartGame();
        }

        private void InitializeDictionary() =>
            _chars = new Dictionary<char, int>
            {
                { 'A', 1 },
                { 'B', 2 },
                { 'C', 3 },
                { 'D', 4 },
                { 'E', 5 },
                { 'F', 6 },
                { 'G', 7 },
                { 'H', 8 },
                { 'I', 9 }
            };

        private char TransformCoordinate(int horizontalCoordinate)
        {
            foreach (KeyValuePair<char, int> item in _chars)
            {
                if (item.Value == horizontalCoordinate)
                {
                    return item.Key;
                }
            }

            throw new FormatException("Wrong horizontal coordinate format");
        }

        public void ReadMove()
        {
            while (true)
            {
                string input = View.GetCommand();

                string[] inputString = input.Split(Array.Empty<char>());
                TryToExecuteCommand(inputString);
            }
        }

        private void TryToExecuteCommand(string[] inputString)
        {
            try
            {
                ExecuteCommand(inputString);
            }
            catch (Exception e)
            {
                View.PrintCommand(e.ToString());
            }
        }

        private void ExecuteCommand(string[] inputString)
        {
            switch (inputString[0])
            {
                case "move":
                    ChangePlayerPosition(inputString);
                    break;
                case "jump":
                    ChangePlayerPosition(inputString, true);
                    break;
                case "wall":
                    PlaceWall(inputString);
                    break;
                default:
                    break;
            }
        }

        private void StartGame()
        {
            View = new();

            Board board = new BoardFactory().CreateBoard();

            (Cell firstPlayerCell, Cell[] firstPlayerEndCells) =
                GetPlayerCells(board, PlayerID.First);
            Player firstPlayer = new(firstPlayerCell, firstPlayerEndCells);

            CurrentGame = CreateGame(firstPlayer, board);
            StartBotTurn();
        }

        private static (Cell playerCell, Cell[] playerEndCells)
            GetPlayerCells(Board board, PlayerID playerID)
        {
            Cell playerCell = board.GetStartCellForPlayer
                ((int)playerID);
            Cell[] playerEndCells = board.GetEndCellsForPlayer
                (board.GetStartCellForPlayer((int)playerID));

            return (playerCell, playerEndCells);
        }

        private QuoridorGame CreateGame(Player firstPlayer, Board board)
        {
            (Cell secondPlayerCell, Cell[] secondPlayerEndCells) =
                GetPlayerCells(board, PlayerID.Second);

            string choosenColor = View.GetCommand();

            MinimaxBot botPlayer = new(secondPlayerCell, secondPlayerEndCells);
            IPlayer firstGamePlayer;
            IPlayer secondGamePlayer;
            if (choosenColor == "black")
            {
                firstGamePlayer = firstPlayer;
                secondGamePlayer = botPlayer;
            }
            else if (choosenColor == "white")
            {
                firstGamePlayer = botPlayer;
                secondGamePlayer = firstPlayer;
            }
            else
            {
                throw new Exception("Wrong color is chosen");
            }

            return CurrentGame = new QuoridorGame(firstGamePlayer,
                    secondGamePlayer, board);
        }

        private void ChangePlayerPosition(string[] values, bool isJump = false)
        {
            Coordinates coordinates = new(values[1][1] - '1',
                _chars[values[1][0]] - 1);
            Cell to = CurrentGame.CurrentBoard.
                GetCellByCoordinates(coordinates);

            CurrentGame.MakeMove(to, isJump);

            if (!CheckWinner(CurrentGame.FirstPlayer))
            {
                StartBotTurn();
            }
        }

        private void PlaceWall(string[] values)
        {
            int letter = values[1][0] - 83;
            int number = values[1][1] - '1';
            char orientation = values[1][2];

            Coordinates firstCoordinates = new Coordinates(number, letter);
            Coordinates secondCoordinates = orientation switch
            {
                'h' => new Coordinates(number + 1, letter),
                'v' => new Coordinates(number, letter + 1),
                _ => throw new FormatException("Wrong orientation input")
            };

            Wall wall = CurrentGame.CurrentBoard.
                GetWallByCoordinates(firstCoordinates, secondCoordinates);
            CurrentGame.PlaceWall(wall);

            StartBotTurn();
        }

        private bool CheckWinner(IPlayer player)
        {
            if (player.HasWon())
            {
                return true;
            }

            return false;
        }

        private void StartBotTurn()
        {
            (string command, IElement element) = CurrentGame.CurrentPlayer.DoMove(CurrentGame);
            var formattedCoordinates = "";
            switch (command)
            {
                case "move":
                    CurrentGame.MakeMove((Cell)element);
                    formattedCoordinates =
                        $"{TransformCoordinate(element.Coordinates.Y + 1)}" +
                        $"{element.Coordinates.X + 1}";
                    break;
                case "jump":
                    CurrentGame.MakeMove((Cell)element, true);
                    formattedCoordinates =
                        $"{TransformCoordinate(element.Coordinates.Y + 1)}" +
                        $"{element.Coordinates.X + 1}";
                    break;
                case "wall":
                    var wall = (Wall)element;
                    CurrentGame.PlaceWall(wall);
                    formattedCoordinates =
                        $"{(char)(wall.Coordinates.Y + 83)}" +
                        $"{wall.Coordinates.X + 1}" +
                        $"{(wall.Coordinates.X == wall.EndCoordinates.X ? 'v' : 'h')}";
                    break;
                default:
                    return;
            }

            CheckWinner(CurrentGame.SecondPlayer);
            var textToPrint = command + " " + formattedCoordinates;
            View.PrintCommand(textToPrint);
        }

        #endregion Methods
    }
}
