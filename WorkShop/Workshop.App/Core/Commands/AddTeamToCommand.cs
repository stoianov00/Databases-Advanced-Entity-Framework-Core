using System;
using Workshop.App.Interfaces;
using Workshop.Services;

namespace Workshop.App.Core.Commands
{
    internal class AddTeamToCommand : ICommand
    {
        private readonly UserService userService;
        private readonly TeamService teamService;
        private readonly EventService eventService;

        public AddTeamToCommand(UserService userService, TeamService teamService, EventService eventService)
        {
            this.userService = userService;
            this.teamService = teamService;
            this.eventService = eventService;
        }

        public string Execute(params string[] args)
        {
            string eventName = args[0];
            string teamName = args[1];

            if (!this.userService.IsAuthenticated())
            {
                throw new InvalidOperationException("You should login first!");
            }

            var loggedUser = this.userService.GetCurrentUser();
            if (this.eventService.IsUserCreatorOfEvent(eventName, loggedUser))
            {
                throw new InvalidOperationException("Not allowed!");
            }

            if (!this.teamService.IsTeamExist(teamName))
            {
                throw new ArgumentException($"Team {teamName} not found!");
            }

            if (!this.eventService.IsEventExisting(eventName))
            {
                throw new ArgumentException($"Event {eventName} not found!");
            }

            this.eventService.AddTeamTo(eventName, teamName);

            return $"Team {teamName} added for {eventName}!";
        }
    }
}
