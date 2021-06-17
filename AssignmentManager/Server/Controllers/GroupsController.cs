﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Extensions;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Resources;
using AssignmentManager.Server.Services;
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
        public IReadOnlyCollection<GroupResourceBriefly> GetAllGroupsBriefly()
        {
            var groups = _service.GetAll().Result;
            var resources = _mapper.Map<List<Group>, List<GroupResourceBriefly>>(groups);
            return resources;
        }
        
        [HttpGet("{id}")]
        public ActionResult<Group> GetGroupByIdCompletely(int id)
        {
            var speciality = _service.GetById(id).Result;
            if (!speciality.Success)
                return BadRequest(speciality.Message);
            var resources = _mapper
                .Map<Group, GroupResource>(
                    speciality.Group
                );
            return Ok(resources);
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateGroup([FromBody] SaveGroupResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());
            
            var group = _mapper.Map<SaveGroupResource, Group>(resource);

            var result = await _service.Create(group);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var groupResource = _mapper.Map<Group, GroupResource>(result.Group);
            return Ok(groupResource);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<GroupResource>> UpdateGroup(int id,
            [FromBody] SaveGroupResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());

            var group = _mapper.Map<SaveGroupResource, Group>(resource);
            var result = await _service.Update(id, group);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var specialityResource = _mapper.Map<Group, GroupResource>(result.Group);
            return Ok(specialityResource);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<GroupResourceBriefly>> DeleteGroup(int id)
        {
            var result = await _service.DeleteCascadeById(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var specialityResource = _mapper.Map<Group, GroupResourceBriefly>(result.Group);
            return  specialityResource;
        }
    }
}