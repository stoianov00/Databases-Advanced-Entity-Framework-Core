using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using Newtonsoft.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Instagraph.Data;
using Instagraph.Models;
using Instagraph.DataProcessor.DtoModels;

namespace Instagraph.DataProcessor
{
    public class Deserializer
    {
        private static string ErrorMsg = "Error: Invalid data.";
        private static string SuccessMsg = "Successfully imported {0}.";

        public static string ImportPictures(InstagraphContext context, string jsonString)
        {
            var deserializedPictures = JsonConvert.DeserializeObject<Picture[]>(jsonString);

            var sb = new StringBuilder();

            var pictures = new List<Picture>();

            foreach (var picture in deserializedPictures)
            {
                bool isValid = !string.IsNullOrWhiteSpace(picture.Path) && picture.Size > 0;

                bool pictureExists = context.Pictures.Any(p => p.Path == picture.Path)
                    || pictures.Any(p => p.Path == picture.Path);

                if (!isValid || pictureExists)
                {
                    sb.AppendLine(ErrorMsg);
                    continue;
                }

                pictures.Add(picture);

                sb.AppendLine(string.Format(SuccessMsg, $"Picture {picture.Path}"));
            }

            context.Pictures.AddRange(pictures);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportUsers(InstagraphContext context, string jsonString)
        {
            var deserializedUsers = JsonConvert.DeserializeObject<UserDto[]>(jsonString);

            var sb = new StringBuilder();

            var users = new List<User>();

            foreach (var userDto in deserializedUsers)
            {
                bool isValid = !string.IsNullOrWhiteSpace(userDto.Username)
                    && userDto.Username.Length <= 30
                    && userDto.Password.Length <= 20
                    && !string.IsNullOrWhiteSpace(userDto.Password)
                    && !string.IsNullOrWhiteSpace(userDto.ProfilePicture);

                var picture = context.Pictures.FirstOrDefault(p => p.Path == userDto.ProfilePicture);

                bool pictureExists = context.Pictures.Any(p => p.Path == userDto.ProfilePicture);

                bool userExists = users.Any(u => u.Username == userDto.Username);

                if (!isValid || picture == null || userExists)
                {
                    sb.AppendLine(ErrorMsg);
                    continue;
                }

                var user = Mapper.Map<User>(userDto);
                user.ProfilePicture = picture;

                users.Add(user);

                sb.AppendLine(string.Format(SuccessMsg, $"User {user.Username}"));
            }

            context.Users.AddRange(users);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportFollowers(InstagraphContext context, string jsonString)
        {
            var deserializedFollowers = JsonConvert.DeserializeObject<UserFollowerDto[]>(jsonString);

            var sb = new StringBuilder();

            var followers = new List<UserFollower>();

            foreach (var dto in deserializedFollowers)
            {
                int? userId = context.Users.FirstOrDefault(u => u.Username == dto.User)?.Id;
                int? followerId = context.Users.FirstOrDefault(u => u.Username == dto.Follower)?.Id;

                if (userId == null || followerId == null)
                {
                    sb.AppendLine(ErrorMsg);
                    continue;
                }

                bool alreadyFollowed = followers.Any(f => f.UserId == userId && f.FollowerId == followerId);
                if (alreadyFollowed)
                {
                    sb.AppendLine(ErrorMsg);
                    continue;
                }

                var userFollower = new UserFollower(userId.Value, followerId.Value);

                followers.Add(userFollower);
                sb.AppendLine(string.Format(SuccessMsg, $"Follower {dto.Follower} to User {dto.User}"));
            }

            context.UserFollowers.AddRange(followers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {
            var xDoc = XDocument.Parse(xmlString);
            var elements = xDoc.Root.Elements();

            var sb = new StringBuilder();

            var posts = new List<Post>();

            foreach (var element in elements)
            {
                string caption = element.Element("caption")?.Value;
                string username = element.Element("user")?.Value;
                string picturePath = element.Element("picture")?.Value;

                bool inputIsValid = !string.IsNullOrWhiteSpace(caption)
                    && !string.IsNullOrWhiteSpace(username)
                    && !string.IsNullOrWhiteSpace(picturePath);

                if (!inputIsValid)
                {
                    sb.AppendLine(ErrorMsg);
                    continue;
                }

                int? userId = context.Users.FirstOrDefault(u => u.Username == username)?.Id;
                int? pictureId = context.Pictures.FirstOrDefault(u => u.Path == picturePath)?.Id;

                if (userId == null || pictureId == null)
                {
                    sb.AppendLine(ErrorMsg);
                    continue;
                }

                var post = new Post(caption, userId.Value, pictureId.Value);

                posts.Add(post);
                sb.AppendLine(string.Format(SuccessMsg, $"Post {caption}"));

            }

            context.Posts.AddRange(posts);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            var xDoc = XDocument.Parse(xmlString);
            var elements = xDoc.Root.Elements();

            var sb = new StringBuilder();

            var comments = new List<Comment>();

            foreach (var element in elements)
            {
                string content = element.Element("content")?.Value;
                string userName = element.Element("user")?.Value;
                string postIdString = element.Element("post")?.Attribute("id")?.Value;

                bool inputIsValid = !string.IsNullOrWhiteSpace(content)
                     && !string.IsNullOrWhiteSpace(userName)
                     && !string.IsNullOrWhiteSpace(postIdString);

                if (!inputIsValid)
                {
                    sb.AppendLine(ErrorMsg);
                    continue;
                }

                int postId = int.Parse(postIdString);
                int? userId = context.Users.FirstOrDefault(u => u.Username == userName)?.Id;

                bool postExists = context.Posts.Any(p => p.Id == postId);
                if (userId == null || !postExists)
                {
                    sb.AppendLine(ErrorMsg);
                    continue;
                }

                var comment = new Comment(content, userId.Value, postId);
                comments.Add(comment);

                sb.AppendLine(string.Format(SuccessMsg, $"Comment {comment}"));
            }

            context.Comments.AddRange(comments);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }
    }
}