using AutoMapper;
using System.Linq;
using Workshop.Data;
using Workshop.Data.DTOs;
using Workshop.Models;

namespace Workshop.Services
{
    public class UserService
    {
        private readonly TeamBuilderDbContext context;
        private static User loggedUser;

        public UserService(TeamBuilderDbContext context)
        {
            this.context = context;
        }

        public UserDto FindUserById(int id)
        {
            var user = this.context.Users
                .Find(id);

            var dto = Mapper.Map<UserDto>(user);

            return dto;
        }

        public UserDto FindUserByUsername(string username)
        {
            var user = this.context.Users.FirstOrDefault(u => u.Username == username);

            var dto = Mapper.Map<UserDto>(user);

            return dto;
        }

        public void RegisterUser(UserDto dto)
        {
            var user = Mapper.Map<User>(dto);

            this.context.Users.Add(user);
            this.context.SaveChanges();
        }

        public bool IsUsernameValid(UserDto dto)
        {
            return dto.Username.Length >= 3 && dto.Username.Length <= 25 ? true : false;
        }

        public bool IsUserExist(string username)
        {
            return this.context.Users.Any(u => u.Username == username);
        }

        public bool IsPasswordExist(string password)
        {
            return this.context.Users.Any(u => u.Password == password);
        }

        public bool IsPasswordValid(string password)
        {
            if (password.Length < 6 || password.Length > 30 || !password.Any(char.IsDigit) || !password.Any(char.IsUpper))
            {
                return true;
            }

            return false;
        }

        public void Login(string username, string password)
        {
            var user = this.context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            loggedUser = user;
        }

        public void Logout()
        {
            this.IsAuthenticated();
            loggedUser = null;
        }

        public bool IsAuthenticated()
        {
            return loggedUser != null;
        }

        public User GetCurrentUser()
        {
            this.IsAuthenticated();
            return loggedUser;
        }

        public void DeleteUser(User user)
        {
            this.context.Users.FirstOrDefault(u => u.Id == user.Id).IsDeleted = true;
            this.context.SaveChanges();
        }

        public void KickMember(string username, string teamName)
        {
            var userToBeKicked = this.context.Users.SingleOrDefault(u => u.Username == username);
            var team = this.context.Teams.SingleOrDefault(t => t.Name == teamName);
            var userTeam = this.context.UserTeams.SingleOrDefault(ut => ut.Team == team && ut.User == userToBeKicked);

            this.context.UserTeams.Remove(userTeam);
            this.context.SaveChanges();
        }

    }
}