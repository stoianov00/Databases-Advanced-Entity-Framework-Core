using Workshop.Data;
using Workshop.Data.DTOs;
using AutoMapper;
using Workshop.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Workshop.Services
{
    public class EventService
    {
        private readonly TeamBuilderDbContext context;

        public EventService(TeamBuilderDbContext context)
        {
            this.context = context;
        }

        public void CreateEvent(EventDto dto)
        {
            var createEvent = Mapper.Map<Event>(dto);

            this.context.Events.Add(createEvent);
            this.context.SaveChanges();
        }

        public bool IsEventExisting(string eventName)
        {
            return this.context.Events.Any(e => e.Name == eventName);
        }

        public bool IsUserCreatorOfEvent(string eventName, User user)
        {
            return this.context.Events.Any(e => e.Name == eventName && e.CreatorId == user.Id);
        }

        public DateTime ParseStartDate(string startDate)
        {
            DateTime startDateOutput;
            if (DateTime.TryParse(startDate, out startDateOutput))
            {
                string.Format("dd/MM/yyyy HH:mm", startDateOutput);
            }
            else
            {
                throw new ArgumentException($"Please insert the dates in format: [dd/MM/yyyy HH:mm]!");
            }

            return startDateOutput;
        }

        public DateTime ParseEndDate(string endDate)
        {
            DateTime endDateOutput;
            if (DateTime.TryParse(endDate, out endDateOutput))
            {
                string.Format("dd/MM/yyyy HH:mm", endDateOutput);
            }
            else
            {
                throw new ArgumentException($"Please insert the dates in format: [dd/MM/yyyy HH:mm]!");
            }

            return endDateOutput;
        }

        public void AddTeamTo(string eventName, string teamName)
        {
            Event eventa = null;
            Team team = null;

            eventa = this.context.Events
                   .Where(e => e.Name == eventName)
                   .OrderBy(e => e.StartDate)
                   .Last();

            team = this.context.Teams.FirstOrDefault(t => t.Name == teamName);

            if (this.context.EventTeams.Any(et => et.Team == team && et.Event == eventa))
            {
                throw new InvalidOperationException("Cannot add same team twice!");
            }

            var result = new TeamEvent(eventa, team);
            this.context.EventTeams.Add(result);
            this.context.SaveChanges();
        }

    }
}
