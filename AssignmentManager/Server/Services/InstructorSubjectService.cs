using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Persistence;
using AssignmentManager.Server.Persistence.Contexts;
using AssignmentManager.Server.Services.Communication;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManager.Server.Services
{
    public class InstructorSubjectService : BaseService, IInstructorSubjectService
    {
        public InstructorSubjectService(AppDbContext context) : base(context)
        {
        }
        
        public async Task<List<InstructorSubject>> GetAllInstructorSubjects()
        {
            return await _context.InstructorSubjects.ToListAsync();
        }

        public async Task<InstructorSubject> GetById(int id)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            var instructorSubject = await _context.InstructorSubjects.FindAsync(id);
            if (instructorSubject == null)
                throw new NullReferenceException(GetErrorString(m, $"instructorsubject with id {id} is not existed"));
            return instructorSubject;
        }

        public async Task<InstructorSubject> AddAsync(InstructorSubject instructorSubject)
        {
            await _context.InstructorSubjects.AddAsync(instructorSubject);
            await _context.SaveChangesAsync();
            return await GetById(instructorSubject.Id);
        }

        public async Task<InstructorSubject> UpdateAsync(int id, InstructorSubject instructorSubject)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            var existingInstructorSubject = await _context.InstructorSubjects.FindAsync(id);

            if (existingInstructorSubject == null)
                throw new NullReferenceException(GetErrorString(m, $"instructorsubject with id {id} is not existed"));

            existingInstructorSubject.IsuId = instructorSubject.IsuId;
            existingInstructorSubject.SubjectId = instructorSubject.SubjectId;

            try
            {
                _context.InstructorSubjects.Update(existingInstructorSubject);
                await _context.SaveChangesAsync();
                return await GetById(id);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                throw new AggregateException($"An error occurred when updating the instructorSubject: {ex.Message}");
            }
        }

        public async Task<InstructorSubject> DeleteAsync(int id)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            var existingInstructorSubject = await _context.InstructorSubjects.FindAsync(id);

            if (existingInstructorSubject == null)
                throw new NullReferenceException(GetErrorString(m, $"instructorsubject with id {id} is not existed"));

            try
            {
                _context.InstructorSubjects.Remove(existingInstructorSubject);
                await _context.SaveChangesAsync();
                return existingInstructorSubject;
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                throw new AggregateException($"An error occurred when deleting the instructorSubject: {ex.Message}");
            }
        }
    }
}