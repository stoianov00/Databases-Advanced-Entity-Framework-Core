using PhotoShare.Services;
using System;

namespace PhotoShare.Client.Core.Commands
{
    public class LoginCommand
    {
        private readonly AuthenticationService authenticationService;
        private readonly UserService userService;

        public LoginCommand(AuthenticationService authenticationService, UserService userService)
        {
            this.authenticationService = authenticationService;
            this.userService = userService;
        }

        // Login <username> <password>
        public string Execute(string[] data)
        {
            string username = data[1];
            string password = data[2];

            if (!this.userService.UserExists(username))
            {
                throw new ArgumentException($"Invalid username or password!");
            }

            if (!this.userService.CheckPassword(username, password))
            {
                throw new ArgumentException($"Invalid username or password!");
            }

            if (AuthenticationService.IsAuthenticated())
            {
                var currentUser = AuthenticationService.GetCurrentUser();

                if (currentUser.Username == username)
                {
                    throw new ArgumentException($"You should logout first!");
                }

                throw new InvalidOperationException("Invalid credentials");
            }

            AuthenticationService.Login(username, password);

            return $"User {username} successfully logged in!";
        }

    }
}
