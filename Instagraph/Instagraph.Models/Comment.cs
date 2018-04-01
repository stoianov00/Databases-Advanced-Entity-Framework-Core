namespace Instagraph.Models
{
    public class Comment
    {
        public Comment()
        {

        }

        public Comment(string content, int userId, int postId)
        {
            this.Content = content;
            this.UserId = userId;
            this.PostId = postId;
        }

        public int Id { get; set; }

        public string Content { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }
    }
}