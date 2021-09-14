using System;

namespace SwaggerTraining.DTOs
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid DeveloperId { get; set; }
    }
}
