using _02.FootballBetting.Data;

namespace _02.FootballBetting.App
{
    public class StartUp
    {
        public static void Main()
        {
            var db = new FootballBettingContext();

            db.Database.EnsureCreated();
        }
    }
}