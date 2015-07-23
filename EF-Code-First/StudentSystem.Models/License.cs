namespace StudentSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class License
    {
        private ICollection<Resource> resources;

        public License()
        {
            this.resources = new HashSet<Resource>();
        } 

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Resource> Resources
        {
            get { return this.resources; }
            set { this.resources = value; }
        }
    }
}
