using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Extensions;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Services;
using AssignmentManager.Shared;
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
        public async Task<IReadOnlyCollection<StudentResourceBriefly>> GetAllStudents()
        {
            var students = await _service.GetAll();
            return students;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentResource>> GetGroupByIdCompletely(int id)
        {
            var student = await _service.GetById(id);
            return Ok(student);
        }
        
        [HttpPost]
        public async Task<ActionResult<StudentResource>> CreateStudent([FromBody] SaveStudentResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());
            var result = await _service.Create(resource);
            return Ok(result);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<StudentResource>> UpdateStudent(int id,
            [FromBody] SaveStudentResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());

            var result = await _service.Update(id, resource);
            return Ok(result);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<StudentResource>> DeleteGroup(int id)
        {
            var result = await _service.DeleteById(id);
            return Ok(result);
        }
    }
}