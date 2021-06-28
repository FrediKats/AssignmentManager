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
            var instructors = await _instructorService.GetAllInstructors();
            var resources = _mapper.Map<IEnumerable<Instructor>, IEnumerable<InstructorResourceBriefly>>(instructors);
            return resources;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Instructor>> GetById(int id)
        {
            try
            {
                var instructor = await _instructorService.GetById(id);
                var resource = _mapper.Map<Instructor, InstructorResource>(instructor);
                return Ok(resource);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveInstructorResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());
            try
            {
                var instructor = _mapper.Map<SaveInstructorResource, Instructor>(resource);
                var result = await _instructorService.AddAsync(instructor);
                var instructorResource = _mapper.Map<Instructor, InstructorResource>(result);
                return Ok(instructorResource);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<InstructorResource>> PutAsync(int id, [FromBody] SaveInstructorResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());

            try
            {
                var instructor = _mapper.Map<SaveInstructorResource, Instructor>(resource);
                var result = await _instructorService.UpdateAsync(id, instructor);
                var instructorResource = _mapper.Map<Instructor, InstructorResource>(result);
                return Ok(instructorResource);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var result = await _instructorService.DeleteAsync(id);
                var instructorResource = _mapper.Map<Instructor, InstructorResource>(result);
                return Ok(instructorResource);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}