using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Shared;

namespace AssignmentManager.Server.Services
{
    public interface IAssignmentService
    {
        Task<List<Assignment>> GetAll();
        Task<Assignment> GetById(int id);
        Task<Assignment> Create(SaveAssignmentResource item);
        Task<Assignment> Update(int id, SaveAssignmentResource item);
        Task<Assignment> DeleteById(int id);
    }
}