namespace Workshop.Data.DTOs
{
    public class InvitationDto
    {
        public InvitationDto(int invitedUserId, int teamId)
        {
            this.InvitedUserId = invitedUserId;
            this.TeamId = teamId;
        }
        
        public int InvitedUserId { get; set; }

        public int TeamId { get; set; }
          
        public bool IsActive { get; set; }
    }
}
