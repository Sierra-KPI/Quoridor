using System;
using System.Collections.Generic;
using Quoridor.Model;

namespace Quoridor.OutputConsole.Input
{
    public class ConsoleInput
    {
        private readonly QuoridorGame _game;

        // private Dictionary<string, Action<>> _stringValues = new();

        private readonly string _greetingMessage = "Hi! Now You are " +
            "playing Quoridor. The object of the game is to advance " +
            "your pawn to the opposite edge of the board. On your " +
            "turn you may either move your pawn or place a wall. " +
            "You may hinder your opponent with wall placement, " +
            "but not completely block them off. Meanwhile, " +
            "they are trying to do the same to you. The first " +
            "pawn to reach the opposite side wins.";
        private readonly string _nullOrEmptyMessage = "Your input " +
            "is empty! Try again";
        private readonly string _helpMessage = "Here's some tips " +
            "tips on how to play the game:\n1. player x y - move " +
            "Your player from x cell to y cell\n2. wall x y - place " +
            "wall from x cell to y cell\n3. help - print this " +
            "helpbox\n4. quit - quit the game";

        public ConsoleInput(QuoridorGame game)
        {
            _game = game;
            WriteGreeting();
        }

        public void WriteGreeting()
        {
            Console.WriteLine(_greetingMessage);
            Console.WriteLine(_helpMessage);
        }

        public void ReadMove()
        {
            var endLoop = false;
            while(!endLoop)
            {
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine(_nullOrEmptyMessage);
                }
                else
                {
                    string[] inputValues = input.Split(Array.Empty<char>());
                    switch (inputValues[0])
                    {
                        case "player":
                            _game.MakeMove(inputValues[1], inputValues[2]);
                            break;
                        case "wall":
                            _game.PlaceWall(inputValues[1], inputValues[2]);
                            break;
                        case "quit":
                            endLoop = true;
                            break;
                        case "help":
                            Console.WriteLine(_helpMessage);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
