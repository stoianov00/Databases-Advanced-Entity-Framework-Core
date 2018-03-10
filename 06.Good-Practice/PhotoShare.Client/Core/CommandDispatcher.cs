namespace PhotoShare.Client.Core
{
    using System;
    using Commands;
    using PhotoShare.Services;

    public class CommandDispatcher
    {
        public string DispatchCommand(string[] commandParameters)
        {
            string command = commandParameters[0].ToLower();
            string result = default(string);

            switch (command)
            {
                case "registeruser":
                    result = RegisterUserCommand.Execute(commandParameters);
                    break;

                case "addtown":
                    result = AddTownCommand.Execute(commandParameters);
                    break;

                case "modifyuser":
                    result = ModifyUserCommand.Execute(commandParameters);
                    break;

                case "deleteuser":
                    result = DeleteUser.Execute(commandParameters);
                    break;

                case "addtag":
                    result = AddTagCommand.Execute(commandParameters);
                    break;

                case "createalbum":
                    result = CreateAlbumCommand.Execute(commandParameters);
                    break;

                case "makefriends":
                    result = AddFriendCommand.Execute(commandParameters);
                    break;

                case "addtagto":
                    result = AddTagToCommand.Execute(commandParameters);
                    break;

                case "acceptfriend":
                    result = AcceptFriendCommand.Execute(commandParameters);
                    break;

                case "listfriends":
                    result = PrintFriendsListCommand.Execute(commandParameters);
                    break;

                case "sharealbum":
                    result = ShareAlbumCommand.Execute(commandParameters);
                    break;

                case "uploadpicture":
                    result = UploadPictureCommand.Execute(commandParameters);
                    break;

                case "login":
                    var authenticationService = new AuthenticationService();
                    var userService = new UserService();

                    var login = new LoginCommand(authenticationService, userService);
                    result = login.Execute(commandParameters);
                    break;

                case "logout":
                    authenticationService = new AuthenticationService();
                    userService = new UserService();

                    var logOut = new LogoutCommand(authenticationService, userService);
                    result = logOut.Execute();
                    break;

                case "exit":
                    result = ExitCommand.Execute();
                    break;

                default:
                    throw new InvalidOperationException($"Command {command} not valid!");
            }

            return result;
        }
    }
}
