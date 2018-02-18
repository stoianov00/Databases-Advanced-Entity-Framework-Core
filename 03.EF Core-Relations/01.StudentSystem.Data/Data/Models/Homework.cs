using System;
using _01.StudentSystem.Data.Data.Models.Enums;

namespace _01.StudentSystem.Data.Data.Models
{
    public class Homework
    {
        public int HomeworkId { get; set; }

        public string Content { get; set; }

        public ContentType ContentType { get; set; }

        public DateTime SubmissionTime { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; } 
    }
}