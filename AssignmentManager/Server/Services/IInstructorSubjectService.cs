using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Services.Communication;

namespace AssignmentManager.Server.Services
{
    public interface IInstructorSubjectService
    {
        Task<List<InstructorSubject>> GetAllInstructorSubjects();
        Task<InstructorSubjectResponse> GetById(int id);
        Task<InstructorSubjectResponse> AddAsync(InstructorSubject instructor);
        Task<InstructorSubjectResponse> UpdateAsync(int id, InstructorSubject instructor);
        Task<InstructorSubjectResponse> DeleteAsync(int id);
    }
}