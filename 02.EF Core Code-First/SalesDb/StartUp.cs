using SalesDatabase.Data;

namespace SalesDb
{
    public class StartUp
    {
        public static void Main()
        {
            using (var db = new SalesContext())
            {
                db.Database.EnsureCreated();
            }
        }
    }
}
