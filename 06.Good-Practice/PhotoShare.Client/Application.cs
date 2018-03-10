namespace PhotoShare.Client
{
    using Core;
    using Data;
    using PhotoShare.Client.Interfaces;
    using PhotoShare.Client.IO;

    public class Application
    {
        public static void Main()
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();
            var commandDispatcher = new CommandDispatcher();

            var engine = new Engine(reader, writer, commandDispatcher);
            engine.Run();
        }

        private static void ResetDatabase()
        {
            using (var db = new PhotoShareContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
    }
}
