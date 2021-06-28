using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Persistence;
using AssignmentManager.Server.Persistence.Contexts;
using AssignmentManager.Server.Services.Communication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManager.Server.Services
{
    public class InstructorService : BaseService,  IInstructorService
    {
        public InstructorService(AppDbContext context) : base(context)
        {
        }
        public async Task<List<Instructor>> GetAllInstructors()
        {
            return await _context.Instructors.ToListAsync();
        }

        public async Task<InstructorResponse> GetById(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor == null)
                return new InstructorResponse("can't find instructor");
            instructor.Subjects = new List<Subject>();
            foreach (var instructorSubject in await _context.InstructorSubjects.ToListAsync())
            {
                if (instructorSubject.IsuId == id)
                    instructor.Subjects.Add(await _context.Subjects.FindAsync(instructorSubject.SubjectId));
            }
            return new InstructorResponse(instructor);
        }

        public async Task<InstructorResponse> AddAsync(Instructor instructor)
        {
            try
            {
                await _context.Instructors.AddAsync(instructor);
                await _context.SaveChangesAsync();
                return new InstructorResponse(instructor);
            }
            catch (Exception er)
            {
                return new InstructorResponse(er.Message);
            }
        }

        public async Task<InstructorResponse> UpdateAsync(int id, Instructor instructor)
        {
            var existingInstructor = await _context.Instructors.FindAsync(id);

            if (existingInstructor == null)
                return new InstructorResponse("Instructor not found.");

            existingInstructor.FirstName = instructor.FirstName;
            existingInstructor.IsuId = instructor.IsuId;
            existingInstructor.LastName = instructor.LastName;
            existingInstructor.PatronymicName = instructor.PatronymicName;
            existingInstructor.Email = instructor.Email;
            existingInstructor.Phone = instructor.Phone;

            try
            {
                _context.Instructors.Update(existingInstructor);
                await _context.SaveChangesAsync();
                return new InstructorResponse(existingInstructor);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new InstructorResponse($"An error occurred when updating the instructor: {ex.Message}");
            }
        }

        public async Task<InstructorResponse> DeleteAsync(int id)
        {
            var existingInstructor = await _context.Instructors.FindAsync(id);

            if (existingInstructor == null)
                return new InstructorResponse("Instructor not found.");

            try
            {
                _context.Instructors.Remove(existingInstructor);
                await _context.SaveChangesAsync();
                return new InstructorResponse(existingInstructor);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new InstructorResponse($"An error occurred when deleting the instructor: {ex.Message}");
            }
        }
    }
}