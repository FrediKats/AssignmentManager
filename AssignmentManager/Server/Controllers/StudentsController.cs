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
            var resources = _mapper.Map<List<Student>, List<StudentResourceBriefly>>(students);
            return resources;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupByIdCompletely(int id)
        {
            try
            {
                var student = await _service.GetById(id);
                var resources = _mapper.Map<Student, StudentResource>(student);
                return Ok(resources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] SaveStudentResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());
            try
            {
                var result = await _service.Create(resource);
                var studentResource = _mapper.Map<Student, StudentResource>(result);
                return Ok(studentResource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id,
            [FromBody] SaveStudentResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());
            try
            {
                var result = await _service.Update(id, resource);
                var studentResource = _mapper.Map<Student, StudentResourceBriefly>(result);
                return Ok(studentResource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            try
            {
                var result = await _service.DeleteById(id);
                var specialityResource = _mapper.Map<Student, StudentResourceBriefly>(result);
                return Ok(specialityResource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}