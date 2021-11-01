namespace Quoridor.OutputConsole.App
{
    internal class Program
    {
        private static void Main()
        {
            // Input.ConsoleInput input = new();
            Input.ConsoleBotInput input = new();
            input.OnStart();
            input.ReadMove();
        }
    }
}
