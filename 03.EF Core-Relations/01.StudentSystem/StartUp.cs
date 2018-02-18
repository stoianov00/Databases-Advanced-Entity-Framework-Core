using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using _01.StudentSystem.Data;
using _01.StudentSystem.Data.Data.Models;
using _01.StudentSystem.Data.Data.Models.Enums;

namespace _01.StudentSystem
{
    public class StartUp
    {
        public static void Main()
        {
            var context = new StudentSystemContext();

            ResetDatabase(context);
        }

        private static void ResetDatabase(StudentSystemContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Database.Migrate();

            Seed(context);
        }

        private static void Seed(StudentSystemContext context)
        {
            var students = new[]
            {
                new Student()
                {
                    Name = "Dave",
                    PhoneNumber = "08945123",
                    RegisteredOn = new DateTime(2012 / 3 / 23),
                    Birthday = new DateTime(1980 / 2 / 2),
                    HomeworkSubmissions = new List<Homework>(),
                    CourseEnrollments = new List<StudentCourse>()
                },

                new Student()
                {
                    Name = "Mercedes",
                    PhoneNumber = "08945375",
                    RegisteredOn = new DateTime(2011 / 5 / 3),
                    Birthday = new DateTime(1974 / 1 / 23),
                    HomeworkSubmissions = new List<Homework>(),
                    CourseEnrollments = new List<StudentCourse>()
                }
            };

            context.Students.AddRange(students);

            var homework = new[]
            {
                new Homework()
                {
                    Content = "Algebra",
                    ContentType = ContentType.Zip,
                    CourseId = 1,
                    StudentId = 1,
                    SubmissionTime = new DateTime(2016 / 9 / 14)
                },

                new Homework()
                {
                    Content = "PHP",
                    ContentType = ContentType.Application,
                    CourseId = 2,
                    StudentId = 2,
                    SubmissionTime = new DateTime(2017 / 11 / 29)
                }
            };

            context.HomeworkSubmissions.AddRange(homework);

            var courses = new[]
            {
                new Course()
                {
                    Name = "Math",
                    Description = "Math",
                    StartDate = new DateTime(2016 / 3 / 6),
                    EndDate = new DateTime(2016 / 9 / 14),
                    Price = 90,
                    Resources = new List<Resource>(),
                    HomeworkSubmissions = new List<Homework>(),
                    StudentsEnrolled = new List<StudentCourse>()
                },

                new Course()
                {
                    Name = "PHP",
                    Description = "PHP",
                    StartDate = new DateTime(2010 / 3 / 6),
                    EndDate = new DateTime(2013 / 9 / 14),
                    Price = 150,
                    Resources = new List<Resource>(),
                    HomeworkSubmissions = new List<Homework>(),
                    StudentsEnrolled = new List<StudentCourse>()
                },
            };

            context.Courses.AddRange(courses);

            var resources = new[]
            {
                new Resource()
                {
                    Name = "Math solver",
                    Url = "www.google.com",
                    ResourceType = ResourceType.Document,
                    CourseId = 1,
                    Course = new Course()
                },

                new Resource()
                {
                    Name = "PHP",
                    Url = "www.php.com",
                    ResourceType = ResourceType.Other,
                    CourseId = 2,
                    Course = new Course()
                },
            };

            context.Resources.AddRange(resources);

            context.SaveChanges();
        }

    }
}
