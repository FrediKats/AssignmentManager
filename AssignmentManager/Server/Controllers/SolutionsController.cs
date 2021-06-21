using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Services;
using AssignmentManager.Shared;
using AutoMapper;
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
        public async Task<ActionResult<Solution>> GetSolutionByIdCompletely(int id)
        {
            try
            {
                var solution = await _service.GetById(id);
                var resources = _mapper.Map<Solution, SolutionResource>(solution);
                return Ok(resources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}