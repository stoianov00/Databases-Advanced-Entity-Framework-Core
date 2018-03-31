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
    internal class ShowEventCommand : ICommand
    {
        private readonly TeamBuilderDbContext context;
        private EventService eventService;

        public ShowEventCommand(TeamBuilderDbContext context, EventService eventService)
        {
            this.context = context;
            this.eventService = eventService;
        }

        // ShowEvent <eventName>
        public string Execute(params string[] args)
        {
            string eventName = args[0];
            
            if (!this.eventService.IsEventExisting(eventName))
            {
                throw new ArgumentException($"Event {eventName} not found!");
            }

            Event eventa = null;

            eventa = this.context.Events
                  .Include(e => e.ParticipatingEventTeams)
                  .ThenInclude(pet => pet.Team)
                  .OrderByDescending(e => e.StartDate)
                  .First(e => e.Name == eventName);

            var sb = new StringBuilder();

            sb.AppendLine($"{eventa.Name} {eventa.StartDate.ToString()} {eventa.EndDate.ToString()}");
            sb.AppendLine($"{eventa.Description}");
            sb.AppendLine($"Teams:");

            foreach (var pet in eventa.ParticipatingEventTeams)
            {
                sb.AppendLine($"-{pet.Team.Name}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
