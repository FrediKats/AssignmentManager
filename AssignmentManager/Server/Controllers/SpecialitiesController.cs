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
        public async Task<IActionResult> GetSpecialityByIdCompletely(int id)
        {
            try
            {
                var speciality = await _service.GetById(id);
                var resources = _mapper.Map<Speciality, SpecialityResource>(speciality);
                return Ok(resources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpeciality(
            [FromBody] SaveSpecialityResource specialityResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());
            var result = await _service.Create(specialityResource);

            var specialityRecourse = _mapper.Map<Speciality, SpecialityResource>(result);
            return Ok(specialityRecourse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSpeciality(int id,
            [FromBody] SaveSpecialityResource briefly)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());
            try
            {
                var result = await _service.Update(id, briefly);
                var specialityResource = _mapper.Map<Speciality, SpecialityResource>(result);
                return Ok(specialityResource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpeciality(int id)
        {
            try
            {
                var result = await _service.DeleteById(id);
                var specialityResource = _mapper.Map<Speciality, SpecialityResource>(result);
                return Ok(specialityResource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
}
}