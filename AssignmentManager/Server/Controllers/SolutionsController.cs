using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Extensions;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Services;
using AssignmentManager.Shared;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentManager.Server.Controllers
{
    [Route("/api/[controller]")]
    public class SolutionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISolutionService _service;
        
        public SolutionsController(ISolutionService studentService, IMapper mapper)
        {
            _service = studentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<SolutionResourceBriefly>> GetAllSolutionsBriefly()
        {
            var solutions = await _service.GetAll();
            var resources = _mapper.Map<List<Solution>, List<SolutionResourceBriefly>>(solutions);
            return resources;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSolutionByIdCompletely(int id)
        {
            var solution = await _service.GetById(id);
            var resources = _mapper.Map<Solution, SolutionResource>(solution);
            return Ok(resources);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateSolution([FromBody] SaveSolutionResource solutionResource) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());
            var result = await _service.Create(solutionResource);
            return Ok(_mapper.Map<Solution, SolutionResource>(result));
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSolution(int id, [FromBody] SaveSolutionResource solutionResource) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());
            var result = await _service.Update(id, solutionResource);
            return Ok(_mapper.Map<Solution, SolutionResource>(result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSolution(int id)
        {
            var result = await _service.DeleteById(id);
            return Ok(_mapper.Map<Solution, SolutionResource>(result));
        }
    }
}