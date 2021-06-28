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
    [Route("/api/[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGroupService _service;
        

        public GroupsController(IGroupService studentService, IMapper mapper)
        {
            _service = studentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<GroupResourceBriefly>> GetAllGroupsBriefly()
        {
            var groups = await _service.GetAll();
            var resources = _mapper.Map<List<Group>, List<GroupResourceBriefly>>(groups);
            return resources;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupResource>> GetGroupByIdCompletely(int id)
        {
            var group = await _service.GetById(id);
            var resources = _mapper.Map<Group, GroupResource>(group);
            return Ok(resources);
        }
        
        [HttpPost]
        public async Task<ActionResult<GroupResource>> CreateGroup([FromBody] SaveGroupResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());
            var result = await _service.Create(resource);
            var groupResource = _mapper.Map<Group, GroupResource>(result);
            return Ok(groupResource);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<GroupResource>> UpdateGroup(int id,
            [FromBody] SaveGroupResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());
            var result = await _service.Update(id, resource);
            var specialityResource = _mapper.Map<Group, GroupResource>(result);
            return Ok(specialityResource);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<GroupResource>> DeleteGroup(int id)
        {
            var result = await _service.DeleteById(id);
            var specialityResource = _mapper.Map<Group, GroupResource>(result);
            return Ok(specialityResource);
        }
    }
}