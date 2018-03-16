using Employees.App.Interfaces;
using System;

namespace Employees.App.IO
{
    internal class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
