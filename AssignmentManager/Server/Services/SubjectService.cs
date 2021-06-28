using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Persistence;
using AssignmentManager.Server.Persistence.Contexts;
using AssignmentManager.Server.Services.Communication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManager.Server.Services
{
    public class SubjectService : BaseService, ISubjectService
    {
        public SubjectService(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Subject>> GetAllSubjects()
        {
            return await _context.Subjects.ToListAsync();
        }

        public async Task<Subject> GetById(int id)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            var subject = await _context.Subjects
                .Include(s => s.Assignments)
                .Include(s => s.Specialities)
                .FirstOrDefaultAsync(s => s.SubjectId == id);
            if (subject == null)
                throw new NullReferenceException(GetErrorString(m, $"subject with id {id} is not existed"));
            subject.Instructors = new List<Instructor>();
            foreach (var instructorSubject in await _context.InstructorSubjects.ToListAsync())
            {
                if (instructorSubject.SubjectId == id)
                    subject.Instructors.Add(await _context.Instructors.FindAsync(instructorSubject.IsuId));
            }
            return subject;
        }

        public async Task<Subject> AddAsync(Subject subject)
        {
            await _context.Subjects.AddAsync(subject);
            await _context.SaveChangesAsync();
            return await GetById(subject.SubjectId);
        }

        public async Task<Subject> UpdateAsync(int id, Subject subject)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            var existingSubject = await _context.Subjects.FindAsync(id);

            if (existingSubject == null)
                throw new NullReferenceException(GetErrorString(m, $"subject with id {id} is not existed"));

            existingSubject.SubjectName = subject.SubjectName;

            try
            {
                _context.Subjects.Update(existingSubject);
                await _context.SaveChangesAsync();
                return await GetById(subject.SubjectId);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                throw new AggregateException($"An error occurred when updating the subject: {ex.Message}");
            }
        }

        public async Task<Subject> DeleteAsync(int id)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            var existingSubject = await _context.Subjects.FindAsync(id);

            if (existingSubject == null)
                throw new NullReferenceException(GetErrorString(m, $"subject with id {id} is not existed"));

            try
            {
                _context.Subjects.Remove(existingSubject);
                await _context.SaveChangesAsync();
                return existingSubject;
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                throw new AggregateException($"An error occurred when deleting the subject: {ex.Message}");
            }
        }
    }
}