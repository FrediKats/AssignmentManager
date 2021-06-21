using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Extensions;
using Microsoft.AspNetCore.Mvc;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Persistence.Contexts;
using AssignmentManager.Shared;
using AssignmentManager.Server.Services;
using AutoMapper;
using IdentityModel.Client;

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
        public async Task<IEnumerable<SubjectResource>> GetAllAsync()
        {
            Console.WriteLine("get all");
            var subjects = await _subjectService.GetAllSubjects();
            var resources = _mapper.Map<IEnumerable<Subject>, IEnumerable<SubjectResource>>(subjects);
            return resources;
        }
        
        [HttpGet("{id}")]
        public ActionResult<Subject> GetById(int id)
        {
            var subject = _subjectService.GetById(id).Result;
            Console.WriteLine(subject.Success);
            if (!subject.Success)
            {
                return BadRequest(subject.Message);
            }
            var resource = _mapper.Map<Subject, SubjectResource>(subject.Subject);
            return Ok(resource);
        }
        
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveSubjectResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());
            
            var subject = _mapper.Map<SaveSubjectResource, Subject>(resource);
            var result = await _subjectService.SaveAsync(subject);

            if (!result.Success)
                return BadRequest(result.Message);

            var subjectResource = _mapper.Map<Subject, SubjectResource>(result.Subject);
            return Ok(subjectResource);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveSubjectResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());

            var category = _mapper.Map<SaveSubjectResource, Subject>(resource);
            var result = await _subjectService.UpdateAsync(id, category);

            if (!result.Success)
                return BadRequest(result.Message);

            var categoryResource = _mapper.Map<Subject, SubjectResource>(result.Subject);
            return Ok(categoryResource);
        }
    }
}