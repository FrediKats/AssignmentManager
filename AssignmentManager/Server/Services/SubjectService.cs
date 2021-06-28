using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<SubjectResponse> GetById(int id)
        {
            var subject = await _context.Subjects
                .Include(s => s.Assignments)
                .Include(s => s.Specialities)
                .FirstOrDefaultAsync(s => s.SubjectId == id);
            if (subject == null)
                return new SubjectResponse("Subject not found");
            subject.Instructors = new List<Instructor>();
            foreach (var instructorSubject in await _context.InstructorSubjects.ToListAsync())
            {
                if (instructorSubject.SubjectId == id)
                    subject.Instructors.Add(await _context.Instructors.FindAsync(instructorSubject.IsuId));
            }
            return new SubjectResponse(subject);
        }

        public async Task<SubjectResponse> AddAsync(Subject subject)
        {
            try
            {
                await _context.Subjects.AddAsync(subject);
                await _context.SaveChangesAsync();
                return new SubjectResponse(subject);
            }
            catch (Exception er)
            {
                return new SubjectResponse(er.Message);
            }
        }

        public async Task<SubjectResponse> UpdateAsync(int id, Subject subject)
        {
            var existingSubject = await _context.Subjects.FindAsync(id);

            if (existingSubject == null)
                return new SubjectResponse("Subject not found.");

            existingSubject.SubjectName = subject.SubjectName;

            try
            {
                _context.Subjects.Update(existingSubject);
                await _context.SaveChangesAsync();
                return new SubjectResponse(existingSubject);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SubjectResponse($"An error occurred when updating the subject: {ex.Message}");
            }
        }

        public async Task<SubjectResponse> DeleteAsync(int id)
        {
            var existingSubject = await _context.Subjects.FindAsync(id);

            if (existingSubject == null)
                return new SubjectResponse("Category not found.");

            try
            {
                _context.Subjects.Remove(existingSubject);
                await _context.SaveChangesAsync();
                return new SubjectResponse(existingSubject);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SubjectResponse($"An error occurred when deleting the subject: {ex.Message}");
            }
        }
    }
}