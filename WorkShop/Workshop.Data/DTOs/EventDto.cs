using System;

namespace Workshop.Data.DTOs
{
    public class EventDto
    {
        public EventDto()
        {

        }

        public EventDto(string name, string description, DateTime startDate, DateTime endDate)
        {
            this.Name = name;
            this.Description = description;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
