namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Models;
    using Data;
    using System.Linq;

    public class RegisterUserCommand
    {
        // RegisterUser <username> <password> <repeat-password> <email>
        public static string Execute(string[] data)
        {
            string username = data[1];
            string password = data[2];
            string repeatPassword = data[3];
            string email = data[4];

            using (PhotoShareContext context = new PhotoShareContext())
            {
                if (context.Users.Any(u => u.Username == username))
                {
                    throw new InvalidOperationException($"Username {username} is already taken!");
                }

                if (password != repeatPassword)
                {
                    throw new ArgumentException("Passwords do not match!");
                }

                var user = new User(username, password, email, false, DateTime.Now, DateTime.Now);

                context.Users.Add(user);
                context.SaveChanges();
            }

            return $"User {username} was registered successfully!";
        }
    }
}
