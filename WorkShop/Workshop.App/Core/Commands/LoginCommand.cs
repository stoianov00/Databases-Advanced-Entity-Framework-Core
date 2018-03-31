using System;
using Workshop.App.Interfaces;
using Workshop.Services;

namespace Workshop.App.Core.Commands
{
    internal class LoginCommand : ICommand
    {
        private readonly UserService service;

        public LoginCommand(UserService service)
        {
            this.service = service;
        }

        // Login <username> <password>
        public string Execute(params string[] args)
        {
            string username = args[0];
            string password = args[1];

            if (!this.service.IsUserExist(username))
            {
                throw new InvalidOperationException("Invalid username or password!");
            }

            if (!this.service.IsPasswordExist(password))
            {
                throw new InvalidOperationException("Invalid username or password!");
            }

            if (this.service.IsAuthenticated())
            {
                var currentUser = this.service.GetCurrentUser();

                if (currentUser.Username == username)
                {
                    throw new ArgumentException("You should logout first!");
                }

                throw new InvalidOperationException("Invalid credentials");
            }

            this.service.Login(username, password);

            return $"User {username} successfully logged in!";
        }
    }
}
