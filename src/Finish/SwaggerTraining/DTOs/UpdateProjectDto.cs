using System.ComponentModel.DataAnnotations;

namespace SwaggerTraining.DTOs
{
    public class UpdateProjectDto : ProjectModificationBaseDto
    {
        // make the description mandatory for update (but not for creation)
        [Required]
        public override string Description
        {
            get => base.Description;
            set => base.Description = value;
        }
    }
}
