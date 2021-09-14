using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SwaggerTraining.Models
{
    public class Developer
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public ICollection<Project> Projects { get; set; }
            = new List<Project>();
    }
}
