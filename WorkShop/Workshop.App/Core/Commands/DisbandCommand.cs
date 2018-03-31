using System;
using Workshop.App.Interfaces;
using Workshop.Services;

namespace Workshop.App.Core.Commands
{
    internal class DisbandCommand : ICommand
    {
        private readonly UserService userService;
        private readonly TeamService teamService;

        public DisbandCommand(UserService userService, TeamService teamService)
        {
            this.userService = userService;
            this.teamService = teamService;
        }

        // Disband <teamName>
        public string Execute(params string[] args)
        {
            string teamName = args[0];

            if (!this.userService.IsAuthenticated())
            {
                throw new InvalidOperationException("You should login first!");
            }

            if (!this.teamService.IsTeamExist(teamName))
            {
                throw new ArgumentException($"Team {teamName} not found!");
            }

            var loggedUser = this.userService.GetCurrentUser();
            if (!this.teamService.IsUserCreatorOfTeam(teamName, loggedUser))
            {
                throw new InvalidOperationException("Not allowed!");
            }

            this.teamService.Disband(teamName);

            return $"{teamName} has disbanded!";
        }
    }
}
