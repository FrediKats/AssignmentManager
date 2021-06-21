using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Persistence;
using AssignmentManager.Server.Persistence.Contexts;
using AssignmentManager.Server.Services.Communication;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManager.Server.Services
{
    public class InstructorSubjectService : BaseService, IInstructorSubjectService
    {
        public InstructorSubjectService(AppDbContext context) : base(context)
        {
        }
        
        public async Task<List<InstructorSubject>> GetAllInstructorSubjects()
        {
            return await _context.InstructorSubjects.ToListAsync();
        }

        public async Task<InstructorSubjectResponse> GetById(int id)
        {
            return new InstructorSubjectResponse(await _context.InstructorSubjects.FindAsync(id));
        }

        public async Task<InstructorSubjectResponse> SaveAsync(InstructorSubject instructor)
        {
            throw new System.NotImplementedException();
        }

        public async Task<InstructorSubjectResponse> UpdateAsync(int id, InstructorSubject instructor)
        {
            throw new System.NotImplementedException();
        }

        public async Task<InstructorSubjectResponse> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}