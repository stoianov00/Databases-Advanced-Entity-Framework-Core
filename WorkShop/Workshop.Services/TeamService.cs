using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Workshop.Data;
using Workshop.Data.DTOs;
using Workshop.Models;

namespace Workshop.Services
{
    public class TeamService
    {
        private readonly TeamBuilderDbContext context;
        private readonly UserService service;

        public TeamService(TeamBuilderDbContext context, UserService service)
        {
            this.context = context;
            this.service = service;
        }

        public void CreateTeam(TeamDto dto)
        {
            var createTeam = Mapper.Map<Team>(dto);

            this.context.Teams.Add(createTeam);
            this.context.SaveChanges();
        }

        public TeamDto FindTeamByTeamName(string teamName)
        {
            var team = this.context.Teams.FirstOrDefault(t => t.Name == teamName);

            var dto = Mapper.Map<TeamDto>(team);

            return dto;
        }

        public bool IsAcronymValid(string acronym)
        {
            return acronym.Length != 3;
        }

        public bool IsTeamExist(string teamName)
        {
            return this.context.Teams.Any(t => t.Name == teamName);
        }

        public bool IsUserCreatorOfTeam(string teamName, User user)
        {
            return this.context.Teams.Any(t => t.Name == teamName && t.CreatorId == user.Id);
        }

        public bool IsMemberOfTeam(string teamName, string username)
        {
            return this.context.Teams
                    .Include(t => t.Members)
                    .ThenInclude(m => m.User)
                    .Single(t => t.Name == teamName)
                    .Members.Any(m => m.User.Username == username);
        }

        public void Disband(string teamName)
        {
            var team = this.context.Teams.SingleOrDefault(t => t.Name == teamName);

            var eventTeams = this.context.EventTeams.Where(et => et.Team == team);
            var userTeams = this.context.UserTeams.Where(ut => ut.Team == team);
            var invitations = this.context.Invitations.Where(i => i.Team == team);

            this.context.EventTeams.RemoveRange(eventTeams);
            this.context.UserTeams.RemoveRange(userTeams);
            this.context.Invitations.RemoveRange(invitations);

            this.context.Teams.Remove(team);
            this.context.SaveChanges();
        }

    }
}
