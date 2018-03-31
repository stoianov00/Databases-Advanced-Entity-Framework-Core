using System;
using Workshop.App.Interfaces;

namespace Workshop.App.IO
{
    internal class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
