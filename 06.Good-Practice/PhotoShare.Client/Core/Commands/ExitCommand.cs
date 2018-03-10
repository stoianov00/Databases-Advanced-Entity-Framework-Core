namespace PhotoShare.Client.Core.Commands
{
    using System;

    public class ExitCommand
    {
        public static string Execute()
        {
            Console.WriteLine("Bye-bye!");
            Environment.Exit(Environment.ExitCode);
            return "Bye-bye!";
        }
    }
}
