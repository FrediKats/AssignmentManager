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
    [ApiController]
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
        public async Task<IReadOnlyCollection<SpecialityResourceBriefly>> GetAllSpecialitiesBriefly()
        {
            var specialities = await _service.GetAll();
            var resources = _mapper.Map<List<Speciality>, List<SpecialityResourceBriefly>>(specialities);
            return resources;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SpecialityResource>> GetSpecialityByIdCompletely(int id)
        {
            var speciality = await _service.GetById(id);
            var resources = _mapper.Map<Speciality, SpecialityResource>(speciality);
            return Ok(resources);
        }

        [HttpPost]
        public async Task<ActionResult<SpecialityResource>> CreateSpeciality(
            [FromBody] SaveSpecialityResource specialityResource)
        {
            var result = await _service.Create(specialityResource);
            var specialityRecourse = _mapper.Map<Speciality, SpecialityResource>(result);
            return Ok(specialityRecourse);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SpecialityResource>> UpdateSpeciality(int id,
            [FromBody] SaveSpecialityResource briefly)
        {
            var result = await _service.Update(id, briefly);
            var specialityResource = _mapper.Map<Speciality, SpecialityResource>(result);
            return Ok(specialityResource);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SpecialityResource>> DeleteSpeciality(int id)
        {
            var result = await _service.DeleteById(id);
            var specialityResource = _mapper.Map<Speciality, SpecialityResource>(result);
            return Ok(specialityResource);
        }
}
}