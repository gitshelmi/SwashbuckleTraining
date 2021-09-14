using System;
using System.ComponentModel.DataAnnotations;

namespace SwaggerTraining.DTOs.V2
{
    public class DeveloperDtoV2
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string BirthDate { get; set; }
    }
}
