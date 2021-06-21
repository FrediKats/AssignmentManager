using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentManager.Server.Extensions;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Persistence;
using AssignmentManager.Server.Persistence.Contexts;
using AssignmentManager.Server.Services.Communication;
using AssignmentManager.Shared;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManager.Server.Services
{
    public class SpecialityService : BaseService, ISpecialityService
    {
        public SpecialityService(AppDbContext context) : base(context)
        {
        }

        private async Task<Speciality> GetSpecialityWithGroups(int id)
        {
            return await _context.Specialities
                .Include(s=> s.Groups)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        private string GetAllEnumValues<T>(T enumType) where T : Type
        {
            var vars = new List<byte>();
            foreach (var en in Enum.GetValues(enumType))
            {
                vars.Add((byte)en);
            }
            var enumStudyTypeValues = string
                .Join(
                    ',',
                    vars
                );
            return enumStudyTypeValues;
        }
        public async Task<List<Speciality>> GetAll()
        {
            return await _context.Specialities.ToListAsync();
        }

        public async Task<SpecialityResponse> GetById(int id)
        {
            try
            {
                var currentSpeciality = await GetSpecialityWithGroups(id);
                /*foreach (var gr in currentSpeciality.Groups)
                {
                    var studentsInGroup = await _context.Students
                        .Include(student => student.Group)
                        .Where(student => student.GroupId == gr.Id)
                        .ToListAsync();
                    gr.Students = studentsInGroup;
                }*/
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
            }
            catch (Exception)
            {
                return new SpecialityResponse($"An error occurred when creating the speciality: enumStudyType hasn't value = {item.EnumStudyType}. enumStudyType values: {GetAllEnumValues(typeof(EStudyType))}");
            }
            try
            {
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
            var existedSpec = await GetSpecialityWithGroups(id);
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
            var existedSpec = await GetSpecialityWithGroups(id);
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
            var existedSpec = await GetSpecialityWithGroups(id);
            if (existedSpec == null)
            {
                return new SpecialityResponse("Speciality not found");
            }
            var groups = existedSpec.Groups;
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