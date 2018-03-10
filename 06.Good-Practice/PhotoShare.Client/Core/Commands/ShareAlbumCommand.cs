namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class ShareAlbumCommand
    {
        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer 
        public static string Execute(string[] data)
        {
            var album = GetAlbumById(int.Parse(data[1]));
            var user = GetUserByUsername(data[2]);
            var permission = ParsePermission(data[3]);

            using (var context = new PhotoShareContext())
            {
                context.AlbumRoles.Attach(new AlbumRole(user, album, permission));
                context.SaveChanges();
            }

            return $"Username {user.Username} added to album {album.Name} ({permission.ToString()})";
        }

        private static Role ParsePermission(string name)
        {
            var isRoleValid = Enum.TryParse(name, true, out Role role);
            if (!isRoleValid)
            {
                throw new ArgumentException("Permission must be either “Owner” or “Viewer”!");
            }

            return role;
        }

        private static User GetUserByUsername(string username)
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

        private static Album GetAlbumById(int id)
        {
            Album album;
            using (var context = new PhotoShareContext())
            {
                album = context.Albums
                   .FirstOrDefault(a => a.Id == id);

                if (album == null)
                {
                    throw new ArgumentException($"Album {id} not found!");
                }
            }

            return album;
        }

    }
}
