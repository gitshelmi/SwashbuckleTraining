using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SwaggerTraining.DTOs
{
    public class ProjectModificationBaseDto : IValidatableObject
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(150)]
        public virtual string Description { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext _)
        {
            if (Name == Description)
            {
                yield return new ValidationResult(
                    "The provided name and description should be different.");
            }
        }
    }
}
