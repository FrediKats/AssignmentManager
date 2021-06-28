using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public async Task<Instructor> GetById(int id)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor == null)
                throw new NullReferenceException(GetErrorString(m, $"instructor with id {id} is not existed"));
            instructor.Subjects = new List<Subject>();
            foreach (var instructorSubject in await _context.InstructorSubjects.ToListAsync())
            {
                if (instructorSubject.IsuId == id)
                    instructor.Subjects.Add(await _context.Subjects.FindAsync(instructorSubject.SubjectId));
            }

            return instructor;
        }

        public async Task<Instructor> AddAsync(Instructor instructor)
        {
            await _context.Instructors.AddAsync(instructor);
            await _context.SaveChangesAsync();
            return await GetById(instructor.IsuId);
        }

        public async Task<Instructor> UpdateAsync(int id, Instructor instructor)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            var existingInstructor = await _context.Instructors.FindAsync(id);

            if (existingInstructor == null)
                throw new NullReferenceException(GetErrorString(m, $"instructor with id {id} is not existed"));

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
                return await GetById(id);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                throw new AggregateException($"An error occurred when updating the instructor: {ex.Message}");
            }
        }

        public async Task<Instructor> DeleteAsync(int id)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            var existingInstructor = await _context.Instructors.FindAsync(id);

            if (existingInstructor == null)
                throw new NullReferenceException(GetErrorString(m, $"instructor with id {id} is not existed"));

            try
            {
                _context.Instructors.Remove(existingInstructor);
                await _context.SaveChangesAsync();
                return existingInstructor;
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                throw new AggregateException($"An error occurred when deleting the instructor: {ex.Message}");
            }
        }
    }
}