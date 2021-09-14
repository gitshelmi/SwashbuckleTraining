using System;
using System.ComponentModel.DataAnnotations;

namespace SwaggerTraining.DTOs
{
    public class DeveloperDto
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        [Required]
        public int Age { get; set; }
    }
}
