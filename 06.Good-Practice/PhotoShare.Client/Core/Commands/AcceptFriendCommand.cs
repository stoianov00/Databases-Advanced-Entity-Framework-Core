namespace PhotoShare.Client.Core.Commands
{
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class AcceptFriendCommand
    {
        // AcceptFriend <username1> <username2>
        public static string Execute(string[] data)
        {
            var acceptingUser = GetDetachedUser(data[1]);
            var suggestingUser = GetDetachedUser(data[2]);
            var friendship = GetFriendship(acceptingUser, suggestingUser);

            using (var context = new PhotoShareContext())
            {
                friendship.IsAccepted = true;
                context.SaveChanges();
            }

            return $"{acceptingUser.Username} accepted {suggestingUser.Username} as a friend";
        }

        private static Friendship GetFriendship(User acceptingUser, User suggestingUser)
        {
            Friendship friendship;
            using (var context = new PhotoShareContext())
            {
                friendship = context.Friendships
                     .FirstOrDefault(f => f.User.Id == suggestingUser.Id && f.Friend.Id == acceptingUser.Id);
            }

            if (friendship == null)
            {
                throw new InvalidOperationException($"{suggestingUser.Username} has not added {acceptingUser.Username} as a friend");
            }

            if (friendship.IsAccepted)
            {
                throw new InvalidOperationException($"{suggestingUser.Username} is already a friend to {acceptingUser.Username}");
            }

            return friendship;
        }

        private static User GetDetachedUser(string username)
        {
            User user;
            using (var context = new PhotoShareContext())
            {
                user = context.Users
                    .AsNoTracking()
                    .FirstOrDefault(u => u.Username == username);

                if (user == null)
                {
                    throw new ArgumentException($"{username} not found!");
                }
            }

            return user;
        }

    }
}
