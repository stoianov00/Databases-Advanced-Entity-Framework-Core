namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;
     
    public class UploadPictureCommand
    {
        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public static string Execute(string[] data)
        {
            var album = GetAlbumByName(data[1]);
            string title = data[2];
            string path = data[3];

            using (var context = new PhotoShareContext())
            {
                context.Pictures.Attach(new Picture(title, path, album));
                context.SaveChanges();
            }

            return $"Picture {title} added to {album.Name}!";
        }

        private static Album GetAlbumByName(string name)
        {
            Album album;
            using (var context = new PhotoShareContext())
            {
                album = context.Albums
                   .FirstOrDefault(a => a.Name == name);

                if (album == null)
                {
                    throw new ArgumentException($"Album {name} not found!");
                }
            }

            return album;
        }
    }
}
