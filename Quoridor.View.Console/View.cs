using System;
using Quoridor.Model;

namespace Quoridor.View
{
    internal class View
    {
        public void DrawBoard()
        {
            // Cell 
        }

        public static string ReadMove()
        {
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                return "No input. Try again";
            }
            return input;
        }
    }
}
