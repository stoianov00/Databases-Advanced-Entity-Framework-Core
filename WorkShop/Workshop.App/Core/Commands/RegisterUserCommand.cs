using System;
using Workshop.App.Interfaces;
using Workshop.Data.DTOs;
using Workshop.Models.Enums;
using Workshop.Services;

namespace Workshop.App.Core.Commands
{
    internal class RegisterUserCommand : ICommand
    {
        private readonly UserService service;

        public RegisterUserCommand(UserService service)
        {
            this.service = service;
        }

        // RegisterUser <username> <password> <repeat-password> <firstName> <lastName> <age> <gender>
        public string Execute(params string[] args)
        {
            string username = args[0];
            string password = args[1];
            string repeatPassword = args[2];
            string firstName = args[3];
            string lastName = args[4];
            int age = int.Parse(args[5]);
            Gender gender;
            bool isValidGender = Enum.TryParse(args[6], out gender);

            var dto = new UserDto(username, password, firstName, lastName, age, gender);

            if (!this.service.IsUsernameValid(dto))
            {
                throw new ArgumentException($"Username {username} not valid!");
            }

            if (this.service.IsPasswordValid(password))
            {
                throw new ArgumentException($"Password {password} is not valid!");
            }

            if (age <= 0)
            {
                throw new ArgumentException("Age not valid!");
            }

            if (password != repeatPassword)
            {
                throw new InvalidOperationException($"Passwords do not match!");
            }

            if (this.service.IsUserExist(username))
            {
                throw new InvalidOperationException($"Username {username} is already taken!");
            }

            if (!isValidGender)
            {
                throw new ArgumentException($"Gender should be \"Male\" or \"Female\"!");
            }

            this.service.RegisterUser(dto);

            return $"User {username} was registered successfully!";
        }
    }
}
