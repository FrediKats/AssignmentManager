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
            try
            {
                var currentStudent = await _context.Students
                    .Include(s => s.Group)
                    .Include(s => s.Solutions)
                    .FirstOrDefaultAsync(g => g.IsuId == id);
                if (currentStudent == null)
                    throw new Exception("Student not found");
                return currentStudent;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred when getting by id the student: {ex.Message}");
            }
        }

        public async Task<Student> Create(Student student)
        {
            try
            {
                student.Group = await _context.Groups.FindAsync(student.GroupId);
                if (student.Group == null)
                {
                    throw new Exception($"Group with id {student.GroupId} is not existed");
                }
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
                return student;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred when created student: {ex.Message}");
            }
        }
        
        public async Task<Student> Update(int id, Student item)
        {
            var existedStudent = await GetById(id);
            existedStudent.Email = item.Email;
            existedStudent.Lastname = item.Lastname;
            existedStudent.Name = item.Name;
            existedStudent.Phone = item.Phone;
            existedStudent.GroupId = item.GroupId;
            existedStudent.MiddleName = item.MiddleName;
            try
            {
                existedStudent.Group = await _context.Groups.FindAsync(item.GroupId);
                if (existedStudent.Group == null)
                {
                    throw new Exception($"Group with id {item.GroupId} is not existed");
                }
                _context.Students.Update(existedStudent);
                await _context.SaveChangesAsync();
                return existedStudent;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred when updating the student: {ex.Message}");
            }
        }
        
        public async Task<Student> DeleteById(int id)
        {
            var existedStudent = await GetById(id);
            try
            {
                _context.Remove(existedStudent);
                await _context.SaveChangesAsync();
                return existedStudent;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred when deleting the student: {ex.Message}");
            }
        }
    }
}