using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;

namespace AssignmentManager.Server.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> ListAsync();
    }
}