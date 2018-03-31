using System;
using Workshop.App.Interfaces;
using Workshop.Data.DTOs;
using Workshop.Services;

namespace Workshop.App.Core.Commands
{
    internal class InviteToTeamCommand : ICommand
    {
        private readonly TeamService teamService;
        private readonly UserService userService;
        private readonly InvitationService invitationService;

        public InviteToTeamCommand(TeamService teamService, UserService userService, InvitationService invitationService)
        {
            this.teamService = teamService;
            this.userService = userService;
            this.invitationService = invitationService;
        }

        // InviteToTeam <teamName> <username>
        public string Execute(params string[] args)
        {
            string teamName = args[0];
            string username = args[1];

            var loggedUser = this.userService.GetCurrentUser();

            var invitedUser = this.userService.FindUserByUsername(username);
            var team = this.teamService.FindTeamByTeamName(teamName);

            if (!this.userService.IsAuthenticated())
            {
                throw new InvalidOperationException("You should login first!");
            }

            if (invitedUser == null || team == null)
            {
                throw new ArgumentException("Team or user does not exist!");
            }

            if (!this.teamService.IsUserCreatorOfTeam(teamName, loggedUser)
                || this.teamService.IsMemberOfTeam(team.Name, loggedUser.Username)
                || this.teamService.IsMemberOfTeam(team.Name, invitedUser.Username)
                )
            {
                throw new InvalidOperationException("Not allowed!");
            }

            if (this.invitationService.IsInviteExist(team.Name, invitedUser))
            {
                throw new InvalidOperationException("Invite is already sent!");
            }

            var invitation = new InvitationDto(invitedUser.Id, team.Id);
            this.invitationService.SendInvitation(invitation);

            return $"Team {teamName} invited {username}!";
        }
    }
}
