using System;
using Workshop.App.Interfaces;

namespace Workshop.App.Core.Commands
{
    internal class ExitCommand : ICommand
    {
        // Exit
        public string Execute(params string[] args)
        {
            Console.WriteLine("Bye Bye !");
            Environment.Exit(0);
            return string.Empty;
        }
    }
}
