using System;
using Workshop.App.Interfaces;
using Workshop.Services;

namespace Workshop.App.Core.Commands
{
    internal class DeleteUserCommand : ICommand
    {
        private readonly UserService service;

        public DeleteUserCommand(UserService service)
        {
            this.service = service;
        }

        public string Execute(params string[] args)
        {
            if (!this.service.IsAuthenticated())
            {
                throw new InvalidOperationException("You should login first!");
            }

            var loggedUser = this.service.GetCurrentUser();
            string username = loggedUser.Username;

            this.service.DeleteUser(loggedUser);

            this.service.Logout();

            return $"User {username} was deleted successfully!";
        }
    }
}
