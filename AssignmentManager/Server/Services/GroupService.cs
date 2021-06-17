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
            return await _context.Groups.ToListAsync();
        }

        public async Task<GroupResponse> GetById(int id)
        {
            try
            {
                var currentGroup = _context.Groups.Include(g => g.Speciality)
                    .First(g => g.Id == id);
                currentGroup.Students = await _context.Students
                    .Where(g => g.GroupId == id).ToListAsync();
                return new GroupResponse(currentGroup);
            }
            catch (Exception ex)
            {
                return new GroupResponse($"An error occurred when getting by id the speciality: {ex.Message}");
            }
        }

        public async Task<GroupResponse> Create(Group group)
        {
            int specialityId = group.SpecialityId.HasValue ? group.SpecialityId.Value : -1;
            if (specialityId > 0) 
            { 
                var speciality = _context.Specialities.FindAsync(specialityId).Result; 
                if (speciality == null) 
                { 
                    return new GroupResponse($"Speciality with id={specialityId} is not existed"); 
                }
                group.Speciality = speciality;
                speciality.Groups ??= new List<Group>();
                speciality.Groups.Add(group);
            }
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();
            return new GroupResponse(group);

            }

        public async Task<List<Group>> GetById(int? id)
        {
            return await _context.Groups
                .Include(gr => gr.Speciality)
                .Where(gr => gr.SpecialityId == id).ToListAsync();
        }
        public async Task<GroupResponse> Update(int id, Group item)
        {
            var existedGroup = await _context.Groups
                .FindAsync(id);
            if (existedGroup == null)
            {
                return new GroupResponse("Group not found");
            }
            existedGroup.Name = item.Name;
            existedGroup.SpecialityId = item.SpecialityId;
            try
            {
                var existedSpeciality = await _context.Specialities.FindAsync(existedGroup.SpecialityId);
                _context.Groups.Update(existedGroup);
                await _context.SaveChangesAsync();
                existedGroup.Speciality = existedSpeciality;
                return new GroupResponse(existedGroup);
            }
            catch (Exception ex)
            {
                return new GroupResponse($"An error occurred when updating the group: {ex.Message}");
            }
        }

        public async Task<GroupResponse> DeleteById(int id)
        {
            var existedGroup = _context.Groups
                .Include(s=> s.Students)
                .FirstOrDefault(p => p.Id == id);
            if (existedGroup == null)
            {
                return new GroupResponse("Group not found");
            }
            try
            {
                _context.Remove(existedGroup);
                await _context.SaveChangesAsync();

                return new GroupResponse(existedGroup);
            }
            catch (Exception ex)
            {
                return new GroupResponse($"An error occurred when deleting the group: {ex.Message}");
            }
        }
        
        public async Task<GroupResponse> DeleteCascadeById(int id)
        {
            var existedGroup = _context.Groups
                .Include(s => s.Students)
                .FirstOrDefault(p => p.Id == id);
            if (existedGroup == null)
            {
                return new GroupResponse("Speciality not found");
            }

            var students = _context.Students.Where(g => g.Group == existedGroup);
            List<int?> studentIds = new List<int?>();
            foreach (var g in students)
            {
                studentIds.Add(g.IsuId);
            }
            try
            {
                var studentsToDelete = await _context.Students
                    .Where(s => studentIds.Contains(s.IsuId)).ToListAsync();
                _context.Students.RemoveRange(studentsToDelete);
                _context.Groups.Remove(existedGroup);
                await _context.SaveChangesAsync();

                return new GroupResponse(existedGroup);
            }
            catch (Exception ex)
            {
                return new GroupResponse(
                    $"An error occurred when cascade deleting the speciality: {ex.Message}");
            }
        }
    }
}