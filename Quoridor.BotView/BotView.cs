using System;

namespace Quoridor.View
{
    public class BotView
    {
        public void PrintCommand(string command)
        {
            Console.WriteLine(command);
        }

        public string GetCommand()
        {
            return Console.ReadLine();
        }
    }
}
