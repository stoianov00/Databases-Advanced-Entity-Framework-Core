using System;
using Workshop.App.Interfaces;
using Workshop.Data.DTOs;
using Workshop.Services;

namespace Workshop.App.Core.Commands
{
    internal class CreateEventCommand : ICommand
    {
        private readonly EventService eventService;
        private readonly UserService userService;

        public CreateEventCommand(EventService eventService, UserService userService)
        {
            this.eventService = eventService;
            this.userService = userService;
        }

        // CreateEvent <name> <description> <startDate> <endDate>
        public string Execute(params string[] args)
        {
            string eventName = args[0];
            string description = args[1];
            string startDateInput = args[2];
            string endDateInput = args[3];
            DateTime startDate = this.eventService.ParseStartDate(startDateInput);
            DateTime endDate = this.eventService.ParseEndDate(endDateInput);
            
            if (!this.userService.IsAuthenticated())
            {
                throw new InvalidOperationException("You should login first!");
            }

            var dto = new EventDto(eventName, description, startDate, endDate);
            this.eventService.CreateEvent(dto);

            return $"Event {eventName} was created successfully!";
        }
    }
}
