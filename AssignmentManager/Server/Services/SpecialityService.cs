using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AssignmentManager.Server.Extensions;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Persistence;
using AssignmentManager.Server.Persistence.Contexts;
using AssignmentManager.Shared;
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

        public async Task<Speciality> GetById(int id)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            var speciality = await _context.Specialities
                .Include(s=> s.Groups)
                .Include(s => s.Subjects)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (speciality == null)
            {
                throw new NullReferenceException(GetErrorString(m,$"speciality with id {id} is not existed"));
            }
            return speciality;

        }
        public async Task<Speciality> Create(SaveSpecialityResource item)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            var speciality = new Speciality(item);
            foreach (var subjectId in item.SubjectsId)
            {
                var sub = await _context.Subjects.FindAsync(subjectId);
                if (sub == null)
                    throw new NullReferenceException(GetErrorString(m,$"subject with id {sub.SubjectId} is not existed"));
                speciality.Subjects.Add(sub);
            }
            
            await _context.Specialities.AddAsync(speciality);
            await _context.SaveChangesAsync();
            return speciality;
        }

        public async Task<Speciality> Update(int id, SaveSpecialityResource item)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            var existedSpec = await GetById(id);
            var speciality = new Speciality(item);
            
            existedSpec.Code = speciality.Code;
            existedSpec.StudyType = speciality.StudyType;
            existedSpec.Subjects = new List<Subject>();
            
            foreach (var subjectId in item.SubjectsId)
            {
                var sub = await _context.Subjects.FindAsync(subjectId);
                if (sub == null)
                    throw new NullReferenceException(GetErrorString(m,$"subject with id {id} is not existed"));
                existedSpec.Subjects.Add(sub);
            }
            
            _context.Specialities.Update(existedSpec);
            await _context.SaveChangesAsync();
            return existedSpec;

        }

        public async Task<Speciality> DeleteById(int id)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            var existedSpec = await _context.Specialities
                .Include(s => s.Groups)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (existedSpec == null)
            {
                throw new NullReferenceException(GetErrorString(m,$"speciality with id {id} is not existed"));
            }
            existedSpec.Groups = await _context.Groups
                .Include(g => g.Students)
                .Where(g => g.Speciality == existedSpec).ToListAsync();
            _context.Remove(existedSpec);
            await _context.SaveChangesAsync();

            return existedSpec;
        }
    }
}