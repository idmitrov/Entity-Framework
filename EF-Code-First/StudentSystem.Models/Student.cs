namespace StudentSystem.Models
{
    using System;
    using System.Collections.Generic;

    public class Student
    {
        // COURSES FIELD
        private ICollection<Course> courses;

        // COURSES INIT
        public Student()
        {
            this.courses = new HashSet<Course>();           
        } 
         
        // ID
        public int Id { get; set; } 

        // Name
        public string Name { get; set; }

        // PHONE NUMBER (OPT)
        public string PhoneNumber { get; set; }

        // REGISTRATION DATE
        public DateTime RegistredOn { get; set; }

        // BIRTHDAY (OPT)
        public DateTime? BirthDay { get; set; }

        // STUDENTS CA BE IN MANY COURSES
        public virtual ICollection<Course> Courses
        {
            get { return this.courses; }
            set { this.courses = value; }
        }
    }
}
