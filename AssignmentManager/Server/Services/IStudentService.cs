using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Services.Communication;

namespace AssignmentManager.Server.Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(int id);
        Task<SaveStudentResponse> CreateAsync(Student item);
        void UpdateAsync(Student item);
        Student DeleteByIdAsync(int id);
    }
}