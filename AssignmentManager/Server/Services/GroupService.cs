using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Persistence;
using AssignmentManager.Server.Persistence.Contexts;
using AssignmentManager.Server.Services.Communication;
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
            try
            {
                var currentGroup = await _context.Groups
                    .Include(g => g.Speciality)
                    .Include(g => g.Students)
                    .FirstOrDefaultAsync(g => g.Id == id);
                if (currentGroup == null)
                {
                    throw new Exception("Group isn't existed");
                }
                return currentGroup;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred when getting by id the group: {ex.Message}");
            }
        }
        
        public async Task<Group> Create(Group group)
        {
            try
            {
                group.Speciality = await _context.Specialities.FindAsync(group.SpecialityId);
                if (group.Speciality == null)
                {
                    throw new Exception($"Speciality with id {group.SpecialityId} is not existed");
                }

                await _context.Groups.AddAsync(group);
                await _context.SaveChangesAsync();
                return group;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred when creating the group: {ex.Message}");
            }
        }
        public async Task<Group> Update(int id, Group item)
        {
            var existedGroup = await GetById(id);
            existedGroup.Name = item.Name;
            existedGroup.SpecialityId = item.SpecialityId;
            try
            {
                existedGroup.Speciality = await _context.Specialities.FindAsync(item.SpecialityId);
                _context.Groups.Update(existedGroup);
                await _context.SaveChangesAsync();
                return existedGroup;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred when updating the group: {ex.Message}");
            }
        }

        public async Task<Group> DeleteById(int id)
        {
            var existedGroup = await GetById(id);
            try
            {
                _context.Remove(existedGroup);
                await _context.SaveChangesAsync();
                return existedGroup;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred when deleting the group: {ex.Message}");
            }
        }
    }
}