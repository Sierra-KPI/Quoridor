using Quoridor.View;

namespace Quoridor.Console.App
{
    internal class Program
    {
        static void Main()
        {
            ViewOutput viewOutput= new();
            viewOutput.DrawBoard();
        }
    }
}
