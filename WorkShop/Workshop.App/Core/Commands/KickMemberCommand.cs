using System;
using Workshop.App.Interfaces;
using Workshop.Services;

namespace Workshop.App.Core.Commands
{
    internal class KickMemberCommand : ICommand
    {
        private readonly UserService userService;
        private readonly TeamService teamService;

        public KickMemberCommand(UserService userService, TeamService teamService)
        {
            this.userService = userService;
            this.teamService = teamService;
        }

        // KickMember <teamName> <username>
        public string Execute(params string[] args)
        {
            string teamName = args[0];
            string username = args[1];

            if (!this.userService.IsAuthenticated())
            {
                throw new InvalidOperationException("You should login first!");
            }

            if (!this.teamService.IsTeamExist(teamName))
            {
                throw new ArgumentException($"Team {teamName} not found!");
            }

            if (!this.userService.IsUserExist(username))
            {
                throw new ArgumentException($"User {username} not found!");
            }
            
            if (!this.teamService.IsMemberOfTeam(teamName, username))
            {
                throw new ArgumentException($"User {username} is not a member in {teamName}!");
            }

            var loggedUser = this.userService.GetCurrentUser();

            if (!this.teamService.IsUserCreatorOfTeam(teamName, loggedUser))
            {
                throw new ArgumentException("Not allowed!");
            }

            if (loggedUser.Username == username)
            {
                throw new InvalidOperationException("Command not allowed. Use Disband instead.");
            }

            this.userService.KickMember(teamName, username);

            return $"User {username} was kicked from {teamName}";
        }
    }
}
