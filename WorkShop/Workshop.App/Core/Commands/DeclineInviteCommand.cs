using System;
using Workshop.App.Interfaces;
using Workshop.Services;

namespace Workshop.App.Core.Commands
{
    internal class DeclineInviteCommand : ICommand
    {
        private readonly UserService userService;
        private readonly TeamService teamService;
        private readonly InvitationService invitationService;

        public DeclineInviteCommand(UserService userService, TeamService teamService, InvitationService invitationService)
        {
            this.userService = userService;
            this.teamService = teamService;
            this.invitationService = invitationService;
        }

        // DeclineInvite <teamName>
        public string Execute(params string[] args)
        {
            string teamName = args[0];

            var loggedUser = this.userService.GetCurrentUser();

            if (!this.userService.IsAuthenticated())
            {
                throw new ArgumentException("You should login first!");
            }

            if (!this.teamService.IsTeamExist(teamName))
            {
                throw new ArgumentException($"Team {teamName} not found!");
            }

            if (!this.invitationService.IsInviteExist(teamName, loggedUser))
            {
                throw new ArgumentException($"Invite from {teamName} is not found!");
            }

            this.invitationService.DeclineInvite(teamName, loggedUser);
            return $"Invite from {teamName} declined!";
        }
    }
}
