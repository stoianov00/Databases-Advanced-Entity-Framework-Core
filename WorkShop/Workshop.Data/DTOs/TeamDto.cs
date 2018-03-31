namespace Workshop.Data.DTOs
{
    public class TeamDto
    {
        public TeamDto()
        {

        }

        public TeamDto(string name, string acronym)
        {
            this.Name = name;
            this.Acronym = acronym;
        }

        public TeamDto(string name, string acronym, string description)
            : this(name, acronym)
        {
            this.Description = description;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Acronym { get; set; }

        public string Description { get; set; }

        public int CreatorId { get; set; }
    }
}
