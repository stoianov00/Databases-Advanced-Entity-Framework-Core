using System;
using Workshop.App.Interfaces;
using Workshop.Services;

namespace Workshop.App.Core.Commands
{
    internal class AcceptInviteCommand : ICommand
    {
        private readonly UserService userService;
        private readonly TeamService teamService;
        private readonly InvitationService invitationService;

        public AcceptInviteCommand(UserService userService, TeamService teamService, InvitationService invitationService)
        {
            this.userService = userService;
            this.teamService = teamService;
            this.invitationService = invitationService;
        }

        // AcceptInvite <teamName>
        public string Execute(params string[] args)
        {
            string teamName = args[0];

            var loggedUser = this.userService.GetCurrentUser();

            if (!this.userService.IsAuthenticated())
            {
                throw new InvalidOperationException("You should login first!");
            }

            if (!this.teamService.IsTeamExist(teamName))
            {
                throw new ArgumentException($"Team {teamName} not found!");
            }

            if (!this.invitationService.IsInviteExist(teamName, loggedUser))
            {
                throw new ArgumentException($"Invite from {teamName} is not found!");
            }

            return $"User {loggedUser.Username} joined team {teamName}!";
        }
    }
}
