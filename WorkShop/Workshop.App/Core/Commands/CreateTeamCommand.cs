using System;
using Workshop.App.Interfaces;
using Workshop.Data.DTOs;
using Workshop.Services;

namespace Workshop.App.Core.Commands
{
    internal class CreateTeamCommand : ICommand
    {
        private readonly TeamService teamService;
        private readonly UserService userService;

        public CreateTeamCommand(TeamService teamService, UserService userService)
        {
            this.teamService = teamService;
            this.userService = userService;
        }

        // CreateTeam <name> <acronym> <description>(optional)
        public string Execute(params string[] args)
        {
            int argsCount = args.Length;

            string teamName = args[0];
            string acronym = args[1];
            string description = argsCount == 3 ? args[2] : null;

            if (!this.userService.IsAuthenticated())
            {
                throw new InvalidOperationException("You should login first!");
            }

            if (this.teamService.IsTeamExist(teamName))
            {
                throw new ArgumentException($"Team {teamName} exists!");
            }

            if (this.teamService.IsAcronymValid(acronym))
            {
                throw new ArgumentException($"Acronym {acronym} not valid!");
            }

            var dto = new TeamDto(teamName, acronym, description)
            {
                CreatorId = this.userService.GetCurrentUser().Id
            };
            this.teamService.CreateTeam(dto);

            return $"Team {teamName} successfully created!";
        }
    }
}
