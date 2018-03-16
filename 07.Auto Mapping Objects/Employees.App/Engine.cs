using Employees.App.Interfaces;
using System;
using System.Linq;

namespace Employees.App
{
    internal class Engine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider, IReader reader, IWriter writer)
        {
            this.serviceProvider = serviceProvider;
            this.reader = reader;
            this.writer = writer;
        }

        internal void Run()
        {
            while (true)
            {
                this.writer.Write("Enter Command: ");
                string input = this.reader.ReadLine();
                string[] commandTokens = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                string commandName = commandTokens[0];
                string[] commandArgs = commandTokens.Skip(1).ToArray();

                var command = CommandParser.Parse(this.serviceProvider, commandName);
                var result = command.Execute(commandArgs);

                this.writer.WriteLine(result);
            }
        }

    }
}
