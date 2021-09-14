using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SwaggerTraining.Models;

namespace SwaggerTraining.DataAccess
{
    public class ProjectRepository : IProjectRepository, IDisposable
    {
        private readonly DatabaseContext _context;

        public ProjectRepository(DatabaseContext context)
        {
            _context = context ??
                       throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Project>> GetDeveloperProjectsAsync(Guid developerId)
        {
            if (developerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(developerId));
            }

            return await _context.Projects
                .Where(project => project.Developer.Id == developerId)
                .OrderBy(project => project.Name)
                .ToListAsync();
        }

        public async Task<Project> GetDeveloperProjectAsync(Guid developerId, Guid projectId)
        {
            if (developerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(developerId));
            }

            if (projectId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(projectId));
            }

            return await _context.Projects
                .FirstOrDefaultAsync(project =>
                    project.Developer.Id == developerId &&
                    project.Id == projectId);
        }

        public async Task<Project> AddDeveloperProjectAsync(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return project;
        }

        public async Task UpdateProjectAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
