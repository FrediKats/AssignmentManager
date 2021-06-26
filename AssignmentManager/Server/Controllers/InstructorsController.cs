using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Extensions;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AssignmentManager.Shared;

namespace AssignmentManager.Server.Controllers
{
    [Route("/api/[controller]")]
    public class InstructorsController : ControllerBase
    {
        private readonly IInstructorService _instructorService;
        private readonly IMapper _mapper;
        
        public InstructorsController(IInstructorService instructorService, IMapper mapper)
        {
            _instructorService = instructorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<InstructorResourceBriefly>> GetAllAsync()
        {
            Console.WriteLine("get all");
            var instructors = await _instructorService.GetAllInstructors();
            var resources = _mapper.Map<IEnumerable<Instructor>, IEnumerable<InstructorResourceBriefly>>(instructors);
            return resources;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Instructor>> GetById(int id)
        {
            Console.WriteLine("get by id");
            var instructor = await _instructorService.GetById(id);
            Console.WriteLine(instructor.Success);
            if (!instructor.Success)
            {
                return BadRequest(instructor.Message);
            }
            var resource = _mapper.Map<Instructor, InstructorResource>(instructor.Instructor);
            return Ok(resource);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveInstructorResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());
            
            var instructor = _mapper.Map<SaveInstructorResource, Instructor>(resource);
            var result = await _instructorService.SaveAsync(instructor);

            if (!result.Success)
                return BadRequest(result.Message);

            var instructorResource = _mapper.Map<Instructor, InstructorResource>(result.Instructor);
            return Ok(instructorResource);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<InstructorResource>> PutAsync(int id, [FromBody] SaveInstructorResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());

            var instructor = _mapper.Map<SaveInstructorResource, Instructor>(resource);
            var result = await _instructorService.UpdateAsync(id, instructor);

            if (!result.Success)
                return BadRequest(result.Message);

            var instructorResource = _mapper.Map<Instructor, InstructorResource>(result.Instructor);
            return Ok(instructorResource);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _instructorService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var instructorResource = _mapper.Map<Instructor, InstructorResource>(result.Instructor);
            return Ok(instructorResource);
        }
    }
}