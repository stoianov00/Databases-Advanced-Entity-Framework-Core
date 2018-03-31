using AutoMapper;
using System.Linq;
using Workshop.Data;
using Workshop.Data.DTOs;
using Workshop.Models;
using System;

namespace Workshop.Services
{
    public class InvitationService
    {
        private readonly TeamBuilderDbContext context;

        public InvitationService(TeamBuilderDbContext context)
        {
            this.context = context;
        }

        public void SendInvitation(InvitationDto dto)
        {
            var invitation = Mapper.Map<Invitation>(dto);

            this.context.Invitations.Add(invitation);
            this.context.SaveChanges();
        }

        public bool IsInviteExist(string teamName, UserDto user)
        {
            return this.context.Invitations.Any(i => i.Team.Name == teamName && i.InvitedUserId == user.Id);
        }

        public bool IsInviteExist(string teamName, User user)
        {
            return this.context.Invitations.Any(i => i.Team.Name == teamName && i.InvitedUserId == user.Id);
        }

        public void DeclineInvite(string teamName, User user)
        {
            var team = this.context.Teams.FirstOrDefault(t => t.Name == teamName);
            var currentInvite = this.context.Invitations
                .Where(i => i.TeamId == team.Id && i.InvitedUserId == user.Id);

            this.context.Invitations
                .FirstOrDefault(i => i.TeamId == team.Id && i.InvitedUserId == user.Id)
                .IsActive = false;

            this.context.SaveChanges();
        }
    }
}
