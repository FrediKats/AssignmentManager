using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Resources;
using AssignmentManager.Server.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentManager.Server.Controllers
{
    [Route("/api/[controller]")]
    public class GroupsController : Controller
    {
        private readonly IGroupService _service;
        private readonly IMapper _mapper;

        public GroupsController(IGroupService studentService, IMapper mapper)
        {
            _service = studentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<GroupResource>> ListAsync()
        {
            var groups = await _service.ListAsync();
            var resources = _mapper.Map<IEnumerable<Group>, IEnumerable<GroupResource>>(groups);
            return resources;
        }
    }
}