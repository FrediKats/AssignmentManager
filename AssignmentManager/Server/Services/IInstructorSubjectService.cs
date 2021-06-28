using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Services.Communication;

namespace AssignmentManager.Server.Services
{
    public interface IInstructorSubjectService
    {
        Task<List<InstructorSubject>> GetAllInstructorSubjects();
        Task<InstructorSubject> GetById(int id);
        Task<InstructorSubject> AddAsync(InstructorSubject instructor);
        Task<InstructorSubject> UpdateAsync(int id, InstructorSubject instructor);
        Task<InstructorSubject> DeleteAsync(int id);
    }
}