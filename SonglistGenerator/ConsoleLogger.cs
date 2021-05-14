using System;

namespace SonglistGenerator
{
    public class ConsoleLogger
    {
        public void WriteLine(string message)
        {
            Console.WriteLine($"{DateTime.Now}: {message}");
        }
    }
}
