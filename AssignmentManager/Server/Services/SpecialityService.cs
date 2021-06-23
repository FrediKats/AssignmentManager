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

        public async Task<Speciality> GetById(int id)
        {
            try
            {
                return await _context.Specialities
                    .Include(s=> s.Groups)
                    .Include(s => s.Subjects)
                    .FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred when getting by id the speciality: {ex.Message}");
            }
            
        }
        public async Task<Speciality> Create(SaveSpecialityResource item)
        {
            Speciality speciality = (Speciality) item;
            try
            {
                speciality.EnumStudyType.ToDescriptionString();
            }
            catch (Exception)
            {
                throw new Exception($"An error occurred when creating the speciality: enumStudyType hasn't value = {item.EnumStudyType}. enumStudyType values: {GetAllEnumValues(typeof(EStudyType))}");
            }
            try
            {
                foreach (var subjectId in item.SubjectsId)
                {
                    var sub = await _context.Subjects.FindAsync(subjectId);
                    if (sub == null)
                        throw new Exception(
                            $"can't find subject with id {subjectId}");
                    speciality.Subjects.Add(sub);
                }
                await _context.Specialities.AddAsync(speciality);
                await _context.SaveChangesAsync();
                return speciality;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred when creating the speciality: {ex.Message}");
            }
        }

        public async Task<Speciality> Update(int id, SaveSpecialityResource item)
        {
            var existedSpec = await GetById(id);
            if (existedSpec == null)
            {
                throw new Exception("Speciality not found");
            }
            
            var speciality = (Speciality) item;
            try
            {
                speciality.EnumStudyType.ToDescriptionString();
            }
            catch (Exception)
            {
                throw new Exception($"An error occurred when creating the speciality: enumStudyType hasn't value = {item.EnumStudyType}. enumStudyType values: {GetAllEnumValues(typeof(EStudyType))}");
            }
            
            existedSpec.Code = speciality.Code;
            existedSpec.EnumStudyType = speciality.EnumStudyType;
            existedSpec.Subjects = new List<Subject>();
            foreach (var subjectId in item.SubjectsId)
            {
                var sub = await _context.Subjects.FindAsync(subjectId);
                if (sub == null)
                    throw new Exception(
                        $"An error occurred when updating the speciality: can't find subject with id {subjectId}");
                existedSpec.Subjects.Add(sub);
            }
            try
            {
                _context.Specialities.Update(existedSpec);
                await _context.SaveChangesAsync();
                return existedSpec;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred when updating the speciality: {ex.Message}");
            }
        }

        public async Task<Speciality> DeleteById(int id)
        {
            var existedSpec = await GetById(id);
            if (existedSpec == null)
            {
                throw new Exception("Speciality not found");
            }
            try
            {   
                _context.Remove(existedSpec);
                await _context.SaveChangesAsync();

                return existedSpec;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred when deleting the speciality: {ex.Message}");
            }
        }

        public async Task<SpecialityResponse> DeleteCascadeById(int id)
        {
            var existedSpec = await GetById(id);
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