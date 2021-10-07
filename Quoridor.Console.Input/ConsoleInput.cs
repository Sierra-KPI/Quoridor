﻿using System;
using System.Collections.Generic;
using Quoridor.Model;
using Quoridor.View;

namespace Quoridor.OutputConsole.Input
{
    public class ConsoleInput
    {
        public QuoridorGame CurrentGame;
        public ViewOutput View;

        private Dictionary<string, int> _chars = new();
        private bool _endLoop;
        private string _currentPlayerName = "First Player";
        private IPlayer _currentPlayer;
        private string[] _gameModePreference;

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
            "(1 for 1 real and 1 bot. 2 for two real players)" +
            "\n2. player x y - move Your player from x cell to" +
            " y cell\n3. wall x1 y1 x2 y2 - place wall from " +
            "x1 y1 cell to x2 y2 cell\n4. help - print this " +
            "helpbox\n5. quit - quit the game";
        private const string IncorrectMessage = "Incorrect command! " +
            "Try something else";
        private const string CongratulationsMessage = " Has won!";
        private const string CurrentPlayerMessage = "Current player is ";
        private const string DelimiterMessage = "-----------------" +
            "----------------------------";

        public ConsoleInput()
        {
            WriteStartingMessage();
            InitializeDictionaries();
        }

        public void WriteStartingMessage()
        {
            Console.WriteLine(GreetingMessage);
            Console.WriteLine(HelpMessage);
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
                    Console.WriteLine(NullOrEmptyMessage);
                }
                else
                {
                    string[] inputValues = input.Split(Array.Empty<char>());
                    ExecuteCommand(inputValues);
                }
            }
        }

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
                        QuitLoop();
                        break;
                    case "help":
                        WriteHelpMessage();
                        break;
                    default:
                        WriteIncorrectMessage();
                        break;
                }
                DoAfterCommand();
            }
            catch (Exception)
            {
                WriteIncorrectMessage();
            }
        }

        private void StartGame(string[] values)
        {
            _gameModePreference = values;
            Board board = new BoardFactory().CreateBoard();

            Cell firstPlayerCell = board.GetStartCellForPlayer
                ((int)PlayerID.First);
            Cell[] firstPlayerEndCells = board.GetEndCellsForPlayer
                (board.GetStartCellForPlayer((int)PlayerID.First));
            Player firstPlayer = new(firstPlayerCell, firstPlayerEndCells);
            _currentPlayer = firstPlayer;

            Cell secondPlayerCell = board.GetStartCellForPlayer
                ((int)PlayerID.Second);
            Cell[] secondPlayerEndCells = board.GetEndCellsForPlayer
                (board.GetStartCellForPlayer((int)PlayerID.Second));

            if (values[1] == "1")
            {
                Bot secondPlayer = new(secondPlayerCell, secondPlayerEndCells);
                CurrentGame = new QuoridorGame(firstPlayer,
                    secondPlayer, board);

                Console.WriteLine("New Singleplayer Game has started!");
            }
            else
            {
                Player secondPlayer = new(secondPlayerCell, secondPlayerEndCells);
                CurrentGame = new QuoridorGame(firstPlayer,
                    secondPlayer, board);

                Console.WriteLine("New Multiplayer Game has started!");
            }

            View = new(CurrentGame);
        }

        private void MovePlayer(string[] values)
        {
            Coordinates coordinates = new(int.Parse(values[1]) - 1,
                _chars[values[2]] - 1);
            Cell to = CurrentGame.CurrentBoard.
                GetCellByCoordinates(coordinates);
            _currentPlayer = CurrentGame.CurrentPlayer;
            CurrentGame.MakeMove(to);
        }

        private void PlaceWall(string[] values)
        {
            Coordinates firstCoordinates = new(int.Parse(values[1]) - 1,
                _chars[values[2]] - 1);
            Coordinates secondCoordinates = new(int.Parse(values[3]) - 1,
                _chars[values[4]] - 1);

            Cell from = CurrentGame.CurrentBoard.
                GetCellByCoordinates(firstCoordinates);
            Cell to = CurrentGame.CurrentBoard.
                GetCellByCoordinates(secondCoordinates);

            CurrentGame.PlaceWall(from, to);
        }

        // TO-DO rename Me PLS
        private void DoAfterCommand()
        {
            WriteDelimiter();
            View.DrawBoard();

            if (_currentPlayer.HasWon())
            {
                WriteCongratulations();
                WriteDelimiter();

                StartGame(_gameModePreference);
                View.DrawBoard();
            }
            WritePlayerMessage();
            //Bot.DoSomething()
        }

        private static void WriteIncorrectMessage()
        {
            Console.WriteLine(IncorrectMessage);
        }

        private static void WriteHelpMessage()
        {
            Console.WriteLine(HelpMessage);
        }

        private static void WriteDelimiter()
        {
            Console.WriteLine(DelimiterMessage);
        }

        private void WritePlayerMessage()
        {
            if (CurrentGame.CurrentPlayer == CurrentGame.FirstPlayer)
            {
                _currentPlayerName = "First Player";
            }
            else
            {
                _currentPlayerName = "Second Player";
            }

            Console.WriteLine(CurrentPlayerMessage +
                _currentPlayerName);
        }

        private void WriteCongratulations()
        {
            Console.WriteLine(_currentPlayerName + CongratulationsMessage);
        }

        private void QuitLoop()
        {
            _endLoop = true;
        }
    }
}
