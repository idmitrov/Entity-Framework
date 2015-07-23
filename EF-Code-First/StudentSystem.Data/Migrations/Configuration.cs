using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace StudentSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentSystem.Data.StudentSystemDb>
    {
        public Configuration()
        {
            //this.AutomaticMigrationDataLossAllowed = true; //MIGRATE WITH DATA LOSES
            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = "StudentSystem.Data.StudentSystemDb";
        }

        protected override void Seed(StudentSystem.Data.StudentSystemDb db)
        {
            // INIT STUDENTS
            db.Students.AddOrUpdate
            (
                u => u.Name,
                new Student() { Name = "Gosho", RegistredOn = DateTime.Now },
                new Student() { Name = "Misho", RegistredOn = DateTime.Now, PhoneNumber = "0899-00-11-22" }
            );

            // INIT COURSES
            db.Courses.AddOrUpdate
            (
                c => c.Name,
                new Course()
                {
                    Name = "C# Basics",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(30),
                    Price = 125,
                    Description = "CSharp Basics"
                },
                new Course()
                {
                    Name = "Java Basics",
                    StartDate = DateTime.Now.AddDays(30),
                    EndDate = DateTime.Now.AddDays(60),
                    Price = 100,
                    Description = "Java Basics",
                }
            );

            // INIT RESOURCES
            db.Resources.AddOrUpdate
            (r => r.Name,
                new Resource()
                { CourseId = 0, Name = "C# Classwork taks", Type = ResourceType.Document, Url = "www.example.com/1/1/1" },
                new Resource()
                { CourseId = 0, Name = "C# Presentation", Type = ResourceType.Presentation, Url = "www.example.com/1/1/3" },
                new Resource()
                { CourseId = 0, Name = "C# Lab", Type = ResourceType.Video, Url = "www.example.com/1/1/4" },
                new Resource()
                { CourseId = 0, Name = "C# Camera video", Type = ResourceType.Video, Url = "www.example.com/1/1/2" },
                new Resource()
                { CourseId = 0, Name = "C# Exam Preparation", Type = ResourceType.Document, Url = "www.example.com/1/1/4" },
                new Resource()
                { CourseId = 0, Name = "C# Teamwork taks", Type = ResourceType.Document, Url = "www.example.com/1/1/5" },
                new Resource()
                { CourseId = 1, Name = "Java Camera video", Type = ResourceType.Video, Url = "www.example.com/1/2/1" },
                new Resource()
                { CourseId = 1, Name = "Java Screencast video", Type = ResourceType.Video, Url = "www.example.com/1/2/2" }
            );


            // INIT HOMEWORK SUBMISSIONS
            db.Homeworks.AddOrUpdate
            (
                h => h.Content,
                new Homework()
                { Content = "Example content", Type = ContentType.Zip, SubmissionDate = DateTime.Now, StudentId = 1 },
                new Homework()
                { Content = "Example content2", Type = ContentType.Zip, SubmissionDate = DateTime.Now, StudentId = 2 },
                new Homework()
                { Content = "Example content3", Type = ContentType.Zip, SubmissionDate = DateTime.Now, StudentId = 3 },
                new Homework()
                { Content = "Example content4", Type = ContentType.Zip, SubmissionDate = DateTime.Now, StudentId = 4 }
            );

            // INIT LICENSES
            db.Licenses.AddOrUpdate(
                l => l.Name,
                new License { Name = "Source MIT License" },
                new License { Name = "Minisoft License" },
                new License { Name = "Example Presentation License" },
                new License { Name = "University License" },
                new License { Name = "Example License" },
                new License { Name = "Another License" }
            );

            // SAVE CHANGES
            db.SaveChanges();
        }
    }
}
