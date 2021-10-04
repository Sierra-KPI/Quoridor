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
        }

        public void ReadMove()
        {
            string input = "";

            while(true)
            {
                input = Console.ReadLine();
                var splitCommand = input.Split(new char[0]);
                foreach (var command in splitCommand)
                {
                    Console.WriteLine(command);
                }
            }
        }
    }
}
