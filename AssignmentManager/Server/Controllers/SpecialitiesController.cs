using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Services;
using Microsoft.AspNetCore.Mvc;


namespace AssignmentManager.Server.Controllers
{
    [Route("/api/[controller]")]
    public class SpecialitiesController : Controller
    {
        private readonly ISpecialityService _service;

        public SpecialitiesController(ISpecialityService studentService)
        {
            _service = studentService;
        }

        [HttpGet]
        public async Task<IEnumerable<Speciality>> ListAsync()
        {
            var megaFaculties = await _service.ListAsync();
            return megaFaculties;
        }
    }
}