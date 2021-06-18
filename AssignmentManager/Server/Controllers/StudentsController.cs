using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Extensions;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Resources;
using AssignmentManager.Server.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentManager.Server.Controllers
{
    [Route("/api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStudentService _service;

        public StudentsController(IStudentService studentService, IMapper mapper)
        {
            _service = studentService;
            _mapper = mapper;
        }

        [HttpGet]
        public IReadOnlyCollection<StudentResourceBriefly> GetAllStudents()
        {
            var students = _service.GetAll().Result;
            var resources = _mapper.Map<List<Student>, List<StudentResourceBriefly>>(students);
            return resources;
        }
        
        [HttpGet("{id}")]
        public ActionResult<Group> GetGroupByIdCompletely(int id)
        {
            var student = _service.GetById(id);
            if (!student.Success)
                return BadRequest(student.Message);
            var resources = _mapper
                .Map<Student, StudentResource>(
                    student.Student
                );
            return Ok(resources);
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateStudent([FromBody] SaveStudentResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());
            
            var student = _mapper.Map<SaveStudentResource, Student>(resource);

            var result = await _service.Create(student);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var studentResource = _mapper.Map<Student, StudentResource>(result.Student);
            return Ok(studentResource);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<StudentResourceBriefly>> UpdateStudent(int id,
            [FromBody] SaveStudentResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());

            var student = _mapper.Map<SaveStudentResource, Student>(resource);
            var result = await _service.Update(id, student);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var studentResource = _mapper.Map<Student, StudentResourceBriefly>(result.Student);
            return Ok(studentResource);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<StudentResourceBriefly>> DeleteGroup(int id)
        {
            var result = await _service.DeleteById(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var specialityResource = _mapper.Map<Student, StudentResourceBriefly>(result.Student);
            return  specialityResource;
        }
    }
}