using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SwaggerTraining.Models;

namespace SwaggerTraining.DataAccess
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetDeveloperProjectsAsync(Guid developerId);
        Task<Project> GetDeveloperProjectAsync(Guid developerId, Guid projectId);
        Task<Project> AddDeveloperProjectAsync(Project project);
        Task UpdateProjectAsync();
    }
}
