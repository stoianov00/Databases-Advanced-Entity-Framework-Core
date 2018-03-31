namespace Workshop.Models
{
    public class TeamEvent
    {
        public TeamEvent()
        {

        }

        public TeamEvent(Event eventa, Team team)
        {
            this.Event = eventa;
            this.Team = team;
        }

        public int TeamId { get; set; }
        public Team Team { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
