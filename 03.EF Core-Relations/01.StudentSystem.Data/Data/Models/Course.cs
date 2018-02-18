using System;
using System.Collections.Generic;

namespace _01.StudentSystem.Data.Data.Models
{
    public class Course
    {
        public Course()
        {
            this.HomeworkSubmissions = new List<Homework>();
            this.StudentsEnrolled = new List<StudentCourse>();
            this.Resources = new List<Resource>();
        }

        public int CourseId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        public ICollection<Resource> Resources { get; set; }

        public ICollection<Homework> HomeworkSubmissions { get; set; }

        public ICollection<StudentCourse> StudentsEnrolled { get; set; }
    }
}