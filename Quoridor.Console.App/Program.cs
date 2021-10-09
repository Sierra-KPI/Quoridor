using Quoridor.OutputConsole.Input;

namespace Quoridor.OutputConsole.App
{
    internal class Program
    {
        private static void Main()
        {
            ConsoleInput input = new();
            input.OnStart();
            input.ReadMove();
        }
    }
}
