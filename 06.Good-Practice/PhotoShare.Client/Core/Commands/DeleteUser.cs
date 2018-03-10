namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Data;

    public class DeleteUser
    {
        // DeleteUser <username>
        public static string Execute(string[] data)
        {
            string username = data[1];

            using (PhotoShareContext context = new PhotoShareContext())
            {
                var user = context.Users
                    .Where(u => u.Username == username)
                    .FirstOrDefault();

                if (user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                if (user.IsDeleted.Value)
                {
                    throw new InvalidOperationException($"User {username} is already deleted!");
                }

                user.IsDeleted = true;
                context.SaveChanges();
            }

            return $"User {username} was deleted successfully!";
        }
    }
}
