using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;

namespace AssignmentManager.Server.Services
{
    public interface IAssignmentService
    {
        Task<List<Assignment>> GetAll();
        Task<Assignment> GetById(int id);
        Task<Assignment> Create(Assignment item);
        Task<Assignment> Update(int id, Assignment item);
        Task<Assignment> DeleteById(int id);
    }
}