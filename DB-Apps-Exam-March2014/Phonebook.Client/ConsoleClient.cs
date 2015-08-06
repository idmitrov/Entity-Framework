using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using Phonebook.models;

namespace Phonebook.Client
{
    using Data;

    class ConsoleClient
    {
        // SET PROP METHOD
        private static void ContactSetProperty(Contact contact, string propertyName, string propertyValue)
        {
            if (!string.IsNullOrWhiteSpace(propertyValue))
            {
                switch (propertyName.ToLower())
                {
                    case "name": contact.Name = propertyValue; break;
                    case "company": contact.Company = propertyValue; break;
                    case "position": contact.Position = propertyValue; break;
                    case "site": contact.Url = propertyValue; break;
                    case "notes": contact.Notes = propertyValue; break;
                    case "lastname": contact.Name += " " + propertyValue; break;
                }
            }
        }

        // SET PROP COLLECTION METHOD
        private static void ContactSetCollectionPropery(Contact contact,
            string collectionName, JToken collection)
        {
            if (collection != null)
            {
                foreach (var entry in collection)
                {
                    switch (collectionName.ToLower())
                    {
                        case "emails": contact.Emails.Add(new Email() { EmailAddress = entry.ToString() }); break;
                        case "phones": contact.Phones.Add(new Phone() { PhoneNumber = entry.ToString() }); break;
                        default: Console.WriteLine("Unimplemented collection property {0}", collectionName); break;
                    }
                }
            }
        }

        // MAIN
        static void Main()
        {
            using (var db = new PhonebookDb())
            {
                // Problem 06
                var dbContacts = db.Contacts.Select(c => new
                {
                    c.Name,
                    c.Emails,
                    c.Phones
                });

                foreach (var contact in dbContacts)
                {
                    var contactName = contact.Name;
                    var contactPhones = string.Join(", ", contact.Phones.Select(p => p.PhoneNumber));
                    var contactEmails = string.Join(", ", contact.Emails.Select(e => e.EmailAddress));

                    Console.WriteLine("Name: {0}\r\nPhone(s): {1}\r\nEmail(s): {2}\r\n",
                        contactName,
                        contactPhones == "" ? "no data" : contactPhones,
                        contactEmails == "" ? "no data" : contactEmails);
                }

                // Problem 07
                var jsonAsText = System.IO.File.ReadAllText("../../../contacts.json");

                var contacts = JArray.Parse(jsonAsText);

                foreach (var contact in contacts)
                {
                    // CONTACT TO CREATE
                    var newlyContact = new Contact();
                    
                    // REQUIRED PROPERTY
                    var contactName = contact["name"]?.ToString() ?? "";
                    
                    // REQUIRED PROPERTY CHECK
                    if (string.IsNullOrWhiteSpace(contactName))
                    {
                        Console.WriteLine("Error: {0} is required", contactName);
                        continue;
                    }
                    
                    // SET REQUIRED PROPERTY TO CONTACT
                    ContactSetProperty(newlyContact, "name", contactName);

                    // NOT REQUIRED PROPS
                    var company = contact["company"]?.ToString() ?? "";
                    var position = contact["position"]?.ToString() ?? "";
                    var site = contact["site"]?.ToString() ?? "";
                    var notes = contact["notes"]?.ToString() ?? "";
                    var emails = contact["emails"];
                    var phones = contact["phones"];

                    // SET NOT REQUIRED PROPS IF EXIST
                    ContactSetProperty(newlyContact, "company", company);
                    ContactSetProperty(newlyContact, "position", position);
                    ContactSetProperty(newlyContact, "site", site);
                    ContactSetProperty(newlyContact, "notes", notes);
                    ContactSetCollectionPropery(newlyContact, "emails", emails);
                    ContactSetCollectionPropery(newlyContact, "phones", phones);

                    // CONTACT IMPORTED
                    db.Contacts.Add(newlyContact);
                    db.SaveChanges();
                    Console.WriteLine("Contact {0} imported", contactName);
                }
            }
        }
    }
}
