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

        public StudentResponse GetById(int id)
        {
            try
            {
                var currentStudent = _context.Students
                    .Include(s => s.Group)
                    .Include(s => s.Group.Speciality)
                    .First(g => g.IsuId == id);
                return new StudentResponse(currentStudent);
            }
            catch (Exception ex)
            {
                return new StudentResponse($"An error occurred when getting by id the student: {ex.Message}");
            }
        }

        public async Task<StudentResponse> Create(Student student)
        {
            try
            {
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
                var existedGroup = await _context.Groups
                    .FirstOrDefaultAsync(g => g.Id == student.GroupId);
                student.Group = existedGroup;
                return new StudentResponse(student);
            }
            catch (Exception ex)
            {
                return new StudentResponse($"An error occurred when created student: {ex.Message}");
            }
        }
        public async Task<StudentResponse> Update(int id, Student item)
        {
            var existedStudent = await _context.Students
                .FindAsync(id);
            if (existedStudent == null)
            {
                return new StudentResponse("Student not found");
            }
            existedStudent.Email = item.Email;
            existedStudent.Lastname = item.Lastname;
            existedStudent.Name = item.Name;
            existedStudent.Phone = item.Phone;
            existedStudent.GroupId = item.GroupId;
            existedStudent.MiddleName = item.MiddleName;
            try
            {
                var existedGroup = await _context.Groups
                    .FirstOrDefaultAsync(g => g.Id == existedStudent.GroupId);
                _context.Students.Update(existedStudent);
                await _context.SaveChangesAsync();
                existedStudent.Group = existedGroup;
                return new StudentResponse(existedStudent);
            }
            catch (Exception ex)
            {
                return new StudentResponse($"An error occurred when updating the student: {ex.Message}");
            }
        }

        public async Task<StudentResponse> DeleteById(int id)
        {
            var existedStudent = _context.Students
                .Include(s=> s.Group)
                .FirstOrDefault(p => p.IsuId == id);
            if (existedStudent == null)
            {
                return new StudentResponse("Student not found");
            }
            try
            {
                _context.Remove(existedStudent);
                await _context.SaveChangesAsync();
                return new StudentResponse(existedStudent);
            }
            catch (Exception ex)
            {
                return new StudentResponse($"An error occurred when deleting the student: {ex.Message}");
            }
        }
    }
}