using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Services.Communication;

namespace AssignmentManager.Server.Services
{
    public interface IInstructorService
    {
        Task<List<Instructor>> GetAllInstructors();
        Task<InstructorResponse> GetById(int id);
        Task<InstructorResponse> SaveAsync(Instructor instructor);
        Task<InstructorResponse> UpdateAsync(int id, Instructor instructor);
        Task<InstructorResponse> DeleteAsync(int id);
    }
}