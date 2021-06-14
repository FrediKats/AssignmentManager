using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Services.Communication;

namespace AssignmentManager.Server.Services
{
    public interface IGroupService
    {
        Task<List<Group>> GetAllAsync();
        Task<Group> GetByIdAsync(int id);
        Task<SaveGroupResponse> CreateAsync(Group item);
        Task<Group> UpdateAsync(Group item);
        Group DeleteByIdAsync(int id);
    }
}