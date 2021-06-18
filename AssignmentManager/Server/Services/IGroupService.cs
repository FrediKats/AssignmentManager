using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Services.Communication;

namespace AssignmentManager.Server.Services
{
    public interface IGroupService
    {
        Task<List<Group>> GetAll();
        Task<GroupResponse> GetById(int id);
        Task<GroupResponse> Create(Group item);
        Task<List<Group>> GetById(int? id);
        Task<GroupResponse> Update(int id, Group item);
        Task<GroupResponse> DeleteById(int id);
        Task<GroupResponse> DeleteCascadeById(int id);
    }
}