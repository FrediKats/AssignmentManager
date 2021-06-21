using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Persistence;
using AssignmentManager.Server.Persistence.Contexts;

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
            return await _context.Solutions
                .Include(solution => solution.Assignment)
                .Include(solution => solution.Students)
                .FirstOrDefaultAsync(solution => solution.SolutionId == id);
        }

        public async Task<Solution> Create(Solution item)
        {
            try
            {
                await _context.Solutions.AddAsync(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred when creating the solution: {ex.Message}");
            }
            return await GetById(item.SolutionId);
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