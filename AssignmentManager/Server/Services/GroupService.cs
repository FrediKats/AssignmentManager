using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<SaveGroupResponse> Create(Group group)
        {
            int specialityId = group.SpecialityId.HasValue ? group.SpecialityId.Value : -1;
            if (specialityId > 0) 
            { 
                var speciality = _context.Specialities.FindAsync(specialityId).Result; 
                if (speciality == null) 
                { 
                    return new SaveGroupResponse($"Speciality with id={specialityId} is not existed"); 
                }
                group.Speciality = speciality;
                speciality.Groups ??= new List<Group>();
                speciality.Groups.Add(group);
            }
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();
            return new SaveGroupResponse(group);

            }

        public async Task<List<Group>> GetBySpecialityId(int? id)
        {
            return await _context.Groups
                .Include(gr => gr.Speciality)
                .Where(gr => gr.SpecialityId == id).ToListAsync();
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