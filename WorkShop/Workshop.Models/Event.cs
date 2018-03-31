using System;
using System.Collections.Generic;

namespace Workshop.Models
{
    public class Event
    {
        private DateTime? endDate;

        public Event()
        {
            this.ParticipatingEventTeams = new HashSet<TeamEvent>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate
        {
            get => this.endDate;
            set
            {
                if (this.StartDate == null || this.StartDate > value)
                {
                    throw new ArgumentException("Start date should be before end date!");
                }

                this.endDate = value;
            }
        }

        public int? CreatorId { get; set; }

        public User Creator { get; set; }

        public ICollection<TeamEvent> ParticipatingEventTeams { get; set; }
    }
}
