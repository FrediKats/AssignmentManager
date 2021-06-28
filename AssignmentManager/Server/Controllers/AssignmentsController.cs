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
        public async Task<ActionResult<AssignmentResource>> GetAssignmentByIdCompletely(int id)
        {
            var assignment = await _service.GetById(id);
            var resources = _mapper.Map<Assignment, AssignmentResource>(assignment);
            return Ok(resources);
        }
        
        [HttpPost]
        public async Task<ActionResult<AssignmentResource>> CreateAssignment([FromBody] SaveAssignmentResource assignmentResource) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());
            var assignment = await _service.Create(assignmentResource);
            var resource = _mapper.Map<Assignment, AssignmentResource>(assignment);
            return Ok(resource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AssignmentResource>> UpdateAssignment([FromBody] SaveAssignmentResource assignmentResource, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());
            var assignment = await _service.Update(id, assignmentResource);
            var resource = _mapper.Map<Assignment, AssignmentResource>(assignment);
            return Ok(resource);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<AssignmentResource>> DeleteAssignment(int id)
        {
            var result = await _service.DeleteById(id);
            var resource = _mapper.Map<Assignment, AssignmentResource>(result);
            return Ok(resource);
        }
    }
}