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
    [ApiController]
    [Route("/api/[controller]")]
    public class SpecialitiesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISpecialityService _service;

        public SpecialitiesController(ISpecialityService studentService, IGroupService groupService, IMapper mapper)
        {
            _service = studentService;
            _mapper = mapper;
        }

        [HttpGet]
        public IReadOnlyCollection<SpecialityResourceShort> GetAllSpecialitiesBriefly()
        {
            var specialities = _service.GetAll().Result;
            var resources = _mapper.Map<List<Speciality>, List<SpecialityResourceShort>>(specialities);
            return resources;
        }

        [HttpGet("{id}")]
        public ActionResult<Speciality> GetSpecialityByIdCompletely(int id)
        {
            var speciality = _service.GetById(id).Result;
            if (!speciality.Success)
                return BadRequest(speciality.Message);
            var resources = _mapper
                .Map<Speciality, SpecialityResource>(
                    speciality.Speciality
                );
            return Ok(resources);
        }

        [HttpPost]
        public async Task<ActionResult<SpecialityResourceShort>> CreateSpeciality(
            [FromBody] SaveSpecialityBriefly briefly)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());

            var speciality = _mapper.Map<SaveSpecialityBriefly, Speciality>(briefly);
            var result = await _service.Create(speciality);

            if (!result.Success)
                return BadRequest(result.Message);

            var specialityRecourse = _mapper.Map<Speciality, SpecialityResourceShort>(result.Speciality);
            return Ok(specialityRecourse);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SpecialityResourceShort>> UpdateSpeciality(int id,
            [FromBody] SaveSpecialityBriefly briefly)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());

            var speciality = _mapper.Map<SaveSpecialityBriefly, Speciality>(briefly);
            var result = await _service.Update(id, speciality);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var specialityResource = _mapper.Map<Speciality, SpecialityResourceShort>(result.Speciality);
            return Ok(specialityResource);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SpecialityResource>> DeleteSpeciality(int id)
        {
            var result = await _service.DeleteCascadeById(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var specialityResource = _mapper.Map<Speciality, SpecialityResource>(result.Speciality);
            return  specialityResource;
        }
}
}