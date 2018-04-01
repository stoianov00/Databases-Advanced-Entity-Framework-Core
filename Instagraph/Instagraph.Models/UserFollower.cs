namespace Instagraph.Models
{
    public class UserFollower
    {
        public UserFollower()
        {

        }

        public UserFollower(int userId, int followerId)
        {
            this.UserId = userId;
            this.FollowerId = followerId;
        }

        public int UserId { get; set; }

        public User User { get; set; }

        public int FollowerId { get; set; }

        public User Follower { get; set; }
    }
}