﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Extensions;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Resources;
using AssignmentManager.Server.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AssignmentManager.Server.Extensions;

namespace AssignmentManager.Server.Controllers
{
    [Route("/api/[controller]")]
    public class InstructorsController : ControllerBase
    {
        private readonly IInstructorService _instructorService;
        private readonly IMapper _mapper;
        
        public InstructorsController(IInstructorService instructorService, IMapper mapper)
        {
            _instructorService = instructorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<InstructorResource>> GetAllAsync()
        {
            var instructors = await _instructorService.GetAllInstructors();
            var resources = _mapper.Map<IEnumerable<Instructor>, IEnumerable<InstructorResource>>(instructors);
            return resources;
        }
        
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveInstructorResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());
            
            var instructor = _mapper.Map<SaveInstructorResource, Instructor>(resource);
            var result = await _instructorService.SaveAsync(instructor);

            if (!result.Success)
                return BadRequest(result.Message);

            var instructorResource = _mapper.Map<Instructor, InstructorResource>(result.Instructor);
            return Ok(instructorResource);
        }
    }
}