using PhotoShare.Data;
using System.Linq;

namespace PhotoShare.Services
{
    public class UserService
    {
        public bool UserExists(string username)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Users.Any(u => u.Username == username);
            }
        }

        public bool CheckPassword(string username, string password)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == username);

                if (user.Password == password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}