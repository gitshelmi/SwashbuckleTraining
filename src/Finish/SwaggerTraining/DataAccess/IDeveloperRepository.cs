using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SwaggerTraining.Models;

namespace SwaggerTraining.DataAccess
{
    public interface IDeveloperRepository
    {
        Task<IEnumerable<Developer>> GetDevelopersAsync();
        Task<Developer> GetDeveloperAsync(Guid developerId);
        Task AddDeveloperAsync(Developer developer);
        Task<bool> DeveloperExists(Guid developerId);
    }
}
