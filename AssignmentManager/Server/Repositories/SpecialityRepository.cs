using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Persistence.Contexts;
using AssignmentManager.Server.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManager.Server.Repositories
{
    public class SpecialityRepository : BaseRepository, ISpecialityRepository
    {
        public SpecialityRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Speciality>> ListAsync()
        {
            return await _context.Specialities.ToListAsync();
        }

        public async Task AddAsync(Speciality speciality)
        {
            await _context.Specialities.AddAsync(speciality);
        }
    }
}