﻿using System.Collections.Generic;

namespace Instagraph.Models
{
    public class Post
    {
        public Post()
        {
            this.Comments = new HashSet<Comment>();
        }

        public Post(string caption, int userId, int pictureId)
        {
            this.Caption = caption;
            this.UserId = userId;
            this.PictureId = pictureId;
        }

        public int Id { get; set; }

        public string Caption { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int PictureId { get; set; }

        public Picture Picture { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}