using Quoridor.View;
using Quoridor.Model;
using System;
using Quoridor.OutputConsole.Input;

namespace Quoridor.OutputConsole.App
{
    internal class Program
    {
        private static void Main()
        {
            ConsoleInput input = new();
            ViewOutput viewOutput = new(game);
            viewOutput.DrawBoard();
            input.ReadMove();
        }
    }
}
