using System;
using System.Collections.Generic;
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
            try
            {
                foreach (var studentId in item.StudentsId)
                {
                    var currentStudent = await _context.Students.FindAsync(studentId);
                    if (currentStudent.Solutions == null)
                    {
                        currentStudent.Solutions = new List<Solution>();
                    }
                    currentStudent.Solutions.Add(solution);
                }
                solution.Assignment = await _context.Assignments.FindAsync(item.AssignmentId);
                await _context.Solutions.AddAsync(solution);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred when creating the solution: {ex.Message}");
            }
            return await GetById(solution.SolutionId);
        }

        public async Task<Solution> Update(int id, Solution item)
        {
            var existedSolution = await GetById(id);
            if (existedSolution == null)
            {
                throw new Exception($"An error occurred when updating the solution: the solution with {id} is not existed");
            }
            existedSolution.Grade = item.Grade;
            existedSolution.Content = item.Content;
            existedSolution.Feedback = item.Feedback;
            try
            {
                _context.Solutions.Update(existedSolution);
                await _context.SaveChangesAsync();
                return existedSolution;
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