using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SwaggerTraining.Models;

namespace SwaggerTraining.DataAccess
{
    public class DeveloperRepository : IDeveloperRepository, IDisposable
    {
        private readonly DatabaseContext _context;

        public DeveloperRepository(DatabaseContext context)
        {
            _context = context ??
                       throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Developer>> GetDevelopersAsync()
        {
            return await _context.Developers.ToListAsync();
        }

        public async Task<Developer> GetDeveloperAsync(Guid developerId)
        {
            if (developerId == null)
            {
                throw new ArgumentException(nameof(developerId));
            }

            return await _context.Developers
                .FirstOrDefaultAsync(developer => developer.Id == developerId);
        }

        public async Task AddDeveloperAsync(Developer developer)
        {
            if (developer == null)
            {
                throw new ArgumentNullException(nameof(developer));
            }

            await _context.Developers.AddAsync(developer);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeveloperExists(Guid developerId)
        {
            if (developerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(developerId));
            }

            return await _context.Developers
                .AnyAsync(developer => developer.Id == developerId);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
