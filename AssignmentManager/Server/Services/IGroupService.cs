using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Services.Communication;
using AssignmentManager.Shared;

namespace AssignmentManager.Server.Services
{
    public interface IGroupService
    {
        Task<List<Group>> GetAll();
        Task<Group> GetById(int id);
        Task<Group> Create(Group item);
        Task<Group> Update(int id, Group item);
        Task<Group> DeleteById(int id);
    }
}