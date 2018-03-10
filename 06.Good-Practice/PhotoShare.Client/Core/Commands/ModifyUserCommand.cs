namespace PhotoShare.Client.Core.Commands
{
    using Data;
    using System;
    using System.Linq;

    public class ModifyUserCommand
    {
        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public static string Execute(string[] data)
        {
            string username = data[1];
            string property = data[2].ToLower();
            string value = data[3];

            using (var context = new PhotoShareContext())
            {
                var user = context.Users
                    .Where(u => u.Username == username)
                    .FirstOrDefault();

                if (user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                string exceptionMsg = $"Value {value} not valid." + Environment.NewLine;

                switch (property)
                {
                    case "password":
                        if (!value.Any(c => char.IsLower(c)) || !value.Any(c => char.IsDigit(c)))
                        {
                            throw new ArgumentException(exceptionMsg + "Invalid Password!");
                        }

                        user.Password = value;
                        break;

                    case "borntown":
                        var bornTown = context.Towns
                            .Where(bt => bt.Name == value)
                            .FirstOrDefault();

                        if (bornTown == null)
                        {
                            throw new ArgumentException(exceptionMsg + $"Town {value} not found!");
                        }

                        user.BornTown = bornTown;
                        break;

                    case "currenttown":
                        var currentTown = context.Towns
                            .Where(ct => ct.Name == value)
                            .FirstOrDefault();

                        if (currentTown == null)
                        {
                            throw new ArgumentException(exceptionMsg + $"Town {value} not found!");
                        }

                        user.CurrentTown = currentTown;
                        break;

                    default:
                        throw new ArgumentException($"Property {property} not supported!");
                }

                context.SaveChanges();
            }

            return $"User {username} {property} is {value}.";
        }
    }
}