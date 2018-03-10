namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class CreateAlbumCommand
    {
        private const Role UserRole = Role.Owner;

        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public static string Execute(params string[] data)
        {
            var user = GetUser(data[1]);
            var albumTitle = data[2];
            CheckIfAlbumExist(albumTitle);
            var bgColor = ParseColor(data[3]);

            var tags = GetTags(data
                .Skip(4)
                .ToArray());

            bool DefaultIsPubluc = true;
            var album = new Album(albumTitle, DefaultIsPubluc, bgColor);
            user.AlbumRoles.Add(new AlbumRole(user, album, UserRole));

            using (var context = new PhotoShareContext())
            {
                foreach (var tag in tags)
                {
                    context.AlbumTags.Add(new AlbumTag(album, tag));
                }

                context.SaveChanges();
            }

            return $"Album {albumTitle} successfully created!";
        }

        private static User GetUser(string username)
        {
            User user;
            using (var context = new PhotoShareContext())
            {
                user = context.Users
                    .FirstOrDefault(u => u.Username == username);

                if (user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }
            }

            return user;
        }

        private static Color ParseColor(string bgColor)
        {
            var isColorAvailable = Enum.TryParse(bgColor, true, out Color color);

            if (!isColorAvailable)
            {
                throw new ArgumentException($"Color {bgColor} not found!");
            }

            return color;
        }

        private static void CheckIfAlbumExist(string albumTitle)
        {
            using (var context = new PhotoShareContext())
            {
                var availableAlbum = context.Albums
                .FirstOrDefault(a => a.Name == albumTitle);

                if (availableAlbum != null)
                {
                    throw new ArgumentException($"Album {albumTitle} exists!");
                }
            }
        }

        private static Tag[] GetTags(string[] tagNames)
        {
            if (tagNames.Length < 1)
            {
                throw new ArgumentException("Invalid tags!");
            }

            var tags = new Tag[tagNames.Length];
            using (var context = new PhotoShareContext())
            {
                for (int i = 0; i < tagNames.Length; i++)
                {
                    var currentTag = context.Tags
                    .FirstOrDefault(t => t.Name == tagNames[i]);

                    if (currentTag == null)
                    {
                        currentTag = new Tag(tagNames[i]);
                    }

                    tags[i] = currentTag;
                }
            }

            return tags;
        }
    }
}
