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
    public class InstructorSubjectsController : ControllerBase
    {
        private readonly IInstructorSubjectService _instructorSubjectService;
        private readonly IMapper _mapper;

        public InstructorSubjectsController(IInstructorSubjectService instructorSubjectService, IMapper mapper)
        {
            _instructorSubjectService = instructorSubjectService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<InstructorSubjectResource>> GetAllAsync()
        {
            Console.WriteLine("get all");
            var instructorSubjects = await _instructorSubjectService.GetAllInstructorSubjects();
            var resources =
                _mapper.Map<IEnumerable<InstructorSubject>, IEnumerable<InstructorSubjectResource>>(instructorSubjects);
            return resources;
        }

        [HttpGet("{id}")]
        public ActionResult<InstructorSubject> GetById(int id)
        {
            Console.WriteLine("get by id");
            var instructorSubject = _instructorSubjectService.GetById(id).Result;
            Console.WriteLine(instructorSubject.Success);
            if (!instructorSubject.Success)
            {
                return BadRequest(instructorSubject.Message);
            }

            var resource =
                _mapper.Map<InstructorSubject, InstructorSubjectResource>(instructorSubject.InstructorSubject);
            return Ok(resource);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveInstructorSubjectResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());

            var instructorSubject = _mapper.Map<SaveInstructorSubjectResource, InstructorSubject>(resource);
            var result = await _instructorSubjectService.SaveAsync(instructorSubject);

            if (!result.Success)
                return BadRequest(result.Message);

            var instructorSubjectResource =
                _mapper.Map<InstructorSubject, InstructorSubjectResource>(result.InstructorSubject);
            return Ok(instructorSubjectResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<InstructorSubjectResource>> PutAsync(int id,
            [FromBody] SaveInstructorSubjectResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());

            var instructorSubject = _mapper.Map<SaveInstructorSubjectResource, InstructorSubject>(resource);
            var result = await _instructorSubjectService.UpdateAsync(id, instructorSubject);

            if (!result.Success)
                return BadRequest(result.Message);

            var instructorSubjectResource =
                _mapper.Map<InstructorSubject, InstructorSubjectResource>(result.InstructorSubject);
            return Ok(instructorSubjectResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _instructorSubjectService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var instructorSubjectResource =
                _mapper.Map<InstructorSubject, InstructorSubjectResource>(result.InstructorSubject);
            return Ok(instructorSubjectResource);
        }
    }
}