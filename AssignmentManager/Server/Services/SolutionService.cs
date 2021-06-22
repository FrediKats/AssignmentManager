using System;
using System.Collections.Generic;
using System.Linq;
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
                throw new Exception($"An error occurred when getting the solution: the solution with {id} is not existed");
            }

            return solution;
        }

        public async Task<Solution> Create(SaveSolutionResource item)
        {
            var solution = (Solution) item;
            
            solution.Students = new List<Student>();
            foreach (var studentId in item.StudentsId)
            {
                try
                {
                    var currentStudent = await _context.Students.FindAsync(studentId);
                    solution.Students.Add(currentStudent);
                }
                catch (Exception)
                {
                    throw new Exception($"An error occurred when updating the solution: student with {studentId} is not found");
                }
            }
            try 
            {
                solution.Assignment = await _context.Assignments.FindAsync(item.AssignmentId);
                await _context.Solutions.AddAsync(solution);
                await _context.SaveChangesAsync();
                return await GetById(solution.SolutionId);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred when creating the solution: {ex.Message}");
            }
        }

        public async Task<Solution> Update(int id, SaveSolutionResource item)
        {
            var existedSolution = await GetById(id);
            if (existedSolution == null)
            {
                throw new Exception($"An error occurred when updating the solution: the solution with {id} is not existed");
            }
            existedSolution.Grade = item.Grade;
            existedSolution.Content = item.Content;
            existedSolution.Feedback = item.Feedback;
            existedSolution.Students = new List<Student>();
            foreach (var studentId in item.StudentsId)
            {
                try
                {
                    var currentStudent = await _context.Students.FindAsync(studentId);
                    existedSolution.Students.Add(currentStudent);
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error occurred when updating the solution: student with {studentId} is not found");
                }
            }

            try
            {
                existedSolution.Assignment = await _context.Assignments.FindAsync(item.AssignmentId);
                _context.Solutions.Update(existedSolution);
                await _context.SaveChangesAsync();
                return await GetById(existedSolution.SolutionId);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred when updating the solution: {ex.Message}");
            }
        }
        
        public async Task<Solution> DeleteById(int id)
        {
            var existedSolution = await GetById(id);
            if (existedSolution == null)
            {
                throw new Exception("Solution not found");
            }
            try
            {
                _context.Remove(existedSolution);
                await _context.SaveChangesAsync();
                return existedSolution;
            } catch (Exception ex)
            {
                throw new Exception($"An error occurred when updating the solution: {ex.Message}");
            }
        }
    }
}