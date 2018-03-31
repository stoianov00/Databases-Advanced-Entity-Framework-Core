using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using Workshop.App.Interfaces;
using Workshop.Data;
using Workshop.Models;
using Workshop.Services;

namespace Workshop.App.Core.Commands
{
    internal class ShowTeamCommand : ICommand
    {
        private readonly TeamBuilderDbContext context;
        private readonly TeamService teamService;

        public ShowTeamCommand(TeamBuilderDbContext context, TeamService teamService)
        {
            this.context = context;
            this.teamService = teamService;
        }

        public string Execute(params string[] args)
        {
            string teamName = args[0];

            if (!this.teamService.IsTeamExist(teamName))
            {
                throw new ArgumentException($"Team {teamName} not found!");
            }

            Team team = null;

            team = this.context.Teams
                .Include(t => t.Members)
                .ThenInclude(m => m.User)
                .SingleOrDefault(t => t.Name == teamName);

            var sb = new StringBuilder();

            sb.AppendLine($"{team.Name} {team.Acronym}");
            sb.AppendLine("Members:");

            foreach (var m in team.Members)
            {
                sb.AppendLine($"-{m.User.Username}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}