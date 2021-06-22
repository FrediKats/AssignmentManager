using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Services;
using AssignmentManager.Shared;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentManager.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AssignmentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAssignmentService _service;

        public AssignmentsController(IAssignmentService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<AssignmentResourceBriefly>> GetAllAssignmentsBriefly()
        {
            var assignments = await _service.GetAll();
            var resources = _mapper.Map<List<Assignment>, List<AssignmentResourceBriefly>>(assignments);
            return resources;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Assignment>> GetAssignmentByIdCompletely(int id)
        {
            try
            {
                var assignment = await _service.GetById(id);
                var resources = _mapper.Map<Assignment, AssignmentResource>(assignment);
                return Ok(resources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}