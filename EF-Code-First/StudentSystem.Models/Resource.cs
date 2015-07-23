namespace StudentSystem.Models
{
    using System.Collections.Generic;

    public class Resource
    {
        private ICollection<License> licenses;

        public Resource()
        {
            this.licenses = new HashSet<License>();
        }

        // ID
        public int Id { get; set; }

        // NAME
        public string Name { get; set; }

        // TYPE OF RESOURCE (video / presentation / document / other)
        public ResourceType Type { get; set; }

        // URL
        public string Url { get; set; }

        // FK -> RESOURCE -> COURSE
        public int CourseId { get; set; }

        // COURSE
        public virtual Course Course { get; set; }

        // LICENSES
        public virtual ICollection<License> Licenses
        {
            get { return this.licenses; }
            set { this.licenses = value; }
        }
    }
}
