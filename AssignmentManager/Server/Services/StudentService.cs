using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<Student>> GetAll()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetById(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<StudentResponse> Create(Student item)
        {
            try
            {
                await _context.Students.AddAsync(item);
                await _context.SaveChangesAsync();
                return new StudentResponse(item);
            }
            catch (Exception er)
            {
                return new StudentResponse(er.Message);
            }
        }

        public void Update(Student item)
        {
            throw new NotImplementedException();
        }

        public Student DeleteById(int id)
        {
            throw new NotImplementedException();
        }
    }
}