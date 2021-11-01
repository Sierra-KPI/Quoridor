using System;
using System.Collections.Generic;
using Quoridor.Model;
using Quoridor.View;

namespace Quoridor.OutputConsole.Input
{
    public class ConsoleInput
    {
        #region Fields

        public QuoridorGame CurrentGame;
        public ViewOutput View;

        private Dictionary<char, int> _chars = new();
        private bool _endLoop;
        private string _currentPlayerName = FirstPlayerName;
        private IPlayer _currentPlayer;
        private string[] _gameModePreference;

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
        private const string SingleModeInput = "1";
        private const string MultiplayerModeInput = "2";
        private const string FirstPlayerName = "White Player";
        private const string SecondPlayerName = "Black Player";

        #endregion Fields

        #region Methods

        public void OnStart()
        {
            WriteStartingMessage();
            InitializeDictionary();
        }

        public void WriteStartingMessage()
        {
            Console.WriteLine(HelpMessage);
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

        private char TransformCoordinate(int horizntalCoordinate)
        {
            foreach (KeyValuePair<char, int> item in _chars)
            {
                if (item.Value == horizntalCoordinate)
                {
                    return item.Key;
                }
            }

            throw new FormatException("Wrong horizontal coordinate format");
        }

        public void ReadMove()
        {
            while (!_endLoop)
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
                case "start":
                    StartGame(inputString);
                    break;
                case "move":
                case "jump":
                    ChangePlayerPosition(inputString);
                    break;
                case "wall":
                    PlaceWall(inputString);
                    break;
                case "quit":
                    QuitLoop();
                    break;
                case "help":
                    WriteHelpMessage();
                    break;
                default:
                    WriteIncorrectMessage();
                    break;
            }

            StartNewTurn();
        }

        private void StartGame(string[] values)
        {
            _gameModePreference = values;
            Board board = new BoardFactory().CreateBoard();

            (Cell firstPlayerCell, Cell[] firstPlayerEndCells) =
                GetPlayerCells(board, PlayerID.First);
            Player firstPlayer = new(firstPlayerCell, firstPlayerEndCells);
            _currentPlayer = firstPlayer;

            CurrentGame = CreateGame(values, firstPlayer, board);

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

        private QuoridorGame CreateGame(string[] values,
            Player firstPlayer, Board board)
        {
            (Cell secondPlayerCell, Cell[] secondPlayerEndCells) =
                GetPlayerCells(board, PlayerID.Second);

            if (values[1] == SingleModeInput)
            {
                Console.WriteLine(ChooseColorMessage);
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

                Console.WriteLine(SingleplayerMessage);

                return CurrentGame = new QuoridorGame(firstGamePlayer,
                    secondGamePlayer, board);
            }
            else if (values[1] == MultiplayerModeInput)
            {
                Player realPlayer = new(secondPlayerCell, secondPlayerEndCells);
                Console.WriteLine(MultiplayerMessage);

                return CurrentGame = new QuoridorGame(firstPlayer,
                    realPlayer, board);
            }

            throw new Exception("Wrong number of players");
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

        private void StartNewTurn()
        {
            WriteDelimiter();

            View.DrawBoard();

            WritePlayerMessage();
        }

        private bool CheckWinner(IPlayer player)
        {
            if (player.HasWon())
            {
                WriteCongratulations(player);
                WriteDelimiter();

                View.DrawBoard();
                StartGame(_gameModePreference);

                return true;
            }

            return false;
        }

        private void StartBotTurn()
        {
            Coordinates coordinates;
            IElement element = (CurrentGame.BotPlayer as Bot).DoMove(CurrentGame, out coordinates);
            if (element is Cell cell)
            {
                CurrentGame.MakeMove(cell);
                CheckWinner(CurrentGame.SecondPlayer);
                string formattedCoordinates = $"{TransformCoordinate(coordinates.Y + 1)}{coordinates.X + 1}";
                Console.WriteLine("move " + formattedCoordinates);
            }
            else
            {
                CurrentGame.PlaceWall((Wall)element);
            }
        }

        private static void WriteIncorrectMessage() =>
            Console.WriteLine(IncorrectMessage);

        private static void WriteHelpMessage() =>
            Console.WriteLine(HelpMessage);

        private static void WriteDelimiter() =>
            Console.WriteLine(DelimiterMessage);

        private void WritePlayerMessage()
        {
            if (CurrentGame.CurrentPlayer == CurrentGame.FirstPlayer)
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

        private void WriteCongratulations(IPlayer player)
        {
            var winner = "";
            if (player == CurrentGame.FirstPlayer)
            {
                winner = FirstPlayerName;
            }
            else
            {
                winner = SecondPlayerName;
            }

            Console.WriteLine(winner + CongratulationsMessage);
        }

        private void QuitLoop() => _endLoop = true;

        #endregion Methods
    }
}
