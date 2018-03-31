using Workshop.Models.Enums;

namespace Workshop.Data.DTOs
{
    public class UserDto
    {
        public UserDto()
        {

        }

        public UserDto(string username, string password, string firstName, string lastName, int age, Gender gender)
        {
            this.Username = username;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
            this.Gender = gender;
        }

        public UserDto(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public UserDto(string username, string password, Gender gender)
            : this(username, password)
        {
            this.Gender = gender;
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }
    }
}
