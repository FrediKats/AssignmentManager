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
                var speciality = await _context.Specialities
                    .Include(s=> s.Groups)
                    .Include(s => s.Subjects)
                    .FirstOrDefaultAsync(p => p.Id == id);
                if (speciality == null)
                {
                    throw new Exception("Speciality not found");
                }
                return speciality;
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
            var existedSpec = await _context.Specialities
                .Include(s => s.Groups)
                .FirstOrDefaultAsync(p => p.Id == id);
            existedSpec.Groups = await _context.Groups
                    .Include(g => g.Students)
                    .Where(g => g.Speciality==existedSpec).ToListAsync();
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
    }
}