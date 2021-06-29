using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Extensions;
using Microsoft.AspNetCore.Mvc;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Persistence.Contexts;
using AssignmentManager.Server.Services;
using AutoMapper;
using IdentityModel.Client;
using AssignmentManager.Shared;

namespace AssignmentManager.Server.Controllers
{
    [Route("/api/[controller]")]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;
        
        public SubjectsController(ISubjectService subjectService, IMapper mapper)
        {
            _subjectService = subjectService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<SubjectResourceBriefly>> GetAllSubjects()
        {
            var subjects = await _subjectService.GetAllSubjects();
            var resources = _mapper.Map<IEnumerable<Subject>, IEnumerable<SubjectResourceBriefly>>(subjects);
            return resources;
        }
        
        [HttpGet("{id}")]
        public ActionResult<Subject> GetSubjectById(int id)
        {
            try
            {
                var subject = _subjectService.GetById(id).Result;
                var resource = _mapper.Map<Subject, SubjectResource>(subject);
                return Ok(resource);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> PostSubject([FromBody] SaveSubjectResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());
            
            try
            {
                var subject = _mapper.Map<SaveSubjectResource, Subject>(resource);
                var result = await _subjectService.AddAsync(subject);
                var subjectResource = _mapper.Map<Subject, SubjectResource>(result);
                return Ok(subjectResource);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubject(int id, [FromBody] SaveSubjectResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());

            try
            {
                var subject = _mapper.Map<SaveSubjectResource, Subject>(resource);
                var result = await _subjectService.UpdateAsync(id, subject);
                var subjectResource = _mapper.Map<Subject, SubjectResource>(result);
                return Ok(subjectResource);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            try
            {
                var result = await _subjectService.DeleteAsync(id);
                var subjectResource = _mapper.Map<Subject, SubjectResource>(result);
                return Ok(subjectResource);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}