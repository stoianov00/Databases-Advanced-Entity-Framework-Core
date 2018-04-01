using System;

namespace Instagraph.DataProcessor.DtoModels
{
    public class UserDto
    {
        private string username;
        private string password;
        private string profilePicture;

        public string Username
        {
            get => this.username;
            set => this.username = value;
        }

        public string Password
        {
            get => this.password;
            set => this.password = value;
        }

        public string ProfilePicture
        {
            get => this.profilePicture;
            set => this.profilePicture = value;
        }

    }
}
