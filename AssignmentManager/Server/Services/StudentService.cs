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
    public class StudentService : BaseService, IStudentService
    {
        public StudentService(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<SaveStudentResponse> CreateAsync(Student item)
        {
            try
            {
                await _context.Students.AddAsync(item);
                await _context.SaveChangesAsync();
                return new SaveStudentResponse(item);
            }
            catch (Exception er)
            {
                return new SaveStudentResponse(er.Message);
            }
        }

        public void UpdateAsync(Student item)
        {
            throw new NotImplementedException();
        }

        public Student DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}