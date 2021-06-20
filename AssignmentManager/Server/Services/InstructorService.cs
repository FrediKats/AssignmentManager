using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Persistence;
using AssignmentManager.Server.Persistence.Contexts;
using AssignmentManager.Server.Services.Communication;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManager.Server.Services
{
    public class InstructorService : BaseService,  IInstructorService
    {
        public InstructorService(AppDbContext context) : base(context)
        {
        }
        public Task<List<Instructor>> GetAllInstructors()
        {
            return _context.Instructors.ToListAsync();
        }

        public async Task<InstructorsResponse> SaveAsync(Instructor instructor)
        {
            try
            {
                await _context.Instructors.AddAsync(instructor);
                await _context.SaveChangesAsync();
                return new InstructorsResponse(instructor);
            }
            catch (Exception er)
            {
                return new InstructorsResponse(er.Message);
            }
        }
    }
}