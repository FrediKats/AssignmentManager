using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Services.Communication;

namespace AssignmentManager.Server.Services
{
    public interface IInstructorService
    {
        Task<List<Instructor>> GetAllInstructors();
        Task<InstructorsResponse> GetById(int id);
        Task<InstructorsResponse> SaveAsync(Instructor instructor);
        Task<InstructorsResponse> UpdateAsync(int id, Instructor instructor);
    }
}