using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Persistence;
using AssignmentManager.Server.Persistence.Contexts;
using AssignmentManager.Server.Services.Communication;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManager.Server.Services
{
    public class GroupService : BaseService, IGroupService
    {
        public GroupService(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Group>> GetAll()
        {
            //TODO: fix $id, $values
            return await _context.Groups
                .Include(p => p.Speciality).ToListAsync();
        }

        public async Task<Group> GetById(int id)
        {
            return await _context.Groups.FindAsync(id);
        }

        public async Task<SaveGroupResponse> Create(Group item)
        {
            try
            {
                await _context.Groups.AddAsync(item);
                await _context.SaveChangesAsync();
                return new SaveGroupResponse(item);
            }
            catch (Exception er)
            {
                return new SaveGroupResponse(er.Message);
            }
        }

        public Task<Group> Update(Group item)
        {
            throw new NotImplementedException();
        }

        public Group DeleteById(int id)
        {
            throw new NotImplementedException();
        }
    }
}