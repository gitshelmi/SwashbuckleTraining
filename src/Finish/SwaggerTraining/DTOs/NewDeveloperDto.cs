using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SwaggerTraining.DTOs
{
    public class NewDeveloperDto
    {
        [Required]
        [MaxLength(50)]
        [DefaultValue(null)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]

        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}
