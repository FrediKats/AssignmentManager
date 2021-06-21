using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Extensions;
using AssignmentManager.Server.Models;
using AssignmentManager.Shared;
using AssignmentManager.Server.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IEnumerable<InstructorResource>> GetAllAsync()
        {
            Console.WriteLine("get all");
            var instructors = await _instructorService.GetAllInstructors();
            var resources = _mapper.Map<IEnumerable<Instructor>, IEnumerable<InstructorResource>>(instructors);
            return resources;
        }

        [HttpGet("{id}")]
        public ActionResult<Instructor> GetById(int id)
        {
            Console.WriteLine("get by id");
            var instructor = _instructorService.GetById(id).Result;
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
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveInstructorResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());

            var category = _mapper.Map<SaveInstructorResource, Instructor>(resource);
            var result = await _instructorService.UpdateAsync(id, category);

            if (!result.Success)
                return BadRequest(result.Message);

            var categoryResource = _mapper.Map<Instructor, InstructorResource>(result.Instructor);
            return Ok(categoryResource);
        }
    }
}