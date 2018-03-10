namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using System;
    using System.Linq;

    public class PrintFriendsListCommand
    {
        // PrintFriendsList <username>
        public static string Execute(string[] data)
        {
            string username = data[1];

            var context = new PhotoShareContext();

            var user = context.Users
              .Select(u => new
              {
                  u.Username,
                  AddedAsFriendBy = u.AddedAsFriendBy
                      .Select(f => f.User.Username)
                      .ToArray(),
                  FriendsAdded = u.FriendsAdded
                      .Select(f => f.Friend.Username)
                      .ToArray()
              })
              .FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            var friends = user.AddedAsFriendBy
                .Concat(user.FriendsAdded);

            return friends.Any()
                ? $"Friends: {Environment.NewLine}- " + string.Join($"{Environment.NewLine}- ", friends)
                : "No friends for this user. :(";

        }
    }
}
