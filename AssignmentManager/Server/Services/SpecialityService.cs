using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentManager.Server.Extensions;
using AssignmentManager.Server.Mapping;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Persistence;
using AssignmentManager.Server.Persistence.Contexts;
using AssignmentManager.Server.Services.Communication;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManager.Server.Services
{
    public class SpecialityService : BaseService, ISpecialityService
    {
        public SpecialityService(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Speciality>> GetAll()
        {
            return await _context.Specialities.ToListAsync();
        }

        public async Task<SpecialityResponse> GetById(int id)
        {
            try
            {
                var currentSpeciality = await _context.Specialities.FindAsync(id);
                currentSpeciality.Groups = await _context.Groups
                    .Where(g => g.SpecialityId == id).ToListAsync();
                foreach (var gr in currentSpeciality.Groups)
                {
                    var studentsInGroup = await _context.Students
                        .Include(student => student.Group)
                        .Where(student => student.GroupId == gr.Id)
                        .ToListAsync();
                    gr.Students = studentsInGroup;
                }
                return new SpecialityResponse(currentSpeciality);
            }
            catch (Exception ex)
            {
                return new SpecialityResponse($"An error occurred when getting by id the speciality: {ex.Message}");
            }
            
        }
        public async Task<SpecialityResponse> Create(Speciality item)
        {
            try
            {
                //validate input value of EStudyType
                item.EnumStudyType.ToDescriptionString();
                await _context.Specialities.AddAsync(item);
                await _context.SaveChangesAsync();
                return new SpecialityResponse(item);
            }
            catch (Exception ex)
            {
                return new SpecialityResponse($"An error occurred when creating the speciality: {ex.Message}");
            }
        }

        public async Task<SpecialityResponse> Update(int id, Speciality item)
        {
            var existedSpec = await _context.Specialities.FindAsync(id);
            if (existedSpec == null)
            {
                return new SpecialityResponse("Speciality not found");
            }
            existedSpec.Code = item.Code;
            existedSpec.EnumStudyType = item.EnumStudyType;
            try
            {
                _context.Specialities.Update(existedSpec);
                await _context.SaveChangesAsync();
                return new SpecialityResponse(existedSpec);
            }
            catch (Exception ex)
            {
                return new SpecialityResponse($"An error occurred when updating the speciality: {ex.Message}");
            }
        }

        public async Task<SpecialityResponse> DeleteById(int id)
        {
            var existedSpec = _context.Specialities.Include(s=> s.Groups).FirstOrDefault(p => p.Id == id);
            if (existedSpec == null)
            {
                return new SpecialityResponse("Speciality not found");
            }
            try
            {
                _context.Remove(existedSpec);
                await _context.SaveChangesAsync();

                return new SpecialityResponse(existedSpec);
            }
            catch (Exception ex)
            {
                return new SpecialityResponse($"An error occurred when deleting the speciality: {ex.Message}");
            }
        }

        public async Task<SpecialityResponse> DeleteCascadeById(int id)
        {
            var existedSpec = _context.Specialities
                .Include(s => s.Groups)
                .FirstOrDefault(p => p.Id == id);
            if (existedSpec == null)
            {
                return new SpecialityResponse("Speciality not found");
            }

            var groups = _context.Groups.Where(g => g.Speciality == existedSpec);
            List<int?> groupsIds = new List<int?>();
            foreach (var g in groups)
            {
                groupsIds.Add(g.Id);
            }
            try
            {
                var studentsToDelete = await _context.Students
                    .Where(s => groupsIds.Contains(s.GroupId)).ToListAsync();
                _context.Students.RemoveRange(studentsToDelete);
                _context.Groups.RemoveRange(groups);
                _context.Specialities.Remove(existedSpec);
                await _context.SaveChangesAsync();

                return new SpecialityResponse(existedSpec);
            }
            catch (Exception ex)
            {
                return new SpecialityResponse(
                    $"An error occurred when cascade deleting the speciality: {ex.Message}");
            }
        }
    }
}