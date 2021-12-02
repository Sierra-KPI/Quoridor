using System;

namespace Quoridor.OutputConsole.Input
{
    public class ConsoleInput
    {
        #region Methods

        public string[] ReadMove()
        {
            string input = Console.ReadLine();
            string[] inputString = input.Split(Array.Empty<char>());
            return inputString;
        }

        #endregion Methods
    }
}
