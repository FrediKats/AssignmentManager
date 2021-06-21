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
    public class InstructorService : BaseService,  IInstructorService
    {
        public InstructorService(AppDbContext context) : base(context)
        {
        }
        public async Task<List<Instructor>> GetAllInstructors()
        {
            return await _context.Instructors.ToListAsync();
        }

        public async Task<InstructorsResponse> GetById(int id)
        {
            return new InstructorsResponse(await _context.Instructors.FindAsync(id));
        }

        public async Task<InstructorsResponse> SaveAsync(Instructor instructor)
        {
            try
            {
                await _context.Instructors.AddAsync(instructor);
                await _context.SaveChangesAsync();
                return new InstructorsResponse(instructor);
            }
            catch (Exception er)
            {
                return new InstructorsResponse(er.Message);
            }
        }

        public async Task<InstructorsResponse> UpdateAsync(int id, Instructor instructor)
        {
            var existingInstructor = await _context.Instructors.FindAsync(id);

            if (existingInstructor == null)
                return new InstructorsResponse("Instructor not found.");

            existingInstructor.FirstName = instructor.FirstName;
            existingInstructor.IsuId = instructor.IsuId;
            existingInstructor.LastName = instructor.LastName;
            existingInstructor.PatronymicName = instructor.PatronymicName;
            existingInstructor.Email = instructor.Email;
            existingInstructor.Phone = instructor.Phone;

            try
            {
                _context.Instructors.Update(instructor);
                await _context.SaveChangesAsync();
                return new InstructorsResponse(existingInstructor);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new InstructorsResponse($"An error occurred when updating the instructor: {ex.Message}");
            }
        }
    }
}