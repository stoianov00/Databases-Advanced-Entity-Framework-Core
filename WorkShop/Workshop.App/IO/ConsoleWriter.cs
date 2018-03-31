using System;
using Workshop.App.Interfaces;

namespace Workshop.App.IO
{
    internal class ConsoleWriter : IWriter
    {
        public void Write(string line)
        {
            Console.Write(line);
        }

        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }

        public void WriteLine(string format, params string[] args)
        {
            Console.WriteLine(string.Format(format, args));
        }
    }
}
