using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Workshop.Models
{
    public class Team
    {
        public Team()
        {
            this.Invitations = new HashSet<Invitation>();
            this.Members = new HashSet<UserTeam>();
            this.EventParticipated = new HashSet<TeamEvent>();
        }

        public int Id { get; set; }

        public string Name { get; set; }
         
        public string Description { get; set; }

        [MinLength(3)]
        public string Acronym { get; set; }

        public int CreatorId { get; set; }

        public User Creator { get; set; }

        public ICollection<Invitation> Invitations { get; set; }

        public ICollection<UserTeam> Members { get; set; }

        public ICollection<TeamEvent> EventParticipated { get; set; }

    }
}
