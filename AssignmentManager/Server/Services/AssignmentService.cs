using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Persistence;
using AssignmentManager.Server.Persistence.Contexts;
using AssignmentManager.Shared;

namespace AssignmentManager.Server.Services
{
    public class AssignmentService : BaseService, IAssignmentService
    {
        public AssignmentService(AppDbContext context) : base(context) { }
        
        public async Task<List<Assignment>> GetAll()
        {
            return await _context.Assignments.ToListAsync();
        }

        public async Task<Assignment> GetById(int id)
        {
            var assignment = await _context.Assignments
                .Include(asgn => asgn.Subject)
                .Include(asgn => asgn.Solutions)
                .FirstOrDefaultAsync(asgn => asgn.AssignmentId == id);
            if (assignment == null)
            {
                throw new Exception("An error occurred while getting an assignment: an assignment with id ${id} does not exist");
            }
            
            return assignment;
        }

        public Task<Assignment> Create(Assignment item)
        {
            throw new System.NotImplementedException();
        }

        public Task<Assignment> Update(int id, Assignment item)
        {
            throw new System.NotImplementedException();
        }

        public Task<Assignment> DeleteById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}