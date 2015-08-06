namespace Phonebook.Data
{
    using System.Data.Entity;
    using Migrations;
    using models;

    public class PhonebookDb : DbContext
    {
        public PhonebookDb() : base("name=PhonebookDb")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PhonebookDb ,Configuration>());
        }

        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Phone> Phones { get; set; }
        public virtual DbSet<Email> Emails { get; set; }
    }
}