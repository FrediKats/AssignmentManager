using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IEnumerable<InstructorSubjectResource>> GetAllInstructorSubjects()
        {
            var instructorSubjects = await _instructorSubjectService.GetAllInstructorSubjects();
            var resources =
                _mapper.Map<IEnumerable<InstructorSubject>, IEnumerable<InstructorSubjectResource>>(instructorSubjects);
            return resources;
        }

        [HttpGet("{id}")]
        public ActionResult<InstructorSubject> GetInstructorSubjectById(int id)
        {
            try
            {
                var instructorSubject = _instructorSubjectService.GetById(id).Result;
                var resource =
                    _mapper.Map<InstructorSubject, InstructorSubjectResource>(instructorSubject);
                return Ok(resource);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("subjects/{IsuId}")]
        public async Task<IEnumerable<int>> GetAllSubjectsForInstructor(int isuId)
        {
            var instructorSubjects = await _instructorSubjectService.GetAllInstructorSubjects();
            var resources =
                _mapper.Map<IEnumerable<InstructorSubject>, IEnumerable<InstructorSubjectResource>>(instructorSubjects);
            return resources
                .Where(q=> q.IsuId == isuId)
                .Select(q => q.SubjectId);
        }
        
        [HttpGet("instructors/{SubjectId}")]
        public async Task<IEnumerable<int>> GetAllInstructorsForSubject(int subjectId)
        {
            var instructorSubjects = await _instructorSubjectService.GetAllInstructorSubjects();
            var resources =
                _mapper.Map<IEnumerable<InstructorSubject>, IEnumerable<InstructorSubjectResource>>(instructorSubjects);
            return resources
                .Where(q=> q.SubjectId == subjectId)
                .Select(q => q.IsuId);
        }

        [HttpPost]
        public async Task<IActionResult> PostInstructorSubject([FromBody] SaveInstructorSubjectResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());
            try
            {
                var instructorSubject = _mapper.Map<SaveInstructorSubjectResource, InstructorSubject>(resource);
                var result = await _instructorSubjectService.AddAsync(instructorSubject);
                var instructorSubjectResource =
                    _mapper.Map<InstructorSubject, InstructorSubjectResource>(result);
                return Ok(instructorSubjectResource);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<InstructorSubjectResource>> PutInstructorSubject(int id,
            [FromBody] SaveInstructorSubjectResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());

            try
            {
                var instructorSubject = _mapper.Map<SaveInstructorSubjectResource, InstructorSubject>(resource);
                var result = await _instructorSubjectService.UpdateAsync(id, instructorSubject);
                var instructorSubjectResource =
                    _mapper.Map<InstructorSubject, InstructorSubjectResource>(result);
                return Ok(instructorSubjectResource);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstructorSubject(int id)
        {
            try
            {
                var result = await _instructorSubjectService.DeleteAsync(id);
                var instructorSubjectResource =
                    _mapper.Map<InstructorSubject, InstructorSubjectResource>(result);
                return Ok(instructorSubjectResource);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}