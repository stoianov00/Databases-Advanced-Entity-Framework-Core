namespace PhotoShare.Models
{
    public class AlbumTag
    {
        public AlbumTag()
        {

        }

        public AlbumTag(Album album, Tag tag)
        {
            this.Album = album;
            this.Tag = tag;
        }

        public int AlbumId { get; set; }
        public Album Album { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
