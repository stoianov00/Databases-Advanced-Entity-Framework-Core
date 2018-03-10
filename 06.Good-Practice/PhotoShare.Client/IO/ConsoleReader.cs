using PhotoShare.Client.Interfaces;
using System;

namespace PhotoShare.Client.IO
{
    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
