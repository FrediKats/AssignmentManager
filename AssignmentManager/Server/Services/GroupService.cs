using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Repositories;

namespace AssignmentManager.Server.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
        public async Task<IEnumerable<Group>> ListAsync()
        {
            return await _groupRepository.ListAsync();
        }
    }
}