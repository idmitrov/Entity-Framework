using System.Collections.Generic;
using Phonebook.models;

namespace Phonebook.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Phonebook.Data.PhonebookDb>
    {
        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(PhonebookDb db)
        {
            db.Contacts.AddOrUpdate(c => c.Name,
            new Contact()
            {
                Name = "Petar Ivanov",
                Position = "CTO",
                Company = "Smart Ideas",
                Emails = new HashSet<Email>()
                {
                    new Email() { EmailAddress = "peter@gmail.com" },
                    new Email() { EmailAddress = "peter_ivanov@yahoo.com" }
                },
                Phones = new HashSet<Phone>()
                {
                    new Phone() { PhoneNumber = "+359 2 22 22 22" },
                    new Phone() { PhoneNumber = "+359 88 77 88 99" }
                },
                Url = "http://blog.peter.com",
                Notes = "Friend from school"
            },
            new Contact()
            {
                Name = "Maria",
                Phones = new HashSet<Phone>() { new Phone() { PhoneNumber = "+359 22 33 44 55" } }
            },
            new Contact()
            {
                Name = "Angie Stanton",
                Emails = new HashSet<Email>() { new Email() { EmailAddress = "Email: info@angiestanton.com" } },
                Url = "http://angiestanton.com"
            });

            db.SaveChanges();
        }
    }
}
