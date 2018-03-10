namespace PhotoShare.Client.Core.Commands
{
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class AddFriendCommand
    {
        // AddFriend <username1> <username2>
        public static string Execute(string[] data)
        {
            string requesterUsername = data[1];
            string addedFriendUsername = data[2];

            using (var context = new PhotoShareContext())
            {
                var requestingUser = context.Users
                    .Include(u => u.FriendsAdded)
                    .ThenInclude(fa => fa.Friend)
                    .FirstOrDefault(u => u.Username == requesterUsername);

                if (requestingUser == null)
                {
                    throw new ArgumentException($"{requesterUsername} not found!");
                }

                var addedFriend = context.Users
                    .Include(u => u.FriendsAdded)
                    .ThenInclude(fa => fa.Friend)
                    .FirstOrDefault(u => u.Username == addedFriendUsername);

                if (addedFriend == null)
                {
                    throw new ArgumentException($"{addedFriendUsername} not found!");
                }

                bool alreadyAdded = requestingUser.FriendsAdded.Any(u => u.Friend == addedFriend);
                bool accepted = addedFriend.FriendsAdded.Any(u => u.Friend == requestingUser);

                if (alreadyAdded && !accepted)
                {
                    throw new InvalidOperationException("Friend request already sent!");
                }

                if (alreadyAdded && accepted)
                {
                    throw new InvalidOperationException($"{addedFriendUsername} is already a friend to {requesterUsername}");
                }

                if (!alreadyAdded && accepted)
                {
                    throw new InvalidOperationException($"{requesterUsername} has already received a friend request from {addedFriendUsername}");
                }

                requestingUser.FriendsAdded.Add(new Friendship(requestingUser, addedFriend));

                context.SaveChanges();
            }

            return $"Friend {addedFriendUsername} added to {requesterUsername}!";

        }
    }
}
