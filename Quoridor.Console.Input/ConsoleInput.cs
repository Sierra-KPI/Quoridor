using System;
using System.Collections.Generic;
using Quoridor.Model;

namespace Quoridor.OutputConsole.Input
{
    public class ConsoleInput
    {
        private QuoridorGame _game;

        private Dictionary<string, int> _chars = new();
        private bool _endLoop;

        private readonly string _greetingMessage = "Hi! Now You are " +
            "playing Quoridor.\nThe object of the game is to advance " +
            "your pawn to the opposite edge of the board.\nOn your " +
            "turn you may either move your pawn or place a wall. " +
            "\nYou may hinder your opponent with wall placement, " +
            "but not completely block them off.\nMeanwhile, " +
            "they are trying to do the same to you.\nThe first " +
            "pawn to reach the opposite side wins.";
        private readonly string _nullOrEmptyMessage = "Your input " +
            "is empty! Try again";
        private readonly string _helpMessage = "Here's some tips " +
            "tips on how to play the game:\n1. start x - start new " +
            "game, where is the number of real players " +
            "(1 for 1 real and 1 bot. 2 for two real players)" +
            "\n2. player x y - move Your player from x cell to" +
            " y cell\n3. wall x1 y1 x2 y2 - place wall from " +
            "x1 y1 cell to x2 y2 cell\n4. help - print this " +
            "helpbox\n5. quit - quit the game";
        private readonly string _incorrectMessage = "Incorrect command! " +
            "Try something else";

        public ConsoleInput()
        {
            WriteGreeting();
            InitializeDictionaries();
        }

        public void WriteGreeting()
        {
            Console.WriteLine(_greetingMessage);
            Console.WriteLine(_helpMessage);
        }

        private void InitializeDictionaries()
        {
            _chars = new Dictionary<string, int>
            {
                { "A", 1 },
                { "B", 2 },
                { "C", 3 },
                { "D", 4 },
                { "E", 5 },
                { "F", 6 },
                { "G", 7 },
                { "H", 8 },
                { "I", 9 }
            };


        }

        public void ReadMove()
        {
            while(!_endLoop)
            {
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine(_nullOrEmptyMessage);
                }
                else
                {
                    string[] inputValues = input.Split(Array.Empty<char>());
                    ExecuteCommand(inputValues);
                }
            }
        }

        // TO-DO bot vs player and player vs player
        private void ExecuteCommand(string[] values)
        {
            try
            {
                switch (values[0])
                {
                    case "start":
                        StartGame(values);
                        break;
                    case "player":
                        MovePlayer(values);
                        break;
                    case "wall":
                        PlaceWall(values);
                        break;
                    case "quit":
                        _endLoop = QuitLoop();
                        break;
                    case "help":
                        WriteHelpMessage();
                        break;
                    default:
                        WriteIncorrectMessage();
                        break;
                }
            }
            catch (Exception)
            {
                WriteIncorrectMessage();
            }
        }

        private void StartGame(string[] values)
        {
            Board board = new BoardFactory().CreateBoard();
            Player firstPlayer = new(board.GetStartCellForPlayer(1),
                board.GetEndCellsForPlayer(board.GetStartCellForPlayer(1)));

            if (values[1] == "1")
            {
                Bot secondPlayer = new(board.GetStartCellForPlayer(2),
                board.GetEndCellsForPlayer(board.GetStartCellForPlayer(2)));
                _game = new QuoridorGame(firstPlayer, secondPlayer, board);
            }
            else
            {
                Player secondPlayer = new(board.GetStartCellForPlayer(2),
                board.GetEndCellsForPlayer(board.GetStartCellForPlayer(2)));
                _game = new QuoridorGame(firstPlayer, secondPlayer, board);
            }
        }

        private void MovePlayer(string[] values)
        {
            Coordinates coordinates = new(int.Parse(values[1]),
                _chars[values[2]]);
            Cell to = _game.CurrentBoard.GetCellByCoordinates(coordinates);

            _game.MakeMove(to);
        }

        private void PlaceWall(string[] values)
        {
            Coordinates firstCoordinates = new(int.Parse(values[1]),
                int.Parse(values[2]));
            Coordinates secondCoordinates = new(int.Parse(values[3]),
                int.Parse(values[4]));
            Cell from = _game.CurrentBoard.GetCellByCoordinates(firstCoordinates);
            Cell to = _game.CurrentBoard.GetCellByCoordinates(secondCoordinates);

            _game.PlaceWall(from, to);
        }

        private void WriteIncorrectMessage()
        {
            Console.WriteLine(_incorrectMessage);
        }

        private void WriteHelpMessage()
        {
            Console.WriteLine(_helpMessage);
        }

        private static bool QuitLoop() => true;
    }
}
