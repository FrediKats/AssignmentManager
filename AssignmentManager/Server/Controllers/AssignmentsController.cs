using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Extensions;
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
        public async Task<IReadOnlyCollection<AssignmentResource>> GetAllAssignmentsBriefly()
        {
            var assignments = await _service.GetAll();
            var resources = _mapper.Map<List<Assignment>, List<AssignmentResource>>(assignments);
            return resources;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssignmentByIdCompletely(int id)
        {
            try
            {
                var assignment = await _service.GetById(id);
                var resources = _mapper.Map<Assignment, AssignmentResource>(assignment);
                return Ok(resources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateAssignment([FromBody] SaveAssignmentResource assignmentResource) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());
            try
            {
                var assignment = await _service.Create(assignmentResource);
                return Ok(_mapper.Map<Assignment, AssignmentResource>(assignment));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssignment([FromBody] SaveAssignmentResource assignmentResource, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());
            try
            {
                var assignment = await _service.Update(id, assignmentResource);
                return Ok(_mapper.Map<Assignment, AssignmentResource>(assignment));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            try
            {
                var result = await _service.DeleteById(id);
                return Ok(_mapper.Map<Assignment, AssignmentResource>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}