using System.Collections.Generic;

namespace _02.FootballBetting.Data.Models
{
    public class Country
    {
        public Country()
        {
            this.Towns = new HashSet<Town>();    
        }

        public int CountryId { get; set; }

        public string Name { get; set; }

        public ICollection<Town> Towns { get; set; }
    }
}