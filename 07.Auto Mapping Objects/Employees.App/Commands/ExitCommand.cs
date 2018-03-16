using Employees.App.Interfaces;
using System;

namespace Employees.App.Commands
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
