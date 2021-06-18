using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Services.Communication;

namespace AssignmentManager.Server.Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetAll();
        StudentResponse GetById(int id);
        Task<StudentResponse> Create(Student item);
        Task<StudentResponse> Update(int id, Student item);
        Task<StudentResponse> DeleteById(int id);
    }
}