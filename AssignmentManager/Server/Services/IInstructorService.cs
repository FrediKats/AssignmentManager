using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Services.Communication;

namespace AssignmentManager.Server.Services
{
    public interface IInstructorService
    {
        Task<List<Instructor>> GetAllInstructors();
        Task<InstructorsResponse> SaveAsync(Instructor instructor);
    }
}