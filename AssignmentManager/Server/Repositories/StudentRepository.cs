using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Persistence.Contexts;
using AssignmentManager.Server.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManager.Server.Repositories
{
    public class StudentRepository : BaseRepository, IStudentRepository
    {
        public StudentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Student>> ListAsync()
        {
            return await _context.Students.ToListAsync();
        }
    }
}