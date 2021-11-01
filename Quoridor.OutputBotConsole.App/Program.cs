using Quoridor.OutputBotConsole.Input;

namespace Quoridor.OutputBotConsole.App
{
    internal class Program
    {
        private static void Main()
        {
            ConsoleBotInput input = new();
            input.OnStart();
            input.ReadMove();
        }
    }
}
