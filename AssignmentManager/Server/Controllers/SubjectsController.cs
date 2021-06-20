using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Extensions;
using Microsoft.AspNetCore.Mvc;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Persistence.Contexts;
using AssignmentManager.Server.Resources;
using AssignmentManager.Server.Services;
using AutoMapper;
using IdentityModel.Client;
using AssignmentManager.Server.Extensions;

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
            var subjects = await _subjectService.GetAllSubjects();
            var resources = _mapper.Map<IEnumerable<Subject>, IEnumerable<SubjectResource>>(subjects);
            return resources;
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
    }
}