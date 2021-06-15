using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Extensions;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Resources;
using AssignmentManager.Server.Services;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentManager.Server.Controllers
{
    [Route("/api/[controller]")]
    public class SpecialitiesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISpecialityService _service;

        public SpecialitiesController(ISpecialityService studentService, IMapper mapper)
        {
            _service = studentService;
            _mapper = mapper;
        }

        [HttpGet]
        public IReadOnlyCollection<SpecialityResource> GetAll()
        {
            var specialities = _service.GetAll().Result;
            var resources = _mapper.Map<List<Speciality>, List<SpecialityResource>>(specialities);
            return resources;
        }

        [HttpPost]
        public async Task<ActionResult<SpecialityResource>> Post([FromBody] SaveSpecialityResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());
            
            var speciality = _mapper.Map<SaveSpecialityResource, Speciality>(resource);
            var result = await _service.Create(speciality);

            if (!result.Success)
                return BadRequest(result.Message);

            var specialityRecourse = _mapper.Map<Speciality, SpecialityResource>(result.Speciality);
            return Ok(specialityRecourse);
        }
    }
}