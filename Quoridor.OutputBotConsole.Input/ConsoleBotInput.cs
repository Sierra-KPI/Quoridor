using System;
using System.Collections.Generic;
using Quoridor.Model;
using Quoridor.View;

namespace Quoridor.OutputConsole.Input
{
    public class ConsoleBotInput
    {
        #region Fields

        public QuoridorGame CurrentGame;
        public ViewOutput View;

        private Dictionary<char, int> _chars = new();

        private const string NullOrEmptyMessage = "Your input " +
            "is empty! Try again";
        private const string IncorrectMessage = "Incorrect command! " +
            "Try something else";

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
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine(NullOrEmptyMessage);
                }
                else
                {
                    string[] inputString = input.Split(Array.Empty<char>());
                    TryToExecuteCommand(inputString);
                }
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
                Console.WriteLine(e);
                WriteIncorrectMessage();
            }
        }

        private void ExecuteCommand(string[] inputString)
        {
            switch (inputString[0])
            {
                case "move":
                case "jump":
                    ChangePlayerPosition(inputString);
                    break;
                case "wall":
                    PlaceWall(inputString);
                    break;
                default:
                    WriteIncorrectMessage();
                    break;
            }
        }

        private void StartGame()
        {
            Board board = new BoardFactory().CreateBoard();

            (Cell firstPlayerCell, Cell[] firstPlayerEndCells) =
                GetPlayerCells(board, PlayerID.First);
            Player firstPlayer = new(firstPlayerCell, firstPlayerEndCells);
            _currentPlayer = firstPlayer;

            CurrentGame = CreateGame(firstPlayer, board);

            View = new(CurrentGame);

            if (CurrentGame.FirstPlayer is Bot bot)
            {
                Cell tempCoords = firstPlayer.CurrentCell;
                firstPlayer.CurrentCell = bot.CurrentCell;
                bot.CurrentCell = tempCoords;

                Cell[] tempCells = firstPlayer.EndCells;
                firstPlayer.EndCells = bot.EndCells;
                bot.EndCells = tempCells;

                StartBotTurn();
            }
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

            string choosenColor = Console.ReadLine();

            MinimaxBot botPlayer = new(secondPlayerCell, secondPlayerEndCells);
            IPlayer firstGamePlayer;
            IPlayer secondGamePlayer;
            if (choosenColor == "white")
            {
                firstGamePlayer = firstPlayer;
                secondGamePlayer = botPlayer;
            }
            else if (choosenColor == "black")
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

        private void ChangePlayerPosition(string[] values)
        {
            Coordinates coordinates = new(values[1][1] - '1',
                _chars[values[1][0]] - 1);
            Cell to = CurrentGame.CurrentBoard.
                GetCellByCoordinates(coordinates);
            _currentPlayer = CurrentGame.CurrentPlayer;

            CurrentGame.MakeMove(to);

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
                StartGame();

                return true;
            }

            return false;
        }

        private void StartBotTurn()
        {
            IElement element = (CurrentGame.BotPlayer as Bot).
                DoMove(CurrentGame, out Coordinates coordinates);
            if (element is Cell cell)
            {
                CurrentGame.MakeMove(cell);
                CheckWinner(CurrentGame.SecondPlayer);
                string formattedCoordinates =
                    $"{TransformCoordinate(coordinates.Y + 1)}" +
                    $"{coordinates.X + 1}";
                Console.WriteLine("move " + formattedCoordinates);
            }
            else
            {
                CurrentGame.PlaceWall((Wall)element);
            }
        }

        private static void WriteIncorrectMessage() =>
            Console.WriteLine(IncorrectMessage);

        #endregion Methods
    }
}
