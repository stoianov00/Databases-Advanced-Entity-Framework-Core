using System.Collections.Generic;
using Workshop.Models.Enums;
using Workshop.Models.Validations;
 
namespace Workshop.Models
{
    public class User
    {
        public User()
        {
            this.CreatedEvents = new HashSet<Event>();
            this.ReceivedInvitations = new HashSet<Invitation>();
            this.UserTeams = new HashSet<UserTeam>();
            this.CreatedTeams = new HashSet<Team>();
        }

        public User(string username, Gender gender, string password)
        {
            this.Username = username;
            this.Gender = gender;
            this.Password = password;
        }

        public int Id { get; set; }

        [Username(3, 25, ErrorMessage = "Invalid Username")]
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Password(6, 30, ContainsDigit = true, ContainsUppercase = true, ErrorMessage = "Invalid Password")]
        public string Password { get; set; }

        public Gender? Gender { get; set; }

        public int? Age { get; set; }

        public bool? IsDeleted { get; set; }

        public ICollection<Event> CreatedEvents { get; set; }

        public ICollection<Invitation> ReceivedInvitations { get; set; }

        public ICollection<UserTeam> UserTeams { get; set; }

        public ICollection<Team> CreatedTeams { get; set; }

    }
}
