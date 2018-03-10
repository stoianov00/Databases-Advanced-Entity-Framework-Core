namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class AddTagToCommand
    {
        // AddTagTo <albumName> <tag>
        public static string Execute(string[] data)
        {
            var album = GetAlbum(data[1]);
            var tag = GetTag(data[2]);

            using (var context = new PhotoShareContext())
            {
                context.AlbumTags.Attach(new AlbumTag(album, tag));
                context.SaveChanges();
            }

            return $"Tag {tag} added to {album.Name}!";
        }

        private static Album GetAlbum(string albumName)
        {
            Album album;
            using (var context = new PhotoShareContext())
            {
                album = context.Albums
                    .FirstOrDefault(a => a.Name == albumName);

                if (album == null)
                {
                    throw new ArgumentException("Either tag or album do not exist!");
                }
            }

            return album;
        }

        private static Tag GetTag(string tagName)
        {
            Tag tag;
            using (var context = new PhotoShareContext())
            {
                if (!tagName.StartsWith('#'))
                {
                    tagName = $"#{tagName}";
                }

                tag = context.Tags
                   .FirstOrDefault(t => t.Name == tagName);
                
                if (tag == null)
                {
                    throw new ArgumentException("Either tag or album do not exist!");
                }
            }

            return tag;
        }
    }
}
