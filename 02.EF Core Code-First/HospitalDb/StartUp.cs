using HospitalDatabase.Data;
using P01_HospitalDatabase.Initializer;

namespace HospitalDb
{
    public class StartUp
    {
        public static void Main()
        {
            using (var db = new HospitalContext())
            {
                DatabaseInitializer.InitialSeed(db);
            }
        }
    }
}
