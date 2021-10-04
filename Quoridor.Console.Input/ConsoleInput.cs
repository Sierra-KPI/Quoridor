using System;
using Quoridor.Model;

namespace Quoridor.OutputConsole.Input
{
    public class ConsoleInput
    {
        QuoridorGame _game;

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
