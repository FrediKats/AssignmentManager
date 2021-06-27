using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AssignmentManager.Server.Mapping;
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

        private IQueryable<StudentResource> QueryableStudentResource()
        {
            return _context.Students
                .Include(s => s.Group)
                .Include(s => s.Solutions)
                .Select(student =>
                    new StudentResource()
                    {
                        IsuId = student.IsuId,
                        Name = student.Name,
                        LastName = student.Lastname,
                        Email = student.Email,
                        MiddleName = student.Lastname,
                        Phone = student.Phone,
                        Group = new GroupResourceBriefly()
                        {
                            Id = student.GroupId,
                            Name = student.Group.Name,
                            SpecialityId = student.Group.SpecialityId
                        },
                        Solutions = student.Solutions
                            .Select(solution => new SolutionResourceBriefly()
                            {
                                SolutionId = solution.SolutionId,
                                Content = solution.Content,
                                Feedback = solution.Feedback,
                                Grade = solution.Grade,
                            }).ToList(),
                        Subjects = student.Group.Speciality.Subjects
                            .Select(subject => new SubjectResourceBriefly()
                            {
                                SubjectId = subject.SubjectId,
                                SubjectName = subject.SubjectName,
                            }).ToList(),
                        Assignments = student.Group.Speciality.Subjects
                            .SelectMany(sub => sub.Assignments,
                                (sub, ass) => new {Subject = sub, Assignment = ass})
                            .Where(u => u.Subject.Specialities.Contains(student.Group.Speciality))
                            .Select(u => new AssignmentResourceBriefly()
                            {
                                AssignmentId = u.Assignment.AssignmentId,
                                Name = u.Assignment.Name,
                                Description = u.Assignment.Description,
                                Deadline = u.Assignment.Deadline,
                            }).ToList()
                    });
        }

        private IQueryable<StudentResourceBriefly> QueryableStudentResourceBriefly()
        {
            return _context.Students
                .Select(student => new StudentResourceBriefly()
                {
                    Email = student.Email,
                    GroupId = student.GroupId,
                    IsuId = student.IsuId,
                    LastName = student.Lastname,
                    MiddleName = student.MiddleName,
                    Name = student.Name,
                    Phone = student.Phone
                });
        }

        public async Task<List<StudentResourceBriefly>> GetAll()
        {
            return await QueryableStudentResourceBriefly().ToListAsync();
        }

        public async Task<StudentResource> GetById(int id)
        {
            return await QueryableStudentResource().FirstOrDefaultAsync(a => a.IsuId == id);
        }

        public async Task<StudentResource> Create(SaveStudentResource saveStudent)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            var student = new Student(saveStudent);
            student.Group = await _context.Groups.FindAsync(student.GroupId);
            if (student.Group == null) 
            {
                throw new NullReferenceException(GetErrorString(m,$"group with id {student.GroupId} is not existed"));
            }
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return await GetById(student.IsuId); 
        }

        public async Task<StudentResource> Update(int id, SaveStudentResource item)
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            var existedStudent = await _context.Students.FindAsync(id);
            existedStudent.Email = item.Email;
            existedStudent.Lastname = item.LastName;
            existedStudent.Name = item.Name;
            existedStudent.Phone = item.Phone;
            existedStudent.MiddleName = item.MiddleName;
            existedStudent.Group = await _context.Groups.FindAsync(item.GroupId); 
            if (existedStudent.Group == null)
            {
                throw new NullReferenceException(GetErrorString(m,$"group with id {id} is not existed"));
            }
            _context.Students.Update(existedStudent);
            await _context.SaveChangesAsync();
            return await GetById(id);
        }
        public async Task<StudentResource> DeleteById(int id)
        {
            var existedStudent = await GetById(id);
            _context.Students.Remove(await _context.Students
                .Include(s => s.Solutions)
                .Include(s => s.Group)
                .FirstOrDefaultAsync(d => d.IsuId==id));
            await _context.SaveChangesAsync();
            return existedStudent;
        }
    }
}