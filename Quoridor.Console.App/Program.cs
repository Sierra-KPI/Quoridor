using Quoridor.View;

namespace Quoridor.Console.App
{
    internal class Program
    {
        private static void Main()
        {
            ViewOutput viewOutput = new();
            viewOutput.DrawBoard();
        }
    }
}
