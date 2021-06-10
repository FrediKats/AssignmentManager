using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Persistence.Contexts;
using AssignmentManager.Server.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManager.Server.Repositories
{
    public class GroupRepository: BaseRepository, IGroupRepository
    {
        public GroupRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Group>> ListAsync()
        {
            return await _context.Groups.ToListAsync();
        }
        
    }
}