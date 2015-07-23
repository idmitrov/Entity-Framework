using System;

namespace StudentSystem.Models
{
    public class Homework
    {
        // ID
        public int Id { get; set; } 

        // CONTENT
        public string Content { get; set; } 

        // CONTENT TYPE (e.g. application/pdf or application/zip)
        public ContentType Type { get; set; }

        // SUBMISSION DATE
        public DateTime SubmissionDate { get; set; }

        // FK HOMEWORK -> COURSE
        public int CourseId { get; set; }

        // FK HOMEWORK -> STUDENT
        public int StudentId { get; set; }

        // HOMEWORK SUBMISSIONS HAVE A STUDENT
        public Student Student { get; set; }

        // HOMEWORK SUBMISSIONS HAVE A COURSE
        public Course Course { get; set; }
    }
}
