using System;
using Quoridor.Model;
using Quoridor.OutputConsole.Input;
using Quoridor.View;

namespace Quoridor.OutputConsole.App
{
    class GameLoop
    {

        public QuoridorGame CurrentGame;
        public ViewOutput View;
        public ConsoleInput Input;

        private string[] _gameModePreference;
        private const string SingleModeInput = "1";
        private const string MultiplayerModeInput = "2";

        private bool _endLoop;

        public GameLoop()
        {
            ViewOutput.WriteStartingMessage();
            Input = new ConsoleInput();
            StartGameLoop();
        }

        private void StartGameLoop()
        {
            while (!_endLoop)
            {
                string[] command = Input.ReadMove();
                TryToExecuteCommand(command);
            }
        }

        private void QuitLoop() => _endLoop = true;

        private void TryToExecuteCommand(string[] inputString)
        {
            try
            {
                ExecuteCommand(inputString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ViewOutput.WriteIncorrectMessage();
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
                    ChangePlayerPosition(inputString);
                    break;
                case "jump":
                    ChangePlayerPosition(inputString, true);
                    break;
                case "wall":
                    PlaceWall(inputString);
                    break;
                case "quit":
                    QuitLoop();
                    break;
                case "help":
                    View.WriteHelpMessage();
                    break;
                default:
                    ViewOutput.WriteIncorrectMessage();
                    break;
            }
            StartNewTurn();
        }


        private void StartGame(string[] values)
        {
            _gameModePreference = values;
            Board board = new BoardFactory().CreateBoard();

            (Cell firstPlayerCell, Cell[] firstPlayerEndCells) =
                board.GetPlayerCells(PlayerID.First);
            Player firstPlayer = new(firstPlayerCell, firstPlayerEndCells);

            if (values[1] == SingleModeInput)
            {
                CurrentGame = CreateSingleModeGame(firstPlayer, board);
            }
            else if (values[1] == MultiplayerModeInput)
            {
                CurrentGame = CreateMultiplayerModeGame(firstPlayer, board);
            }
            else throw new Exception("Wrong number of players");

            View = new(CurrentGame);
            StartBotTurn();
        }


        private QuoridorGame CreateSingleModeGame(
            Player firstPlayer, Board board)
        {
            ViewOutput.WriteChooseColorMessage();
            string choosenColor = Input.ReadMove()[0];

            (Cell secondPlayerCell, Cell[] secondPlayerEndCells) =
                board.GetPlayerCells(PlayerID.Second);
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
            ViewOutput.WriteSingleplayerMessage();

            return CurrentGame = new QuoridorGame(firstGamePlayer,
                secondGamePlayer, board);
        }

        private QuoridorGame CreateMultiplayerModeGame(
            Player firstPlayer, Board board)
        {
            (Cell secondPlayerCell, Cell[] secondPlayerEndCells) =
                board.GetPlayerCells(PlayerID.Second);
            Player realPlayer = new(secondPlayerCell, secondPlayerEndCells);
            ViewOutput.WriteMultiplayerMessage();

            return CurrentGame = new QuoridorGame(firstPlayer,
                realPlayer, board);
        }

        private void ChangePlayerPosition(string[] values, bool isJump = false)
        {
            Coordinates coordinates = new(values[1][1] - '1',
                values[1][0] - 65);
            Cell to = CurrentGame.CurrentBoard.
                GetCellByCoordinates(coordinates);

            CurrentGame.MakeMove(to, isJump);
            if (!CheckWinner())
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
            View.WriteDelimiter();
            View.DrawBoard();
            View.WritePlayerMessage();
        }

        private bool CheckWinner()
        {
            if (CurrentGame.CheckGameEnd())
            {
                View.WriteCongratulations(CurrentGame.CurrentPlayer);
                StartGame(_gameModePreference);
                return true;
            }
            return false;
        }

        private void StartBotTurn()
        {
            (string command, IElement element) = CurrentGame.CurrentPlayer.DoMove(CurrentGame);
            switch (command)
            {
                case "move":
                    CurrentGame.MakeMove((Cell)element);
                    View.WriteBotMove(command, (Cell)element);
                    break;
                case "jump":
                    CurrentGame.MakeMove((Cell)element, true);
                    View.WriteBotMove(command, (Cell)element);
                    break;
                case "wall":
                    CurrentGame.PlaceWall((Wall)element);
                    View.WriteBotPlaceWall(command, (Wall)element);
                    break;
                default:
                    return;
            }
            CheckWinner();
        }

    }

}
