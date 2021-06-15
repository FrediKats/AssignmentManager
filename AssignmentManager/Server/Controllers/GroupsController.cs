using System.Collections.Generic;
using System.Threading.Tasks;
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
        public IReadOnlyCollection<GroupResource> GetAll()
        {
            var groups = _service.GetAll().Result;
            var resources = _mapper.Map<List<Group>, List<GroupResource>>(groups);
            return resources;
        }
    }
}