using System;
using System.Collections.Generic;
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

        public async Task<InstructorSubjectResponse> GetById(int id)
        {
            return new InstructorSubjectResponse(await _context.InstructorSubjects.FindAsync(id));
        }

        public async Task<InstructorSubjectResponse> AddAsync(InstructorSubject instructorSubject)
        {
            try
            {
                await _context.InstructorSubjects.AddAsync(instructorSubject);
                await _context.SaveChangesAsync();
                return new InstructorSubjectResponse(instructorSubject);
            }
            catch (Exception er)
            {
                return new InstructorSubjectResponse(er.Message);
            }
        }

        public async Task<InstructorSubjectResponse> UpdateAsync(int id, InstructorSubject instructorSubject)
        {
            var existingInstructorSubject = await _context.InstructorSubjects.FindAsync(id);

            if (existingInstructorSubject == null)
                return new InstructorSubjectResponse("InstructorSubject not found.");

            existingInstructorSubject.IsuId = instructorSubject.IsuId;
            existingInstructorSubject.SubjectId = instructorSubject.SubjectId;

            try
            {
                _context.InstructorSubjects.Update(existingInstructorSubject);
                await _context.SaveChangesAsync();
                return new InstructorSubjectResponse(existingInstructorSubject);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new InstructorSubjectResponse($"An error occurred when updating the instructorSubject: {ex.Message}");
            }
        }

        public async Task<InstructorSubjectResponse> DeleteAsync(int id)
        {
            var existingInstructorSubject = await _context.InstructorSubjects.FindAsync(id);

            if (existingInstructorSubject == null)
                return new InstructorSubjectResponse("InstructorSubject not found.");

            try
            {
                _context.InstructorSubjects.Remove(existingInstructorSubject);
                await _context.SaveChangesAsync();
                return new InstructorSubjectResponse(existingInstructorSubject);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new InstructorSubjectResponse($"An error occurred when deleting the instructorSubject: {ex.Message}");
            }
        }
    }
}