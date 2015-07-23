
using System.Collections.Generic;

namespace StudentSystem.Models
{
    using System;

    public class Course
    {
        // STUDENTS FIELD
        private ICollection<Student> students;

        // RESOURCES FIELD
        private ICollection<Resource> resources;
        
        // SUBMISSIONS FIELD
        private ICollection<Homework> submissions; 

        // STUDENTS INIT
        // RESOURCE INIT
        // SUBMISSIONS INIT
        public Course()
        {
            this.students = new HashSet<Student>();
            this.resources = new HashSet<Resource>();
            this.submissions = new HashSet<Homework>();
        } 

        // ID
        public int Id { get; set; }

        // NAME
        public string Name { get; set; }

        // DESCRIPTION (OPT)
        public string Description { get; set; }

        // START DATE
        public DateTime StartDate { get; set; }

        // END DATE
        public DateTime EndDate { get; set; }

        // PRICE
        public decimal Price { get; set; }

        // COURSES CAN HAVE MANY STUDENTS
        public virtual ICollection<Student> Students
        {
            get { return this.students; } set { this.students = value; }
        }

        // COURSES CAN HAVE MANY RESOURCES
        public virtual ICollection<Resource> Resources
        {
            get { return this.resources; }
            set { this.resources = value; }
        }

        // ONE COURSE CAN HAVE MANY HOMEWORK SUBMISSIONS
        public virtual ICollection<Homework> Submissions
        {
            get { return this.submissions; }
            set { this.submissions = value; }
        }
    }
}
