using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Persistence;
using AssignmentManager.Server.Persistence.Contexts;
using AssignmentManager.Shared;
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
            return await _context.Groups.ToListAsync();
        }

        public async Task<Group> GetById(int id)
        {
            var currentGroup = await _context.Groups
                .Include(g => g.Speciality)
                .Include(g => g.Students)
                .FirstOrDefaultAsync(g => g.Id == id); 
            if (currentGroup == null)
            {
                throw new NullReferenceException( GetErrorString($"a group with id {id} does not exist"));
            }
            return currentGroup;
        }

        public async Task<Group> Create(Group group)
        {
            group.Speciality = await _context.Specialities.FindAsync(group.SpecialityId);
            if (group.Speciality == null)
            {
                throw new NullReferenceException(GetErrorString($"speciality with id {group.SpecialityId} is not existed"));
            }

            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();
            return group;
        }


        //TODO: SaveGroupResource
        public async Task<Group> Update(int id, Group item)
        {
            var existedGroup = await GetById(id);
            existedGroup.Name = item.Name;
            existedGroup.SpecialityId = item.SpecialityId;
            existedGroup.Speciality = await _context.Specialities.FindAsync(item.SpecialityId);
            if (existedGroup.Speciality == null)
            {
                throw new NullReferenceException(GetErrorString($"speciality with id {existedGroup.SpecialityId} is not existed"));
            }
            _context.Groups.Update(existedGroup);
            await _context.SaveChangesAsync();
            return existedGroup;
        }

        public async Task<Group> DeleteById(int id)
        {
            var existedGroup = await GetById(id);
            _context.Remove(existedGroup);
            await _context.SaveChangesAsync();
            return existedGroup;
        }
    }
}