using System;
using System.Linq;
using Workshop.App.Interfaces;

namespace Workshop.App.Core
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
                try
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
                catch (Exception e)
                {
                    Console.WriteLine(e.GetBaseException().Message);
                }
            }
        }

    }
}
