using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;

namespace AssignmentManager.Server.Services
{
    public interface ISolutionService
    {
        Task<List<Solution>> GetAll();
        Task<Solution> GetById(int id);
        Task<Solution> Create(Solution item);
        Task<Solution> Update(int id, Solution item);
        Task<Solution> DeleteById(int id);
    }
}