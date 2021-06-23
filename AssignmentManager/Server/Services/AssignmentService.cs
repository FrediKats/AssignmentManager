using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Persistence;
using AssignmentManager.Server.Persistence.Contexts;
using AssignmentManager.Shared;

namespace AssignmentManager.Server.Services
{
    public class AssignmentService : BaseService, IAssignmentService
    {
        public AssignmentService(AppDbContext context) : base(context) { }
        
        public async Task<List<Assignment>> GetAll()
        {
            return await _context.Assignments.ToListAsync();
        }

        public async Task<Assignment> GetById(int id)
        {
            var assignment = await _context.Assignments
                .Include(asgn => asgn.Subject)
                .Include(asgn => asgn.Solutions)
                .FirstOrDefaultAsync(asgn => asgn.AssignmentId == id);
            if (assignment == null)
            {
                throw new Exception($"An error occurred while getting an assignment: an assignment with id {id} does not exist");
            }

            return assignment;
        }

        public async Task<Assignment> Create(SaveAssignmentResource item)
        {
            var assigment = (Assignment) item;
            assigment.Subject = await _context.Subjects.FindAsync(item.SubjectId);
            await _context.Assignments.AddAsync(assigment);
            await _context.SaveChangesAsync();
            return assigment;
        }

        public async Task<Assignment> Update(int id, SaveAssignmentResource item)
        {
            var updateForSolution = (Assignment) item;
            var existedAssignment = await GetById(id);

            existedAssignment.Deadline = updateForSolution.Deadline;
            existedAssignment.Description = updateForSolution.Description;
            existedAssignment.Name = updateForSolution.Name;
            existedAssignment.Subject = await _context.Subjects.FindAsync(item.SubjectId);
            try
            {
                _context.Assignments.Update(existedAssignment);
                await _context.SaveChangesAsync();
                return await GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred when updating the assignment: {ex.Message}");
            }
            throw new System.NotImplementedException();
        }

        public async Task<Assignment> DeleteById(int id)
        {
            var existedAssignment = await GetById(id);
            try
            {
                _context.Assignments.Remove(existedAssignment);
                await _context.SaveChangesAsync();
                return existedAssignment;
            } catch (Exception ex)
            {
                throw new Exception($"An error occurred when deleting the solution: {ex.Message}");
            }
        }
    }
}