using System;
using Workshop.App.Interfaces;
using Workshop.Services;

namespace Workshop.App.Core.Commands
{
    internal class LogoutCommand : ICommand
    {
        private readonly UserService service;

        public LogoutCommand(UserService service)
        {
            this.service = service;
        }

        // Logout
        public string Execute(params string[] args)
        {
            if (args.Length != 0)
            {
                throw new InvalidOperationException("Invalid arguments count!");
            }

            if (!this.service.IsAuthenticated())
            {
                throw new InvalidOperationException("You should log in!");
            }

            var currentUser = this.service.GetCurrentUser();
            this.service.Logout();

            return $"User {currentUser.Username} successfully logged out!";
        }
    }
}
