using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Persistence;
using AssignmentManager.Server.Persistence.Contexts;
using AssignmentManager.Shared;

namespace AssignmentManager.Server.Services
{
    public class SolutionService : BaseService, ISolutionService
    {
        
        public SolutionService(AppDbContext context) : base(context) { }
        
        public async Task<List<Solution>> GetAll()
        {
            return await _context.Solutions.ToListAsync();
        }
        
        public async Task<Solution> GetById(int id)
        {
            var solution = await _context.Solutions
                .Include(sol => sol.Assignment)
                .Include(sol => sol.Students)
                .FirstOrDefaultAsync(sol => sol.SolutionId == id);
            if (solution == null)
            {
                throw new NullReferenceException(GetErrorString($"An error occurred when getting the solution: the solution with {id} is not existed"));
            }

            return solution;
        }

        public async Task<Solution> Create(SaveSolutionResource item)
        {
            var solution = (Solution) item;
            solution.Students = new List<Student>();
            foreach (var studentId in item.StudentsId.ToHashSet())
            {
                var currentStudent = await _context.Students.FindAsync(studentId);
                if (currentStudent == null)
                    throw new NullReferenceException(GetErrorString($"a student with id {studentId} is not existed"));
                solution.Students.Add(currentStudent);
            }
            solution.Assignment = await _context.Assignments.FindAsync(item.AssignmentId);
            if (solution.Assignment == null)
                throw new NullReferenceException(GetErrorString($"an assignment with id {solution.AssignmentId} is not existed"));
            await _context.Solutions.AddAsync(solution);
            await _context.SaveChangesAsync();
            return await GetById(solution.SolutionId);
        }

        public async Task<Solution> Update(int id, SaveSolutionResource item)
        {
            var existedSolution = await GetById(id);
            var solutionAssignment = await _context.Assignments.FindAsync(item.AssignmentId);
            if (solutionAssignment == null)
            {
                throw new NullReferenceException(GetErrorString($"assignment with id {item.AssignmentId} is not existed"));
            }
            existedSolution.Grade = item.Grade;
            existedSolution.Content = item.Content;
            existedSolution.Feedback = item.Feedback;
            existedSolution.Students = new List<Student>();
            foreach (var studentId in item.StudentsId.ToHashSet())
            {
                var currentStudent = await _context.Students.FindAsync(studentId);
                if (currentStudent == null) 
                    throw new NullReferenceException(GetErrorString($"student with id {studentId} is not existed"));
                existedSolution.Students.Add(currentStudent);
            }
            
            existedSolution.Assignment = solutionAssignment;
            _context.Solutions.Update(existedSolution);
            await _context.SaveChangesAsync();
            return await GetById(existedSolution.SolutionId);
        }
        
        public async Task<Solution> DeleteById(int id)
        {
            var existedSolution = await GetById(id);
            if (existedSolution == null)
            {
                throw new NullReferenceException(GetErrorString($"a solution with id {id} is not existed"));
            }
            _context.Solutions.Remove(existedSolution);
            await _context.SaveChangesAsync();
            return existedSolution;
        }
    }
}