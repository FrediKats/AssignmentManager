using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Extensions;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Repositories;
using AssignmentManager.Server.Resources;
using AssignmentManager.Server.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;


namespace AssignmentManager.Server.Controllers
{
    [Route("/api/[controller]")]
    public class SpecialitiesController : Controller
    {
        private readonly ISpecialityService _service;
        private readonly IMapper _mapper;

        public SpecialitiesController(ISpecialityService studentService, IMapper mapper)
        {
            _service = studentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<SpecialityResource>> ListAsync()
        {
            var specialities = await _service.ListAsync();
            var resources = _mapper.Map<IEnumerable<Speciality>, IEnumerable<SpecialityResource>>(specialities);
            return resources;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveSpecialityResource resource)
        {
            System.Diagnostics.Debug.WriteLine(resource);
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());

            var speciality = _mapper.Map<SaveSpecialityResource, Speciality>(resource);
            var result = await _service.SaveAsync(speciality);

            if (!result.Success)
                return BadRequest(result.Message);

            var specialityRecourse = _mapper.Map<Speciality, SpecialityResource>(result.Speciality);
            return Ok(specialityRecourse);
        }
    }
}