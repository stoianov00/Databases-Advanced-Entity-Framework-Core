namespace PhotoShare.Models
{
    using System;
    using System.Collections.Generic;

    public class Town
    {
        public Town()
        {
            this.UsersBornInTown = new HashSet<User>();
            this.UsersCurrentlyLivingInTown = new HashSet<User>();
        }

        public Town(string townName, string countryName)
        {
            this.Name = townName;
            this.Country = countryName;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public ICollection<User> UsersBornInTown { get; set; }

        public ICollection<User> UsersCurrentlyLivingInTown { get; set; }

        public override string ToString()
        {
            return $"{this.Name}, {this.Country}";
        }

        public static implicit operator Town(string v)
        {
            throw new NotImplementedException();
        }
    }
}
