using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;

namespace AssignmentManager.Server.Services
{
    public interface IGroupService
    {
        Task<IEnumerable<Group>> ListAsync();
    }
}