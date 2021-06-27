using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Shared;

namespace AssignmentManager.Server.Services
{
    public interface IStudentService
    {
        Task<List<StudentResourceBriefly>> GetAll();
        Task<StudentResource> GetById(int id);
        Task<StudentResource> Create(SaveStudentResource item);
        Task<StudentResource> Update(int id, SaveStudentResource item);
        Task<StudentResource> DeleteById(int id);
    }
}