namespace PhotoShare.Client.Core
{
    using PhotoShare.Client.Interfaces;
    using System;

    public class Engine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly CommandDispatcher commandDispatcher;

        public Engine(IReader reader, IWriter writer, CommandDispatcher commandDispatcher)
        {
            this.reader = reader;
            this.writer = writer;
            this.commandDispatcher = commandDispatcher;
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    string input = this.reader.ReadLine().Trim();
                    string[] data = input.Split(' ');
                    string result = this.commandDispatcher.DispatchCommand(data);
                    this.writer.WriteLine(result);
                }
                catch (Exception e)
                {
                    this.writer.WriteLine(e.Message);
                }
            }
        }

    }
}
