using System.Collections.Generic;

namespace Exercises.Data.Models
{
    public class Passport
    {
        public Passport()
        {
            this.Persons = new HashSet<Person>();
        }

        public int PassportId { get; set; }
        public string PassportNumber { get; set; }

        public ICollection<Person> Persons { get; set; }
    }
}
