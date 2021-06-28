using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Services.Communication;

namespace AssignmentManager.Server.Services
{
    public interface IInstructorService
    {
        Task<List<Instructor>> GetAllInstructors();
        Task<Instructor> GetById(int id);
        Task<Instructor> AddAsync(Instructor instructor);
        Task<Instructor> UpdateAsync(int id, Instructor instructor);
        Task<Instructor> DeleteAsync(int id);
    }
}