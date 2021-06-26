using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Persistence;
using AssignmentManager.Server.Persistence.Contexts;
using AssignmentManager.Shared;
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
            MethodBase m = MethodBase.GetCurrentMethod();
            var currentStudent = await _context.Students
                .Include(s => s.Group)
                .Include(s => s.Solutions)
                .FirstOrDefaultAsync(g => g.IsuId == id);
            if (currentStudent == null)
                throw new NullReferenceException(GetErrorString(m, $"student with id {id} is not existed"));
            var currentStudentSpeciality = await _context.Specialities
                .Include(s => s.Groups)
                .Include(s => s.Subjects)
                .FirstOrDefaultAsync(s => s.Groups.Contains(currentStudent.Group));



            _context
                .Students
                .Select(s => new StudentResource()
                {
                    Name = s.Name,
                    Subjects = s.Group.Speciality.Subjects.Select()
                })

            currentStudent.Group.Speciality.Subjects

            foreach (var subject in currentStudentSpeciality.Subjects)
            {
                var assignments = await _context.Assignments
                    .Include(a => a.Subject)
                    .Where(a => a.Subject == subject)
                    .ToListAsync();
                foreach (var assignment in assignments)
                {
                    currentStudent.Assignments.Add(assignment);
                }

                currentStudent.Subjects.Add(subject);
            }

            return currentStudent;
        }

        public async Task<Student> Create(Student student)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            student.Group = await _context.Groups.FindAsync(student.GroupId);
            if (student.Group == null) 
            {
                throw new NullReferenceException(GetErrorString(m,$"group with id {student.GroupId} is not existed"));
            }
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return student; 
        }

        public async Task<Student> Update(int id, Student item)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            var existedStudent = await GetById(id);
            existedStudent.Email = item.Email;
            existedStudent.Lastname = item.Lastname;
            existedStudent.Name = item.Name;
            existedStudent.Phone = item.Phone;
            existedStudent.GroupId = item.GroupId;
            existedStudent.MiddleName = item.MiddleName;
            existedStudent.Group = await _context.Groups.FindAsync(item.GroupId); 
            if (existedStudent.Group == null)
            {
                throw new NullReferenceException(GetErrorString(m,$"group with id {id} is not existed"));
            }
            _context.Students.Update(existedStudent);
            await _context.SaveChangesAsync();
            return existedStudent;
        }
        public async Task<Student> DeleteById(int id)
        {
            var existedStudent = await GetById(id);
            _context.Remove(existedStudent);
            await _context.SaveChangesAsync();
            return existedStudent;
        }
    }
}