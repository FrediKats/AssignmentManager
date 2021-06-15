using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Services.Communication;

namespace AssignmentManager.Server.Services
{
    public interface IGroupService
    {
        Task<List<Group>> GetAll();
        Task<Group> GetById(int id);
        Task<SaveGroupResponse> Create(Group item);
        Task<Group> Update(Group item);
        Group DeleteById(int id);
    }
}