namespace StudentSystem.Data
{
    using System.Data.Entity;
    using Models;
    using Migrations;

    public class StudentSystemDb : DbContext
    {
        public StudentSystemDb()
            : base("StudentSystemDb")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentSystemDb, Configuration>());
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Homework> Homeworks { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<License> Licenses { get; set; }
    }
}