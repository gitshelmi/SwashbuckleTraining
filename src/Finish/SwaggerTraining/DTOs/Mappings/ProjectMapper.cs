using AutoMapper;
using SwaggerTraining.Models;

namespace SwaggerTraining.DTOs.Mappings
{
    public class ProjectMapper : Profile
    {
        public ProjectMapper()
        {
            CreateMap<ProjectDto, Project>();
            CreateMap<Project, ProjectDto>();
            CreateMap<NewProjectDto, Project>();
            CreateMap<UpdateProjectDto, Project>();
        }
    }
}
