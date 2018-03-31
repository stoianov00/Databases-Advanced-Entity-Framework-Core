using Workshop.Data;

namespace Workshop.App.Core.Utilities
{
    internal class DbTools
    {
        internal static void ResetDb()
        {
            using (var context = new TeamBuilderDbContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }
    }
}